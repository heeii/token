#include "arduino_base64.hpp"
#include "mbedtls/pk.h"
#include "mbedtls/rsa.h"
#include "mbedtls/error.h"
#include "mbedtls/base64.h"
#include "mbedtls/entropy.h"
#include "mbedtls/ctr_drbg.h"
#include <mbedtls/aes.h>
#include <mbedtls/base64.h>
#define PERS "rsa_encrypt"

const String pass = "edfjgh563i0q";
const int ID = 86428642; // уникальный айди для каждого физического токена
String inputString;  // Переменная для хранения строки

mbedtls_pk_context public_key;
mbedtls_entropy_context entropy;
mbedtls_ctr_drbg_context ctr_drbg;

String esp_pub_key_pem_test = ""; // Переменная для хранения ключа


size_t get_encoded_size(size_t plain_data_size) {
  return (4 * ceil((double)plain_data_size / 3)) + 1;
}

void RSA_Encrypt(const char* plain_text_p) { // шифрование для отправки ключа AES
  mbedtls_pk_init(&public_key);
  mbedtls_ctr_drbg_init(&ctr_drbg);
  mbedtls_entropy_init(&entropy);
  int res = mbedtls_ctr_drbg_seed(&ctr_drbg, mbedtls_entropy_func, &entropy, (const uint8_t*)PERS, strlen(PERS));
  if (res != 0) {
    char buff[512];
    mbedtls_strerror(res, buff, sizeof(buff));
    printf("Error mbedtls_ctr_drbg_seed = %s", buff);
    return;
  }

  // Формируем PEM-ключ из переданного ключа
  String pemKey = "-----BEGIN PUBLIC KEY-----\n";
  pemKey += esp_pub_key_pem_test;
  pemKey += "\n-----END PUBLIC KEY-----";

  size_t key_len = pemKey.length() + 1;
  res = mbedtls_pk_parse_public_key(&public_key, (const uint8_t*)pemKey.c_str(), key_len);
  if (res != 0) {
    char buff[512];
    mbedtls_strerror(res, buff, sizeof(buff));
    printf("Parse key error: %s\n", buff);
    return;
  }

  // Используем mbedtls_rsa_get_len() для получения длины ключа
  size_t ciphertext_buff_len = mbedtls_rsa_get_len(mbedtls_pk_rsa(public_key));
  uint8_t *ciphertext_buff = (uint8_t*)malloc(ciphertext_buff_len); // выделение памяти под данные

  mbedtls_rsa_set_padding(mbedtls_pk_rsa(public_key), MBEDTLS_RSA_PKCS_V21, MBEDTLS_MD_SHA256);

  res = mbedtls_rsa_rsaes_pkcs1_v15_encrypt(
        mbedtls_pk_rsa(public_key),  // Контекст RSA
        mbedtls_ctr_drbg_random,     // Функция генерации случайных чисел
        &ctr_drbg,                   // Контекст для генерации случайных чисел
        strlen(plain_text_p),        // Длина открытого текста
        (const uint8_t*)plain_text_p, // Открытый текст
        ciphertext_buff              // Буфер для зашифрованного текста
    );

  if (res != 0) {
    char buff[512];
    mbedtls_strerror(res, buff, sizeof(buff));
    printf("Encryption error: %s\n", buff);
    free(ciphertext_buff);
    return;
  }

  size_t outlen = 0;
  size_t encoded_buff_size = get_encoded_size(ciphertext_buff_len);

  char *ciphertext_base64_buff = (char*)malloc(encoded_buff_size); // выделение памяти
  if (ciphertext_base64_buff == NULL) {
    printf("Failed to allocate %zu bytes...\n", encoded_buff_size);
    free(ciphertext_buff);
    return;
  }

  res = mbedtls_base64_encode( // преобразование к base64 для защиты от потерь
    (uint8_t*)ciphertext_base64_buff,
    encoded_buff_size,
    &outlen,
    ciphertext_buff,
    ciphertext_buff_len
  );
  if (res != 0) { // обработка ошибок длины
    char buff[512];
    mbedtls_strerror(res, buff, sizeof(buff));
    printf("Encode error: %s\n", buff);
    free(ciphertext_base64_buff);
    free(ciphertext_buff);
    return;
  }

  printf(ciphertext_base64_buff);
  printf("\n");
  free(ciphertext_base64_buff); // освобождение памяти от шифрованных данных
  free(ciphertext_buff);        // освобождение памяти
}

