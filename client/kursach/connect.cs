using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;

namespace kursach
{
    

    public class TokenConnect : Connection
    {
        public delegate void SM(string mess);
        public event SM OnSended;

        private SerialPort sp;

        public TokenConnect(SerialPort sp)
        {
            this.sp = sp;
        }
        public override string Connect()
        {
            try
            {

                sp.Open();
                return "Connection relise";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }
        public static string[] GetPortNames()
        {
            string[] ports = SerialPort.GetPortNames();
            return ports;
        }
        public async Task<List<string>> GetMainDataAsync()
        {
            try
            {
                List<string> info = new List<string>();
                info.AddRange(await FirstSendMessageAsync("getAesKey"));
                info.AddRange(await FirstSendMessageAsync("getId"));

                if (info[0] != "fail")
                {
                    string mes = $"ID:{info[2]},PASS-PHRASE:{info[3]},AES-KEY:{info[0]},AES-IV:{info[1]}";
                    return new List<string> { mes };
                }
                return new List<string> { "fail token data" };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string> { "fail" };
            }
        }

        public async Task SetEncTipeAsync(bool type)
        {
            try
            {
                if (type)
                {
                    await Task.Run(() => sp.Write("setControlAesEncrypt"));
                    await Task.Run(() => sp.ReadLine());
                }
                else
                {
                    await Task.Run(() => sp.Write("setControlAesDecrypt"));
                    await Task.Run(() => sp.ReadLine());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при установке типа шифрования: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public override async Task<string> ConnectAsync()
        {
            try
            {
                await Task.Run(() => sp.Open());
                return "Connection realized";
            }
            catch (Exception ex)
            {
                return $"Connection error: {ex.Message}";
            }
        }

        public async Task SetRsaKeyAsync(string key)
        {
            try
            {
                await Task.Run(() => sp.Write("setRsaKey"));
                await Task.Run(() => sp.ReadLine());

                int c = (key.Length / 64);
                for (int i = 0; i < c; i++)
                {
                    string spwr = key.Remove(64);
                    key = key.Substring(64);

                    await Task.Run(() => sp.WriteLine(spwr));
                    await Task.Run(() => sp.ReadLine());

                    if (i + 1 == c)
                    {
                        await Task.Run(() => sp.WriteLine(key));
                        await Task.Run(() => sp.ReadLine());
                    }
                }
                await Task.Run(() => sp.ReadLine());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при установке RSA ключа: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task<List<string>> FirstSendMessageAsync(string mess)
        {
            try
            {
                List<string> list = new List<string>();
                await Task.Run(() => sp.Write(mess));
                OnSended?.Invoke(mess);

                string res = await Task.Run(() => sp.ReadLine());
                list.Add(res);
                await Task.Delay(500);
                res = await Task.Run(() => sp.ReadLine());
                list.Add(res);

                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке сообщения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string> { "fail" };
            }
        }

        public override async Task<List<string>> SendMessageAsync(string mess, bool bas = false, bool t = false)
        {
            try
            {
                List<string> list = new List<string>();
                await Task.Run(() => sp.Write(mess));
                OnSended?.Invoke(mess);

                string res = await Task.Run(() => sp.ReadLine());
                list.Add(res);

                return list;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке сообщения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string> { "fail" };
            }
        }

        public override async Task CloseAsync()
        {
            try
            {
                await Task.Run(() => sp.Close());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при закрытии соединения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class ServerConnect : Connection
    {
        private Socket sender;
        private IPEndPoint remoteEP;
        public override string Connect()
        {
            throw new NotImplementedException();
        }
        public ServerConnect(byte[] ipServ, int port)
        {
            remoteEP = new IPEndPoint(new IPAddress(ipServ), port);
            sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public override async Task<string> ConnectAsync()
        {
            try
            {
                await Task.Run(() => sender.Connect(remoteEP));
                return $"Socket connected to {sender.RemoteEndPoint}";
            }
            catch (Exception ex)
            {
                return $"Connection error: {ex.Message}";
            }
        }

        public override async Task<List<string>> SendMessageAsync(string mess, bool IsBase64 = false, bool sendtype = false)
        {
            try
            {
                byte[] msg = sendtype ? Convert.FromBase64String(mess) : Encoding.UTF8.GetBytes(mess);
                await Task.Run(() => sender.Send(msg));

                byte[] bytes = new byte[4096];
                int bytesRec = await Task.Run(() => sender.Receive(bytes));
                string recive = IsBase64 ? Convert.ToBase64String(bytes, 0, bytesRec) : Encoding.UTF8.GetString(bytes, 0, bytesRec);

                return new List<string> { recive };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке сообщения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string> { "fail" };
            }
        }

        public override async Task CloseAsync()
        {
            try
            {
                await Task.Run(() => sender.Shutdown(SocketShutdown.Both));
                await Task.Run(() => sender.Close());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при закрытии соединения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public abstract class Connection
    {
        public abstract string Connect();
        public abstract Task<string> ConnectAsync();
        public abstract Task<List<string>> SendMessageAsync(string mess, bool bas = false, bool type = false);
        public abstract Task CloseAsync();
    }
}