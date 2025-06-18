using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kursach
{
    public partial class ServSettings : Form
    {
        public static byte[] ip = new byte[4];
        public static int port;
        public ServSettings()
        {
            InitializeComponent();
        }

        private void labelPorts_Click(object sender, EventArgs e)
        {

        }

        private byte[] BytesParse()
        {
            byte[] bytes = new byte[4];
            if (textBox1.Text.Trim().Length < 16 && textBox1.Text.Trim().Length > 7)
            {
                int b = 0;
                string str = textBox1.Text;
                string byt = "";
                int k = 0;
                
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] != '.' && k < 4)
                    {
                        k += 1;
                        byt += str[i];
                    }
                    else if (str[i] == '.')
                    {
                        bytes[b] = (byte)(int.Parse(byt));
                        b += 1;
                        k = 0;
                        byt = "";

                    }
                }
                bytes[3] = (byte)(int.Parse(byt));
                return bytes;
            }
            else { 
                MessageBox.Show("вы ввели не правильный ip"); 
                return bytes; 
            }
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ip = BytesParse();
            port = int.Parse(textBox2.Text);

            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < (int)ConsoleKey.D0 && e.KeyChar > (int)ConsoleKey.D9) && e.KeyChar == (int)ConsoleKey.Backspace )
            {
                e.Handled = true;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < (int)ConsoleKey.D0 || e.KeyChar > (int)ConsoleKey.D9) && e.KeyChar != (int)ConsoleKey.Backspace && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }
    }
}
