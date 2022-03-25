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

namespace BioCloneBot
{
    public partial class Form1 : Form
    {
        //During Form1 initilization, goes through each available serial port one by one sending the "ping" message. If "pong" is received back, the Arduino has been found then
        //opens a serial connection with the Arduino. If not found, displays an error message to try again.
        public Form1()
        {
            InitializeComponent();
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            string response = "";
            bool handshake = false;
            int i = 0;

            while (!handshake)
            {
                if (i == ports.Length)
                {
                    i = 0;
                    DialogResult msg = MessageBox.Show("Arduino not found.", " Error: Device Not Found", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    if (msg == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                }
                serialPort1.PortName = ports[i];

                try
                {
                    serialPort1.Open();
                    serialPort1.BaudRate = 9600;
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.Message);
                }

                if (serialPort1.IsOpen)
                {
                    serialPort1.DiscardInBuffer();
                    serialPort1.Write("ping%");
                    System.Threading.Thread.Sleep(50);
                    response = serialPort1.ReadExisting();
                    Console.WriteLine(response);

                    if (response == "pong")
                    {
                        //MessageBox.Show("Arduino connection established.");
                        handshake = true;
                    }
                    else
                    {
                        serialPort1.Close();
                    }
                }
                i++;
            }
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write(sendText.Text);
                    sendText.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void recBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    recText.Text = serialPort1.ReadTo("%");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void sendText_TextChanged(object sender, EventArgs e)
        {

        }

        private void recText_TextChanged(object sender, EventArgs e)
        {

        }

        private void homeDevice()
        {
            //TODO
            //Once handshake with arduino has been made
            //Display message indicating to clear the board
            //Explain homing procedure
            //button to start homing procedures
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void aspirate_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write("0" + aspirateVolume.Text + "%");
                    aspirateVolume.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dispense_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write("1" + dispenseVolume.Text + "%");
                    dispenseVolume.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void home_Text(object sender, EventArgs e)
        {

        }

        private void home_Button(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write("0000%");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void close_Serial_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                DialogResult msg = MessageBox.Show("Serial port closed.", "Serial port closed.", MessageBoxButtons.OK);
            }
        }

        private void open_Serial_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
                DialogResult msg = MessageBox.Show("Serial port opened.", "Serial port opened.", MessageBoxButtons.OK);
            }
        }
    }
}
