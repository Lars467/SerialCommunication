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
using System.Windows.Forms.VisualStyles;

namespace SerialCommunication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();
                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;

                comboBoxBaudrate.SelectedIndex = comboBoxBaudrate.Items.IndexOf("115200");
            }
            catch (Exception)
            { }
        }

        private void cboPoort_DropDown(object sender, EventArgs e)
        {
            try
            {
                string selected = (string)comboBoxPoort.SelectedItem;
                string[] portNames = SerialPort.GetPortNames().Distinct().ToArray();

                comboBoxPoort.Items.Clear();
                comboBoxPoort.Items.AddRange(portNames);

                comboBoxPoort.SelectedIndex = comboBoxPoort.Items.IndexOf(selected);
            }
            catch (Exception)
            {
                if (comboBoxPoort.Items.Count > 0) comboBoxPoort.SelectedIndex = 0;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
              if (serialPortArduino.IsOpen)
                {
                    // er is verbinding --> verbreken
                    serialPortArduino.Close();
                    radioButtonVerbonden.Enabled = false;
                    buttonConnect.Text = "Connect";
                    labelStatus.Text = "Status: Disconnected";
                }
              else
                {
                    // er is geen verbinding --> maken
                    serialPortArduino.PortName = (string) comboBoxPoort.SelectedItem;
                    serialPortArduino.BaudRate = Int32.Parse((string) comboBoxBaudrate.SelectedItem);
                    serialPortArduino.DataBits = (int) numericUpDownDatabits.Value;

                    if (radioButtonParityEven.Checked) serialPortArduino.Parity = Parity.Even;
                    else if (radioButtonParityOdd.Checked) serialPortArduino.Parity = Parity.Odd;
                    else if (radioButtonParityNone.Checked) serialPortArduino.Parity = Parity.None;
                    else if (radioButtonParityMark.Checked) serialPortArduino.Parity = Parity.Mark;
                    else if (radioButtonParitySpace.Checked) serialPortArduino.Parity = Parity.Space;

                    if (radioButtonStopbitsNone.Checked) serialPortArduino.StopBits = StopBits.None;
                    else if (radioButtonStopbitsOne.Checked) serialPortArduino.StopBits = StopBits.One;
                    else if (radioButtonStopbitsOnePointFive.Checked) serialPortArduino.StopBits = StopBits.OnePointFive;
                    else if (radioButtonStopbitsTwo.Checked) serialPortArduino.StopBits = StopBits.Two;

                    if (radioButtonHandshakeNone.Checked) serialPortArduino.Handshake = Handshake.None;
                    else if (radioButtonHandshakeRTS.Checked) serialPortArduino.Handshake = Handshake.RequestToSend;
                    else if (radioButtonHandshakeRTSXonXoff.Checked) serialPortArduino.Handshake = Handshake.RequestToSendXOnXOff;
                    else if (radioButtonHandshakeXonXoff.Checked) serialPortArduino.Handshake = Handshake.XOnXOff;

                    serialPortArduino.RtsEnable = checkBoxRtsEnable.Checked;
                    serialPortArduino.DtrEnable = checkBoxDtrEnable.Checked;

                    serialPortArduino.Open();
                    string commando = "ping";
                    serialPortArduino.WriteLine(commando);
                    string antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();

                    if (antwoord == "pong")
                    {
                        radioButtonVerbonden.Checked = true;
                        buttonConnect.Text = "Disconnect";
                        labelStatus.Text = "Status: Connected";
                    }
                    else
                    {
                        serialPortArduino.Close();
                        labelStatus.Text = "ERROR!: verkeerd antwoord"; 
                    }
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "ERROR!: " + exception.Message;
                serialPortArduino.Close();
                buttonConnect.Text = "Connect";
                radioButtonVerbonden.Enabled = false;
                labelStatus.Text = "Status: Disconnected";
            }
        }

        private void checkBoxDigital2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;  //setd2 H/L
                    if (checkBoxDigital2.Checked) commando = "set d2 high";
                    else commando = "set d2 low";
                    serialPortArduino.WriteLine(commando);
                }
            }
            catch (Exception exception) 
            {
                labelStatus.Text = "ERROR!: " + exception.Message;
                serialPortArduino.Close();
                buttonConnect.Text = "Connect";
                radioButtonVerbonden.Enabled = false;
                labelStatus.Text = "Status: Disconnected";
            }
        }

        private void checkBoxDigital3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;  //setd3 H/L
                    if (checkBoxDigital3.Checked) commando = "set d3 high";
                    else commando = "set d3 low";
                    serialPortArduino.WriteLine(commando);
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "ERROR!: " + exception.Message;
                serialPortArduino.Close();
                buttonConnect.Text = "Connect";
                radioButtonVerbonden.Enabled = false;
                labelStatus.Text = "Status: Disconnected";
            }
        }

        private void checkBoxDigital4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;  //setd4 H/L
                    if (checkBoxDigital4.Checked) commando = "set d4 high";
                    else commando = "set d4 low";
                    serialPortArduino.WriteLine(commando);
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "ERROR!: " + exception.Message;
                serialPortArduino.Close();
                buttonConnect.Text = "Connect";
                radioButtonVerbonden.Enabled = false;
                labelStatus.Text = "Status: Disconnected";
            }
        }

        private void trackBarPWM9_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;  //set pwm9 to value 
                    int value = trackBarPWM9.Value;
                    commando = "set pwm9 " + value.ToString();
                    serialPortArduino.WriteLine(commando);
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "ERROR!: " + exception.Message;
                serialPortArduino.Close();
                buttonConnect.Text = "Connect";
                radioButtonVerbonden.Enabled = false;
                labelStatus.Text = "Status: Disconnected";
            }
        }

        private void trackBarPWM10_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;  //set pwm10 to value 
                    int value = trackBarPWM10.Value;
                    commando = "set pwm10 " + value.ToString();
                    serialPortArduino.WriteLine(commando);
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "ERROR!: " + exception.Message;
                serialPortArduino.Close();
                buttonConnect.Text = "Connect";
                radioButtonVerbonden.Enabled = false;
                labelStatus.Text = "Status: Disconnected";
            }
        }

        private void trackBarPWM11_Scroll(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    string commando;  //set pwm11 to value 
                    int value = trackBarPWM11.Value;
                    commando = "set pwm11 " + value.ToString();
                    serialPortArduino.WriteLine(commando);
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "ERROR!: " + exception.Message;
                serialPortArduino.Close();
                buttonConnect.Text = "Connect";
                radioButtonVerbonden.Enabled = false;
                labelStatus.Text = "Status: Disconnected";
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            timerOefening3.Enabled = tabControl.SelectedIndex == 3;
            timerOefening5.Enabled = tabControl.SelectedIndex == 5;

        }

        private void timerOefening3_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    serialPortArduino.ReadExisting();
                    string commando = "get d5";
                    serialPortArduino.WriteLine(commando);
                    string antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital5.Checked = (antwoord == "1");

                    commando = "get d6";
                    serialPortArduino.WriteLine(commando);
                    antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital6.Checked = (antwoord == "1");

                    commando = "get d7";
                    serialPortArduino.WriteLine(commando);
                    antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    radioButtonDigital7.Checked = (antwoord == "1");

                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "ERROR!: " + exception.Message;
                serialPortArduino.Close();
                buttonConnect.Text = "Connect";
                radioButtonVerbonden.Enabled = false;
                labelStatus.Text = "Status: Disconnected";
            }
        }

        private void timerOefening5_Tick(object sender, EventArgs e)
        {
            try
            {
                if (serialPortArduino.IsOpen)
                {
                    serialPortArduino.ReadExisting();
                    string commando = "get a0";
                    serialPortArduino.WriteLine(commando);
                    string antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    int ingesteldV = Int32.Parse(antwoord);

                    commando = "get a1";
                    serialPortArduino.WriteLine(commando);
                    antwoord = serialPortArduino.ReadLine();
                    antwoord = antwoord.TrimEnd();
                    antwoord = antwoord.Substring(4);
                    int werkelijkV = Int32.Parse(antwoord);

                    double ingesteldT = (double)((double)40 / (double)1023) * (double)ingesteldV + 5;
                    double werkelijkT = (double)((double)500 / (double)1023) * (double)werkelijkV;

                    labelGewensteTemp.Text = ingesteldT.ToString("00.0 °C");
                    labelHuidigeTemp.Text = werkelijkT.ToString("00.0 °C");

                    if (ingesteldT > werkelijkT)
                    {
                        serialPortArduino.ReadExisting();
                        commando = "set d2 1";
                        serialPortArduino.WriteLine(commando);
                    }
                    else
                    {
                        serialPortArduino.ReadExisting();
                        commando = "set d2 0";
                        serialPortArduino.WriteLine(commando);
                    }
                }
            }
            catch (Exception exception)
            {
                labelStatus.Text = "ERROR!: " + exception.Message;
                serialPortArduino.Close();
                buttonConnect.Text = "Connect";
                radioButtonVerbonden.Enabled = false;
                labelStatus.Text = "Status: Disconnected";
            }
        }
    }
}
