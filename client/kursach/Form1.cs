using kursach;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
namespace kursach
{

    public partial class Form1 : Form
    {
        public SerialPort sp;
        TokenConnect token;
        ServerConnect server;

        public Form1(SerialPort sp)
        {
            this.sp = sp;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private async void connectTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new TokenCon().Show();
                sp = TokenCon.sp;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при подключении токена: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ServSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new ServSettings().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии настроек сервера: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void initToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                token = new TokenConnect(sp);
                listBox_Output.Items.Add(token.Connect());

                server = new ServerConnect(ServSettings.ip, ServSettings.port);
                listBox_Output.Items.Add(await server.ConnectAsync());

                string keyRsa = (await server.SendMessageAsync("rsa", true))[0];
                listBox_Output.Items.Add(keyRsa);

                if (keyRsa != null)
                {
                    await token.SetRsaKeyAsync(keyRsa);
                    listBox_Output.Items.Add("keySet");
                }

                await Task.Delay(1000); // Задержка для стабилизации

                string f = (await token.SendMessageAsync("getAesKey"))[0];
                listBox_Output.Items.Add(f);

                var serverResponse = await server.SendMessageAsync(f, false, true);
                listBox_Output.Items.AddRange(serverResponse.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void deletConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                await server.CloseAsync();
                await token.CloseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при закрытии соединений: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string mes = textBox2.Text;
                listBox_Output.Items.Add($"Отправлено: {mes}");

                await token.SetEncTipeAsync(true);
                mes = (await token.SendMessageAsync(mes))[0];

                if (mes != null)
                {
                    mes = mes.Trim();
                    string res = (await server.SendMessageAsync(mes))[0];
                    await token.SetEncTipeAsync(false);
                    listBox_Output.Items.Add($"Результат: {(await token.SendMessageAsync(res))[0]}");
                    textBox2.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отправке сообщения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)ConsoleKey.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}