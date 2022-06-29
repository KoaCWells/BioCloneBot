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

namespace BioCloneBot
{
    public partial class ManualControlForm : Form
    {
        Button[] controlButtons;
        SerialPort serialPort;

        public ManualControlForm(SerialPort serialPort)
        {
            InitializeComponent();
            controlButtons = new Button[6];
            controlButtons[0] = xPlusButton;
            controlButtons[1] = xMinusButton;
            controlButtons[2] = yPlusButton;
            controlButtons[3] = yMinusButton;
            controlButtons[4] = zPlusButton;
            controlButtons[5] = zMinusButton;
            this.serialPort = serialPort;
            MessageBox.Show("Manual Control is intended for debugging and troubleshooting ONLY. " +
                "Make sure to home the device before starting an experiment.", "Warning: Manual Control Debugging", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void moveAxis_onClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            double input = -1.0;
            string command = "#0001";
            string direction = "";
            string distance = "";
            byte[] messageCharacter = new byte[1];

            try
            {
                input = Convert.ToDouble(inputDistanceTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Volume must be a decimal number.", "Error: Invalid Volume Entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (button.Text == "x+")
            {
                direction = "100";
                distance = convertToTwoDecimalDouble(input) + "000.00" + "000.00" + "%";
            }

            else if (button.Text == "y+")
            {
                direction = "010";
                distance = "000.00" + convertToTwoDecimalDouble(input) + "000.00" + "%";
            }
            else if (button.Text == "z+")
            {
                direction = "001";
                distance = "000.00" + "000.00" + convertToTwoDecimalDouble(input) + "%";
            }
            else if (button.Text == "x-")
            {
                direction = "000";
                distance = convertToTwoDecimalDouble(input) + "000.00" + "000.00" + "%";
            }
            else if (button.Text == "y-")
            {
                direction = "000";
                distance = "000.00" + convertToTwoDecimalDouble(input) + "000.00" + "%";
            }
            else if (button.Text == "z-")
            {
                direction = "000";
                distance = "000.00" + "000.00" + convertToTwoDecimalDouble(input) + "%";
            }

            command += direction + distance;

            try
            {
                if (serialPort.IsOpen)
                {
                    for (int i = 0; i < command.Length; i++)
                    {
                        messageCharacter[0] = Convert.ToByte(command[i]);
                        serialPort.Write(messageCharacter, 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Serial Port failed to open.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void aspirate_Click(object sender, EventArgs e)
        {
            double input = -1.0;
            string command = "#0011";
            string volume = "";
            byte[] messageCharacter = new byte[1];

            try
            {
                input = Convert.ToDouble(inputDistanceTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Volume must be a decimal number.", "Error: Invalid Volume Entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            command += convertToTwoDecimalDouble(input) + "%";

            try
            {
                if (serialPort.IsOpen)
                {
                    for (int i = 0; i < command.Length; i++)
                    {
                        messageCharacter[0] = Convert.ToByte(command[i]);
                        serialPort.Write(messageCharacter, 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Serial Port failed to open.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dispense_Click(object sender, EventArgs e)
        {
            double input = -1.0;
            string command = "#0100";
            string direction = "";
            string volume = "";
            byte[] messageCharacter = new byte[1];

            try
            {
                input = Convert.ToDouble(inputDistanceTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Volume must be a decimal number.", "Error: Invalid Volume Entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            command += convertToTwoDecimalDouble(input) + "%";

            try
            {
                if (serialPort.IsOpen)
                {
                    for (int i = 0; i < command.Length; i++)
                    {
                        messageCharacter[0] = Convert.ToByte(command[i]);
                        serialPort.Write(messageCharacter, 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Serial Port failed to open.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string convertToTwoDecimalDouble(double input)
        {
            string conversion = input.ToString();
            string leftSide = "";
            string rightSide = "";
            char character = ' ';
            int decimalLocation = 0;
            bool passedDecimal = false;

            for (int i = 0; i < conversion.Length; i++)
            {
                character = conversion[i];
                if (character == '.')
                {
                    decimalLocation = i;
                    passedDecimal = true;
                }
                else if (character != '.' && passedDecimal == false)
                {
                    leftSide += conversion[i];
                }
                else if (passedDecimal == true)
                {
                    rightSide += conversion[i];
                }
            }

            if (leftSide.Length == 1)
            {
                leftSide = "00" + leftSide;
            }
            else if (leftSide.Length == 2)
            {
                leftSide = "0" + leftSide;
            }

            if (rightSide.Length == 0)
            {
                rightSide += "00";
            }
            else if (rightSide.Length == 1)
            {
                rightSide += "0";
            }

            conversion = leftSide + "." + rightSide;
            return conversion;
        }
    }
}