// Глобальные переменные для хранения ключа и IV
byte aesKey[32];
byte aesIv[16];

void add_pkcs7_padding(uint8_t* data, size_t* length, size_t block_size) {
    size_t padding_length = block_size - (*length % block_size);
    for (size_t i = 0; i < padding_length; i++) {
        data[*length + i] = (uint8_t)padding_length;
    }
    *length += padding_length;
}

// Функция для шифрования AES-CBC-256
String aes_encrypt(const String& inputText) {
    // Преобразуем входной текст в байтовый массив
    size_t inputLength = inputText.length();
    uint8_t* bytesInput = (uint8_t*)malloc(inputLength + 16); // +16 для паддинга
    memcpy(bytesInput, inputText.c_str(), inputLength);

    // Добавляем паддинг PKCS7
    add_pkcs7_padding(bytesInput, &inputLength, 16);

    // Выделяем память для зашифрованных данных
    uint8_t* bytesEncrypted = (uint8_t*)malloc(inputLength);

    // Инициализация контекста AES
    mbedtls_aes_context aes;
    mbedtls_aes_init(&aes);
    mbedtls_aes_setkey_enc(&aes, aesKey, 256);

    // Создаем копию IV, так как функция требует неконстантный указатель
    uint8_t iv_copy[16];
    memcpy(iv_copy, aesIv, 16);

    // Шифрование
    if (mbedtls_aes_crypt_cbc(&aes, MBEDTLS_AES_ENCRYPT, inputLength, iv_copy, bytesInput, bytesEncrypted) != 0) {
        free(bytesInput);
        free(bytesEncrypted);
        mbedtls_aes_free(&aes);
        return "AES encrypt error";
    }

    // Кодируем результат в base64
    size_t base64Length = base64::encodeLength(inputLength);
    char* base64EncodedOutput = (char*)malloc(base64Length);
    base64::encode((const unsigned char*)bytesEncrypted, inputLength, base64EncodedOutput);

    // Преобразуем результат в строку
    String encryptedText = String(base64EncodedOutput);

    // Очистка памяти
    mbedtls_aes_free(&aes);
    free(bytesInput);
    free(bytesEncrypted);
    free(base64EncodedOutput);

    return encryptedText;
}


size_t remove_pkcs7_padding(uint8_t* data, size_t length) {
    if (length == 0) return 0;
    uint8_t padding_value = data[length - 1];
    if (padding_value > 16) return length; // Некорректный паддинг
    return length - padding_value;
}


String aes_cbc_256_decrypt(const String& base64_ciphertext, const uint8_t* key, const uint8_t* iv) {
    // Декодируем base64
    size_t ciphertext_len = base64_ciphertext.length();
    size_t decoded_len;
    uint8_t* ciphertext = (uint8_t*)malloc(ciphertext_len);
    if (mbedtls_base64_decode(ciphertext, ciphertext_len, &decoded_len, (const unsigned char*)base64_ciphertext.c_str(), ciphertext_len) != 0) {
        free(ciphertext);
        return "Base64 decode error";
    }

    // Инициализация контекста AES
    mbedtls_aes_context aes;
    mbedtls_aes_init(&aes);
    mbedtls_aes_setkey_dec(&aes, key, 256);

    // Создаем копию IV, так как функция требует неконстантный указатель
    uint8_t iv_copy[16];
    memcpy(iv_copy, iv, 16);

    // Расшифровка
    uint8_t* plaintext = (uint8_t*)malloc(decoded_len);
    if (mbedtls_aes_crypt_cbc(&aes, MBEDTLS_AES_DECRYPT, decoded_len, iv_copy, ciphertext, plaintext) != 0) {
        free(ciphertext);
        free(plaintext);
        mbedtls_aes_free(&aes);
        return "AES decrypt error";
    }

    // Удаляем паддинг PKCS7
    size_t plaintext_len = remove_pkcs7_padding(plaintext, decoded_len);

    // Кодируем расшифрованные данные в Base64
    size_t base64_encoded_len;
    uint8_t* base64_encoded = (uint8_t*)malloc(plaintext_len * 2); // Выделяем достаточно памяти
    if (mbedtls_base64_encode(base64_encoded, plaintext_len * 2, &base64_encoded_len, plaintext, plaintext_len) != 0) {
        free(ciphertext);
        free(plaintext);
        free(base64_encoded);
        mbedtls_aes_free(&aes);
        return "Base64 encode error";
    }

    // Преобразуем закодированные данные в строку
    String base64_encoded_str = String((char*)base64_encoded, base64_encoded_len);

    // Очистка памяти
    mbedtls_aes_free(&aes);
    free(ciphertext);
    free(plaintext);
    free(base64_encoded);

    return base64_encoded_str;
}


