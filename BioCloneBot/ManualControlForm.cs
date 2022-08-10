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
        Platform platform;
        double xLocation;
        double yLocation;
        double zLocation;

        public ManualControlForm(SerialPort serialPort, Platform platform)
        {
            InitializeComponent();
            controlButtons = new Button[6];
            this.platform = platform;
            controlButtons[0] = xPlusButton;
            controlButtons[1] = xMinusButton;
            controlButtons[2] = yPlusButton;
            controlButtons[3] = yMinusButton;
            controlButtons[4] = zPlusButton;
            controlButtons[5] = zMinusButton;
            this.serialPort = serialPort;
            xLocation = platform.XLocation;
            yLocation = platform.YLocation;
            zLocation = platform.ZLocation;
            inputDistanceTextBox.Clear();
            MessageBox.Show("Manual Control is intended for debugging and troubleshooting ONLY. " +
                "Make sure to home the device before starting an experiment.", "Warning: Manual Control Debugging", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (xLocation > 0 && yLocation > 0 && zLocation > 0)
            {
                xLocationTextBox.Text = "x: " + convertToTwoDecimalDouble(xLocation);
                yLocationTextBox.Text = "y: " + convertToTwoDecimalDouble(yLocation);
                zLocationTextBox.Text = "z: " + convertToTwoDecimalDouble(zLocation);
            }
            else
            {
                xLocationTextBox.Text = "x: " + xLocation;
                yLocationTextBox.Text = "y: " + yLocation;
                zLocationTextBox.Text = "z: " + zLocation;
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
                    if (rightSide.Length < 2)
                    {
                        rightSide += conversion[i];
                    }
                    else
                    {
                        break;
                    }
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

        private string convertToThreeDigitInt(int input)
        {
            string conversion = input.ToString();
            if (conversion.Length == 1)
            {
                conversion = "00" + conversion;
            }
            else if (conversion.Length == 2)
            {
                conversion = "0" + conversion;
            }
            else if (conversion.Length == 3)
            {
                conversion = conversion;
            }
            return conversion;
        }
        private void moveAxis_onClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            double input = 0.0;
            string command = "#0001";
            string direction = "";
            string distance = "";
            byte[] messageCharacter = new byte[1];

            try
            {
                input = Convert.ToDouble(inputDistanceTextBox.Text);

                if (button.Text == "x+")
                {
                    direction = "100";
                    distance = convertToTwoDecimalDouble(input) + "000.00" + "000.00" + "%";
                    xLocation += input;
                    xLocationTextBox.Text = "x: " + convertToTwoDecimalDouble(xLocation);
                }

                else if (button.Text == "y+")
                {
                    direction = "010";
                    distance = "000.00" + convertToTwoDecimalDouble(input) + "000.00" + "%";
                    yLocation += input;
                    yLocationTextBox.Text = "y: " + convertToTwoDecimalDouble(yLocation);
                }
                else if (button.Text == "z+")
                {
                    direction = "001";
                    distance = "000.00" + "000.00" + convertToTwoDecimalDouble(input) + "%";
                    zLocation += input;
                    zLocationTextBox.Text = "z: " + convertToTwoDecimalDouble(zLocation);
                }
                else if (button.Text == "x-")
                {
                    direction = "000";
                    distance = convertToTwoDecimalDouble(input) + "000.00" + "000.00" + "%";
                    xLocation -= input;
                    xLocationTextBox.Text = "x: " + convertToTwoDecimalDouble(xLocation);
                }
                else if (button.Text == "y-")
                {
                    direction = "000";
                    distance = "000.00" + convertToTwoDecimalDouble(input) + "000.00" + "%";
                    yLocation -= input;
                    yLocationTextBox.Text = "y: " + convertToTwoDecimalDouble(yLocation);
                }
                else if (button.Text == "z-")
                {
                    direction = "000";
                    distance = "000.00" + "000.00" + convertToTwoDecimalDouble(input) + "%";
                    zLocation -= input;
                    zLocationTextBox.Text = "z: " + convertToTwoDecimalDouble(zLocation);
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
            catch (Exception ex)
            {
                MessageBox.Show("Input must be decimal number.", "Error: Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void aspirate_Click(object sender, EventArgs e)
        {
            double input = -1.0;
            string command = "#0011";
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
        private void ejectTipButton_Click(object sender, EventArgs e)
        {
            string command = "#0010%";
            byte[] messageCharacter = new byte[1];

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

        private void homeButton_Click(object sender, EventArgs e)
        {
            string command = "#0000%";
            byte[] messageCharacter = new byte[1];
            xLocation = 0.0;
            yLocation = 0.0;
            zLocation = platform.ZMax;

            try
            {
                if (serialPort.IsOpen)
                {
                    for (int i = 0; i < command.Length; i++)
                    {
                        messageCharacter[0] = Convert.ToByte(command[i]);
                        serialPort.Write(messageCharacter, 0, 1);
                    }
                    xLocationTextBox.Text = "x: " + convertToTwoDecimalDouble(xLocation);
                    yLocationTextBox.Text = "y: " + convertToTwoDecimalDouble(yLocation);
                    zLocationTextBox.Text = "z: " + convertToTwoDecimalDouble(zLocation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Serial Port failed to open.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mixButton_Click(object sender, EventArgs e)
        {
            string command = "#0101";
            int mixCount = 0;
            double mixVolume = 0.0;

            byte[] messageCharacter = new byte[1];

            if (inputDistanceTextBox.Text == "" || mixCountTextBox.Text == "" || !Double.TryParse(inputDistanceTextBox.Text, out _) || !int.TryParse(mixCountTextBox.Text, out _))
            {
                MessageBox.Show("Volume must be a decimal number and mix count must be an integer.", "Error: Invalid Volume", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                mixVolume = Convert.ToDouble(inputDistanceTextBox.Text);
                mixCount = Convert.ToInt32(mixCountTextBox.Text);

                command = command + convertToThreeDigitInt(mixCount) + convertToTwoDecimalDouble(mixVolume) + "%";
            }

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
    }
}
