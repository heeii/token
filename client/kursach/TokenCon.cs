using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace kursach
{
    public partial class TokenCon : Form
    {
        public static SerialPort sp = new SerialPort();
        public TokenCon()
        {

            InitializeComponent();

            comboBoxParity.Items.Add("Нет устройств");
            comboBoxDataBits.Items.Add("Нет устройств");
            comboBoxStopBits.Items.Add("Нет устройств");
            comboBoxHandshake.Items.Add("Нет устройств");

            comboBoxParity.SelectedIndex = comboBoxDataBits.SelectedIndex =
            comboBoxStopBits.SelectedIndex = comboBoxHandshake.SelectedIndex = 0;
            if (TokenConnect.GetPortNames().Length != comboBoxPorts.Items.Count)
            {
                if (!comboBoxPorts.Enabled)
                    comboBoxPorts.Enabled = false;
                comboBoxPorts.Text = "";
                comboBoxPorts.Items.Clear();
                comboBoxPorts.Items.AddRange(SerialPort.GetPortNames());
                if (comboBoxPorts.Items.Count > 0)
                    comboBoxPorts.SelectedIndex = 0;
                comboBoxParity.Items.Clear();
                comboBoxDataBits.Items.Clear();
                comboBoxStopBits.Items.Clear();
                comboBoxHandshake.Items.Clear();

                comboBoxParity.Items.AddRange(new string[]
                { "None - без контроля четности",
                    "Odd - всегда нечет.",
                    "Even - всегда чет.",
                    "Mark - всегда 1",
                    "Space - всегда 0"
                });
                comboBoxDataBits.Items.AddRange(new string[] { "5", "6", "7", "8" });
                comboBoxStopBits.Items.AddRange(new string[] { "1", "2", "1.5" });
                comboBoxHandshake.Items.AddRange(new string[] { "None - без протокола", "XOnXOff", "RTS", "RTS + XOnXOff" });

                comboBoxPorts.SelectedIndex = 0;
                textBoxBaudsRate.Text = "115200";
                comboBoxParity.SelectedIndex = 0;
                comboBoxDataBits.SelectedIndex = 3;
                comboBoxStopBits.SelectedIndex = 0;
                comboBoxHandshake.SelectedIndex = 0;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sp.PortName = comboBoxPorts.Text;
            sp.BaudRate = int.Parse(textBoxBaudsRate.Text);
            sp.Parity = (Parity)comboBoxParity.SelectedIndex;
            sp.DataBits = Convert.ToInt32(comboBoxDataBits.SelectedItem);
            sp.StopBits = (StopBits)comboBoxStopBits.SelectedIndex + 1;
            sp.Handshake = (Handshake)comboBoxHandshake.SelectedIndex;
            this.Close();
        }

        private void labelPorts_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxParity_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxHandshake_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