int encryptControl = 0;
int keyPartIndex = 0; // Индекс текущей части ключа
String keyParts[7];   // Массив для хранения частей ключа

void loop() {
  if (Serial.available() > 0) {
    // Считываем строку из Serial
    String inputString = Serial.readString();
    inputString.trim();  // Удаляем лишние пробелы и символы новой строки

    if (inputString == "setControlRsaEncrypt") {
      encryptControl = 0;
      Serial.println("rsa");
    }

    if (inputString == "setRsaKey") {
      encryptControl = 10;
      keyPartIndex = 0; // Сбрасываем индекс при начале ввода ключа
      Serial.println("key:0");
    }

    if (inputString == "setControlAesEncrypt") {
      encryptControl = 1;
      Serial.println("aesEnc");
    }

    if (inputString == "getId") {
      Serial.println(String(ID));
      Serial.println(pass);
    }

    if (inputString == "getAesKey") {
      // Генерация случайного ключа AES длиной 16 байт (128 бит)
      randomSeed(analogRead(0)); // Инициализация генератора случайных чисел

      // Генерация случайного ключа AES
      for (int i = 0; i < 32; i++) {
          aesKey[i] = random(255); // Генерация случайного байта (0-255)
      }

      // Генерация случайного вектора инициализации (IV)
      for (int i = 0; i < 16; i++) {
          aesIv[i] = random(255); // Генерация случайного байта (0-255)
      }

      // Кодирование ключа в Base64
      char base64Key[25]; // Буфер для хранения Base64-закодированного ключа (24 символа + нулевой терминатор)
      base64::encode(aesKey, 32, base64Key); // Кодирование в Base64
      String aesKeyBase64 = String(base64Key);

      // Кодирование IV в Base64
      char base64Iv[25]; // Буфер для хранения Base64-закодированного IV (24 символа + нулевой терминатор)
      base64::encode(aesIv, 16, base64Iv); // Кодирование в Base64
      String aesIvBase64 = String(base64Iv);

      // Формируем пакет
      String packet = "$ID:" + String(ID) + ",PASS-PHRASE:" + pass + ",AES-KEY:" + aesKeyBase64 + ",AES-IV:" + aesIvBase64;
      // Serial.println(packet);
      // Шифруем пакет с использованием RSA
      RSA_Encrypt(packet.c_str());
    }

    if (inputString == "setControlAesDecrypt") {
      encryptControl = 2;
      Serial.println("aesDec");
    }

    if (encryptControl == 10 && inputString != "setRsaKey") {
      // Сохраняем текущую часть ключа
      keyParts[keyPartIndex] = inputString;
      keyPartIndex++;

      // Если все части ключа получены, формируем полный ключ
      if (keyPartIndex == 7) {
        esp_pub_key_pem_test = keyParts[0] + keyParts[1] + keyParts[2] + keyParts[3] + keyParts[4] + keyParts[5] + keyParts[6];
        encryptControl = 0;
        Serial.println("RSA key set successfully!");
      } else {
        // Запрашиваем следующую часть ключа
        Serial.println("key:" + String(keyPartIndex));
      }
    }

    if (encryptControl == 0 && inputString != "getAesKey" && inputString != "getId") {
      RSA_Encrypt(inputString.c_str());
    }

    if (encryptControl == 1 && inputString != "getAesKey" && inputString != "getId" && inputString != "setControlAesEncrypt") {
      // Шифруем строку
      String encryptedText = aes_encrypt(inputString);
      Serial.println(encryptedText);
    }

    if (encryptControl == 2 && inputString != "getAesKey" && inputString != "getId" && inputString != "setControlAesDecrypt") {
      // Расшифровываем строку
      String decryptedText = aes_cbc_256_decrypt(inputString,aesKey,aesIv);
      Serial.println(decryptedText);
    }
  }
}

void setup() {
  Serial.begin(115200);
  randomSeed(analogRead(0)); // Инициализация генератора случайных чисел
}