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
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BioCloneBot
{
    public partial class ControlForm : Form
    {
        private int labwareCount;
        private bool[] labwareSelected;
        private bool[] aspirateOperation;
        private bool[] dispenseOperation;
        private bool[] getTipOperation;
        private bool[] mixOperation;
        private Platform platform;
        private Button[] labwareButtons;
        private SerialPort serialPort;
        private Color formBackgroundColor = Color.FromArgb(33, 33, 33);
        private Color buttonSelectedColor = Color.FromArgb(24, 225, 204);
        private Color buttonBackgroundColor1 = Color.FromArgb(194, 148, 255);
        private Color buttonBackgroundColor2 = Color.FromArgb(18, 18, 20);
        private Color buttonTextColor1 = Color.FromArgb(18, 18, 20);
        private Color buttonTextColor2 = Color.FromArgb(194, 148, 255);
        private Color buttonOperationsColor = Color.FromArgb(177, 255, 154);

        public ControlForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            labwareCount = 4;
            platform = new Platform(labwareCount);
            labwareButtons = new Button[labwareCount];
            serialPort = new SerialPort();
            labwareSelected = new bool[labwareCount];
            aspirateOperation = new bool[labwareCount];
            dispenseOperation = new bool[labwareCount];
            getTipOperation = new bool[labwareCount];
            mixOperation = new bool[labwareCount];

            for (int i = 0; i < labwareCount; i++)
            {
                labwareSelected[i] = false;
                aspirateOperation[i] = false;
                dispenseOperation[i] = false;
                getTipOperation[i] = false;
                mixOperation[i] = false;
            }

            labwareMenuStrip = new ContextMenuStrip();
            labwareButtons[0] = labwareButton1;
            labwareButtons[1] = labwareButton2;
            labwareButtons[2] = labwareButton3;
            labwareButtons[3] = labwareButton4;

            initialize_Serial_Port();
        }

        //loops through all available serial ports, send "#ping%" message and waits for "#pong%" response
        //once response is received, the connection is established
        private void initialize_Serial_Port()
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            string connectionMessage = "#ping%";
            string response = "";
            char receivedCharacter = ' ';
            bool handshake = false;
            bool messageComplete = false;
            int i = 0;
            byte[] messageCharacter = new byte[1];

            while (!handshake)
            {
                if (i == ports.Length && i != 0)
                {
                    i = 0;

                    DialogResult msg = MessageBox.Show("BioCloneBot not found.", " Error: Device Not Found", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    if (msg == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                }

                if (ports.Length > 0)
                {
                    serialPort.PortName = ports[i];

                    try
                    {
                        serialPort.Open();
                        serialPort.ReadTimeout = 500;
                        serialPort.BaudRate = 9600;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    if (serialPort.IsOpen)
                    {
                        for (int j = 0; j < connectionMessage.Length; j++)
                        {
                            messageCharacter[0] = Convert.ToByte(connectionMessage[j]);
                            serialPort.Write(messageCharacter, 0, 1);
                        }

                        try
                        {
                            receivedCharacter = Convert.ToChar(serialPort.ReadByte());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        if (receivedCharacter == '#')
                        {
                            while (messageComplete == false)
                            {
                                try
                                {
                                    receivedCharacter = Convert.ToChar(serialPort.ReadByte());
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }

                                if (receivedCharacter != '%')
                                {
                                    response += receivedCharacter;
                                }
                                else
                                {
                                    if (response == "pong")
                                    {
                                        messageComplete = true;
                                        handshake = true;
                                    }
                                }
                            }
                        }

                        if (messageComplete == true && handshake == true)
                        {
                            break;
                        }
                    }
                    serialPort.Close();
                    i++;
                }
                else
                {
                    DialogResult msg = MessageBox.Show("BioCloneBot not found. Are you sure it is connected?", " Error: Device Not Found", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    ports = System.IO.Ports.SerialPort.GetPortNames();
                    if (msg == DialogResult.Cancel)
                    {
                        Environment.Exit(0);
                    }
                }
            }
        }
        private void updateCommand_List()
        {
            if (platform.ProtocolList.Count > 0)
            {
                commandListTextBox.Clear();
                for (int i = 0; i < platform.ProtocolList.Count(); i++)
                {
                    commandListTextBox.Text += (i + 1) + ". " + platform.ProtocolList[i] + ".\r\n";
                }
            }
        }

        private double[] calculateTravelDistance(int position, int[] wellLocation)
        {
            double[] destination = new double[3];
            double elementSeparation = platform.Labwares[position].ReservoirSeparation;
            double[] topLeftCorner = platform.Labwares[position].TopLeftCorner;
            double[] startLocation = platform.Labwares[position].StartLocation;

            destination[0] = topLeftCorner[0] + startLocation[0] + elementSeparation * wellLocation[1];
            destination[1] = topLeftCorner[1] - startLocation[1] - elementSeparation * wellLocation[0];
            destination[2] = platform.ZMax - platform.Labwares[position].Dimensions[2];

            return destination;
        }
        private void getTip(int labwarePosition)
        {
            double[] destination = calculateTravelDistance(labwarePosition, platform.SelectedPosition);
            platform.Operations.Add(new Operation("gettip", platform.XLocation, platform.YLocation, platform.ZLocation, destination[0], destination[1], destination[2],
                labwarePosition, platform.SelectedPosition, platform.Labwares[labwarePosition]));

            platform.NumberOfOperations++;
            platform.XLocation = destination[0];
            platform.YLocation = destination[1];
            platform.ZLocation = 0.0;
            platform.ProtocolList.Add("Get Tip from " + platform.Labwares[labwarePosition].LabwareType + " in position " + (labwarePosition + 1));
            updateCommand_List();
        }
        private void removeTip()
        {
            platform.Operations.Add(new Operation("removetip", platform.XLocation, platform.YLocation, platform.ZLocation, platform.TrashLocation[0], platform.TrashLocation[1]));
            platform.TipAttached = false;
            platform.NumberOfOperations++;
            platform.XLocation = platform.TrashLocation[0];
            platform.YLocation = platform.TrashLocation[1];
            platform.ZLocation = 0.0;
            platform.ProtocolList.Add("Remove tip");
            updateCommand_List();
        }
        private void aspirateVolume(int labwarePosition, double volumeAspirated)
        {
            double[] destination = calculateTravelDistance(labwarePosition, platform.SelectedPosition);
            platform.Operations.Add(new Operation("aspirate", volumeAspirated, platform.XLocation, platform.YLocation, platform.ZLocation, destination[0], destination[1], destination[2],
                labwarePosition, platform.SelectedPosition, platform.Labwares[labwarePosition]));

            platform.NumberOfOperations++;
            platform.XLocation = destination[0];
            platform.YLocation = destination[1];
            platform.ZLocation = 0;
            platform.ProtocolList.Add("Aspirated " + volumeAspirated + "uL from " + platform.Labwares[labwarePosition].LabwareType + " in position " + (labwarePosition + 1));
            updateCommand_List();
        }
        private void dispenseVolume(int labwarePosition, double volumeDispensed)
        {

            double[] destination = calculateTravelDistance(labwarePosition, platform.SelectedPosition);
            platform.Operations.Add(new Operation("dispense", volumeDispensed, platform.XLocation, platform.YLocation, platform.ZLocation, destination[0], destination[1], destination[2],
                labwarePosition, platform.SelectedPosition, platform.Labwares[labwarePosition]));

            platform.NumberOfOperations++;
            platform.XLocation = destination[0];
            platform.YLocation = destination[1];
            platform.ZLocation = 0;
            platform.ProtocolList.Add("Dispensed " + volumeDispensed + "uL to " + platform.Labwares[labwarePosition].LabwareType + " in position " + (labwarePosition + 1));
            updateCommand_List();
        }

        private void mix(int labwarePosition, double volumeMixed, int mixCount)
        {
            double[] destination = calculateTravelDistance(labwarePosition, platform.SelectedPosition);
            platform.Operations.Add(new Operation("mix", volumeMixed, platform.XLocation, platform.YLocation, platform.ZLocation, destination[0], destination[1], destination[2],
                mixCount, labwarePosition, platform.SelectedPosition, platform.Labwares[labwarePosition]));

            platform.NumberOfOperations++;
            platform.XLocation = destination[0];
            platform.YLocation = destination[1];
            platform.ZLocation = 0;
            platform.ProtocolList.Add("Mixed " + volumeMixed + "uL " + mixCount + " times on " + platform.Labwares[labwarePosition].LabwareType + " in position " + (labwarePosition + 1));
            updateCommand_List();
        }
        /* BioCloneBot Control Buttons
         * 
         * 
         */
        //TODO: Add functionality for canceling experiment when the Arduino does not respond after a certain amount of time
        private void startExperimentButton_Click(object sender, EventArgs e)
        {
            string command = "";
            string response = "";
            char receivedCharacter = ' ';
            bool commandCompleted = false;
            byte[] messageCharacter = new byte[1];

            DialogResult msg = MessageBox.Show("Click OK to confirm and start the experiment", "Starting Experiment", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (msg == DialogResult.OK)
            {
                for (int i = 0; i < platform.Operations.Count; i++)
                {

                    for (int j = 0; j < platform.Operations[i].Steps.Count; j++)
                    {
                        try
                        {
                            if (serialPort.IsOpen)
                            {
                                commandCompleted = false;
                                response = "";
                                command = platform.Operations[i].Steps[j];
                                for (int k = 0; k < command.Length; k++)
                                {
                                    messageCharacter[0] = Convert.ToByte(command[k]);
                                    serialPort.Write(messageCharacter, 0, 1);
                                }
                                serialPort.DiscardOutBuffer();

                                while (commandCompleted == false)
                                {
                                    try
                                    {
                                        receivedCharacter = Convert.ToChar(serialPort.ReadByte());
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.Message);
                                    }

                                    if (receivedCharacter == '#')
                                    {
                                        while (commandCompleted == false)
                                        {
                                            receivedCharacter = Convert.ToChar(serialPort.ReadByte());
                                            if (receivedCharacter != '%')
                                            {
                                                response += receivedCharacter;
                                            }
                                            else
                                            {
                                                if (response == "complete")
                                                {
                                                    commandCompleted = true;
                                                    //MessageBox.Show("Press OK to start next command.", " Testing each command", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    serialPort.DiscardInBuffer();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                MessageBox.Show("BioCloneBot has finished running your experiment.", "Experiment Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void homeButton_Click(object sender, EventArgs e)
        {
            string homeMessage = "#0000%";
            byte[] messageCharacter = new byte[1];
            try
            {
                if (serialPort.IsOpen)
                {
                    MessageBox.Show("BioCloneBot will now start homing. Please make sure the platform is empty before continuining.", "Homing Device", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    platform.XLocation = 0.0;
                    platform.YLocation = 0.0;
                    platform.ZLocation = 0.0;
                    for (int i = 0; i < homeMessage.Length; i++)
                    {
                        messageCharacter[0] = Convert.ToByte(homeMessage[i]);
                        serialPort.Write(messageCharacter, 0, 1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveProtocolButton_Click(object sender, EventArgs e)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IgnoreReadOnlyFields = true };
            string jsonString = JsonSerializer.Serialize(platform, options);


            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.DefaultExt = "json";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, jsonString);
            }
        }

        private void loadProtocolButton_Click(object sender, EventArgs e)
        {
            string filePath = "";
            string jsonString = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    jsonString = File.ReadAllText(filePath);
                    JsonNode platformNode = JsonNode.Parse(jsonString)!;
                    JsonNode labwaresNode = platformNode!["Labwares"]!;
                    JsonNode labwareNode;
                    JsonNode operationsNode = platformNode!["Operations"]!;
                    JsonNode operationNode;

                    //load platform values
                    platform.NumberOfOperations = (int)platformNode!["NumberOfOperations"]!;
                    platform.VolumeInTip = (int)platformNode!["VolumeInTip"]!;
                    platform.TipCapacity = (int)platformNode!["TipCapacity"]!;
                    platform.XLocation = (double)platformNode!["XLocation"]!;
                    platform.YLocation = (double)platformNode!["YLocation"]!;
                    platform.ZLocation = (double)platformNode!["ZLocation"]!;
                    //ignore XMax
                    //ignore YMax
                    //ignore ZMax
                    //ignore TrashLocation
                    platform.SelectedPosition[0] = (int)platformNode!["SelectedPosition"]![0];
                    platform.SelectedPosition[1] = (int)platformNode!["SelectedPosition"]![1];
                    platform.TipAttached = (bool)platformNode!["TipAttached"]!;
                    for (int i = 0; i < platform.LabwareCount; i++)
                    {
                        platform.LabwareOccupied[i] = (bool)platformNode!["LabwareOccupied"]![i];
                    }

                    for (int i = 0; i < platform.NumberOfOperations; i++)
                    {
                        platform.ProtocolList.Add((string)platformNode!["ProtocolList"]![i]);
                    }
                    updateCommand_List();

                    //load labwares
                    for (int i = 0; i < platform.LabwareCount; i++)
                    {
                        labwareNode = labwaresNode[i];

                        if (platform.LabwareOccupied[i] == false)
                        {
                            platform.Labwares[i] = null;
                        }
                        else
                        {
                            if ((string)labwareNode!["LabwareType"] == "wellplate")
                            {
                                platform.AddLabware(i, "wellplate");
                                labwareButtons[i].Text = "96 Wellplate";
                                labwareButtons[i].BackColor = buttonSelectedColor;
                                labwareButtons[i].ForeColor = buttonTextColor1;
                                //ignore Row
                                //ignore Col
                                //ignore MaxVolume
                                //ignore ReservoirSeparation
                                //ignore Dimensions
                                //ignore TopLeftCorner
                                //ignore StartLocation
                                for (int row = 0; row < (int)labwareNode!["Row"]; row++)
                                {
                                    for (int col = 0; col < (int)labwareNode!["Col"]; col++)
                                    {
                                        platform.Labwares[i].Volumes[row][col] = (double)labwareNode!["Volumes"][row][col];
                                    }
                                }
                                //ignore Dimensions
                            }
                            else if ((string)labwareNode!["LabwareType"] == "tubestand")
                            {
                                platform.AddLabware(i, "tubestand");
                                labwareButtons[i].Text = "5mL Eppendorf Tubestand";
                                labwareButtons[i].BackColor = buttonSelectedColor;
                                labwareButtons[i].ForeColor = buttonTextColor1;
                                //ignore Row
                                //ignore Col
                                //ignore MaxVolume
                                //ignore ReservoirSeparation
                                //ignore Dimensions
                                //ignore TopLeftCorner
                                //ignore StartLocation
                                for (int row = 0; row < (int)labwareNode!["Row"]; row++)
                                {
                                    for (int col = 0; col < (int)labwareNode!["Col"]; col++)
                                    {
                                        platform.Labwares[i].Volumes[row][col] = (double)labwareNode!["Volumes"][row][col];
                                    }
                                }
                            }
                            else if ((string)labwareNode!["LabwareType"] == "tipbox")
                            {
                                platform.AddLabware(i, "tipbox");
                                labwareButtons[i].Text = "200 uL Tip Box";
                                labwareButtons[i].BackColor = buttonSelectedColor;
                                labwareButtons[i].ForeColor = buttonTextColor1;
                                //ignore Row
                                //ignore Col
                                //ignore MaxVolume
                                //ignore ReservoirSeparation
                                //ignore Dimensions
                                //ignore TopLeftCorner
                                //ignore StartLocation
                                for (int row = 0; row < (int)labwareNode!["Row"]; row++)
                                {
                                    for (int col = 0; col < (int)labwareNode!["Col"]; col++)
                                    {
                                        platform.Labwares[i].Volumes[row][col] = (double)labwareNode!["Volumes"][row][col];
                                    }
                                }
                            }
                        }
                    }

                    //load operations
                    for (int i = 0; i < (int)platformNode!["NumberOfOperations"]!; i++)
                    {
                        operationNode = operationsNode[i];

                        if ((string)operationNode!["Command"]! == "home")
                        {
                            platform.Operations.Add(new Operation("home"));
                        }
                        else if ((string)operationNode!["Command"]! == "gettip")
                        {
                            platform.Operations.Add(new Operation(
                                "gettip",
                                (double)operationNode!["XStart"]!,
                                (double)operationNode!["YStart"]!,
                                (double)operationNode!["ZStart"]!,
                                (double)operationNode!["XDest"]!,
                                (double)operationNode!["YDest"]!,
                                (double)operationNode!["ZDest"]!,
                                (int)operationNode!["LabwarePosition"]!,
                                new int[] { (int)operationNode!["SelectedReservoirPosition"][0], (int)operationNode!["SelectedReservoirPosition"][1]! },
                                platform.Labwares[(int)operationNode!["LabwarePosition"]!]));
                        }
                        else if ((string)operationNode!["Command"]! == "removetip")
                        {
                            platform.Operations.Add(new Operation(
                                "removetip",
                                (double)operationNode!["XStart"]!,
                                (double)operationNode!["YStart"]!,
                                (double)operationNode!["ZStart"]!,
                                (double)operationNode!["XDest"]!,
                                (double)operationNode!["YDest"]!));
                        }
                        else if ((string)operationNode!["Command"]! == "aspirate")
                        {
                            platform.Operations.Add(new Operation(
                                "aspirate",
                                (double)operationNode!["VolumeMoved"]!,
                                (double)operationNode!["XStart"]!,
                                (double)operationNode!["YStart"]!,
                                (double)operationNode!["ZStart"]!,
                                (double)operationNode!["XDest"]!,
                                (double)operationNode!["YDest"]!,
                                (double)operationNode!["ZDest"]!,
                                (int)operationNode!["LabwarePosition"]!,
                                new int[] { (int)operationNode!["SelectedReservoirPosition"][0], (int)operationNode!["SelectedReservoirPosition"][1]! },
                                platform.Labwares[(int)operationNode!["LabwarePosition"]!]));
                        }
                        else if ((string)operationNode!["Command"]! == "dispense")
                        {
                            platform.Operations.Add(new Operation(
                                "dispense",
                                (double)operationNode!["VolumeMoved"]!,
                                (double)operationNode!["XStart"]!,
                                (double)operationNode!["YStart"]!,
                                (double)operationNode!["ZStart"]!,
                                (double)operationNode!["XDest"]!,
                                (double)operationNode!["YDest"]!,
                                (double)operationNode!["ZDest"]!,
                                (int)operationNode!["LabwarePosition"]!,
                                new int[] { (int)operationNode!["SelectedReservoirPosition"][0], (int)operationNode!["SelectedReservoirPosition"][1]! },
                                platform.Labwares[(int)operationNode!["LabwarePosition"]!]));
                        }
                        else if ((string)operationNode!["Command"]! == "mix")
                        {
                            platform.Operations.Add(new Operation(
                                "mix",
                                (double)operationNode!["VolumeMixed"]!,
                                (double)operationNode!["XStart"]!,
                                (double)operationNode!["YStart"]!,
                                (double)operationNode!["ZStart"]!,
                                (double)operationNode!["XDest"]!,
                                (double)operationNode!["YDest"]!,
                                (double)operationNode!["ZDest"]!,
                                (int)operationNode!["MixCount"],
                                (int)operationNode!["LabwarePosition"]!,
                                new int[] { (int)operationNode!["SelectedReservoirPosition"][0], (int)operationNode!["SelectedReservoirPosition"][1]! },
                                platform.Labwares[(int)operationNode!["LabwarePosition"]!]));
                        }
                    }
                }
            }
        }
        private void loadSample_Click(object sender, EventArgs e)
        {
            double[][] wellplateVolume1 = new double[8][];
            for (int i = 0; i < 8; i++)
            {
                wellplateVolume1[i] = new double[12];
            }

            double[][] wellplateVolume2 = new double[8][];
            for (int i = 0; i < 8; i++)
            {
                wellplateVolume2[i] = new double[12];
            }

            double[][] tubestandVolume = new double[4][];
            for (int i = 0; i < 4; i++)
            {
                tubestandVolume[i] = new double[6];
            }

            double[][] tipboxTips = new double[8][];
            for (int i = 0; i < 8; i++)
            {
                tipboxTips[i] = new double[12];
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i < 4 && j < 6)
                    {
                        tubestandVolume[i][j] = 500.0;
                    }
                    wellplateVolume1[i][j] = 100.00;
                    wellplateVolume2[i][j] = 0.0;
                    tipboxTips[i][j] = 1.0;
                }
            }

            if (platform.NumberOfOperations == 0)
            {
                platform.AddLabware(0, "wellplate");
                platform.LabwareOccupied[0] = true;
                platform.Labwares[0].Volumes = wellplateVolume1;
                labwareButtons[0].Text = "96 Wellplate";
                labwareButtons[0].BackColor = buttonSelectedColor;
                labwareButtons[0].ForeColor = buttonTextColor1;

                platform.AddLabware(1, "tipbox");
                platform.LabwareOccupied[1] = true;
                platform.Labwares[1].Volumes = tipboxTips;
                labwareButtons[1].Text = "200uL Tip Box";
                labwareButtons[1].BackColor = buttonSelectedColor;
                labwareButtons[1].ForeColor = buttonTextColor1;

                platform.AddLabware(2, "tubestand");
                platform.LabwareOccupied[2] = true;
                platform.Labwares[2].Volumes = tubestandVolume;
                labwareButtons[2].Text = "5mL Eppendorf Tubes";
                labwareButtons[2].BackColor = buttonSelectedColor;
                labwareButtons[2].ForeColor = buttonTextColor1;

                platform.AddLabware(3, "wellplate");
                platform.LabwareOccupied[3] = true;
                platform.Labwares[3].Volumes = wellplateVolume2;
                labwareButtons[3].Text = "96 Wellplate";
                labwareButtons[3].BackColor = buttonSelectedColor;
                labwareButtons[3].ForeColor = buttonTextColor1;
            }

            else
            {
                MessageBox.Show("Sample experiments can only be loaded if there are no operations in the Protocol Queue.", "Error: Cannot Load Sample Experiment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void reconnect_Arduino_Click(object sender, EventArgs e)
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
            initialize_Serial_Port();
            MessageBox.Show("Connection established.", "Arduino Found", MessageBoxButtons.OK);
        }

        private void clearProtocolButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("WARNING: Clears all experiments. Click OK to remove ALL labware and protocols.", "WARNING: Clear Labware",
                MessageBoxButtons.OKCancel);

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                commandListTextBox.Clear();
                platform.ProtocolList.Clear();
                platform.Operations.Clear();
                platform.NumberOfOperations = 0;
                platform.TipAttached = false;
                for (int i = 0; i < labwareCount; i++)
                {
                    labwareSelected[i] = false;
                    aspirateOperation[i] = false;
                    dispenseOperation[i] = false;
                    getTipOperation[i] = false;
                    platform.Labwares[i] = null;
                    platform.LabwareOccupied[i] = false;
                    labwareButtons[i].BackColor = buttonBackgroundColor2;
                    labwareButtons[i].ForeColor = buttonTextColor2;
                    labwareButtons[i].Text = "Labware " + (i + 1);
                }
            }
        }
        /* Operations Buttons
         */
        private void homeDeviceOperationButton_Click(object sender, EventArgs e)
        {
            platform.Operations.Add(new Operation("home"));
            platform.ProtocolList.Add("Home Device");
            platform.NumberOfOperations++;
            platform.XLocation = 0.0;
            platform.YLocation = 0.0;
            platform.ZLocation = 0.0;

            updateCommand_List();
        }
        private void getTipOperationButton_Click(object sender, EventArgs e)
        {
            if (platform.TipAttached == false)
            {
                bool labwareAvailable = false;

                for (int i = 0; i < labwareCount; i++)
                {
                    if (platform.Labwares[i] != null && (platform.Labwares[i].LabwareType == "tipbox"))
                    {
                        labwareButtons[i].BackColor = buttonOperationsColor;
                        getTipOperation[i] = true;
                        labwareAvailable = true;
                    }
                }

                if (labwareAvailable == false)
                {
                    MessageBox.Show("Before you can attach a fresh tip, add a tipbox to the platform.", "Error: No Available Tipbox", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("Pipette tip already attached to the pump. Please add a 'Remove Tip' operation before adding a new tip.", "Error: Tip Already Attached", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void removeTipOperationButton_Click(object sender, EventArgs e)
        {

            if (platform.TipAttached == true)
            {
                removeTip();
            }
            else if (platform.TipAttached == false)
            {
                MessageBox.Show("Pump does not have an attached tip.", "Error: No Tip Attached.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void aspirateOperationButton_Click(object sender, EventArgs e)
        {
            bool labwareAvailable = false;

            if (platform.TipAttached == true)
            {
                for (int i = 0; i < labwareCount; i++)
                {
                    if (platform.Labwares[i] != null && (platform.Labwares[i].LabwareType == "wellplate" || platform.Labwares[i].LabwareType == "tubestand"))
                    {
                        labwareButtons[i].BackColor = buttonOperationsColor;
                        aspirateOperation[i] = true;
                        labwareAvailable = true;
                    }
                }

                if (labwareAvailable == false)
                {
                    MessageBox.Show("Before you can aspirate, dispense, or mix" +
                        " add a tip box to the platform," +
                        " add a 'Get Tip' operation," +
                        " and add a wellplate or tubestand to the platform."
                        , "Error: No Available Labware", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (platform.TipAttached == false)
            {
                MessageBox.Show("Before you can aspirate, dispense, or mix" +
                  " add a tip box to the platform," +
                  " add a 'Get Tip' operation," +
                  " and add a wellplate or tubestand to the platform."
                  , "Error: No Tip Attached", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dispenseOperationButton_Click(object sender, EventArgs e)
        {
            bool labwareAvailable = false;

            if (platform.TipAttached == true)
            {
                for (int i = 0; i < labwareCount; i++)
                {
                    if (platform.Labwares[i] != null && (platform.Labwares[i].LabwareType == "wellplate" || platform.Labwares[i].LabwareType == "tubestand"))
                    {
                        labwareButtons[i].BackColor = buttonOperationsColor;
                        dispenseOperation[i] = true;
                        labwareAvailable = true;
                    }
                }

                if (labwareAvailable == false)
                {
                    MessageBox.Show("Before you can aspirate, dispense, or mix," +
                        " add a tip box to the platform," +
                        " add a 'Get Tip' operation," +
                        " and add a wellplate or tubestand to the platform."
                        , "Error: No Available Labware", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (platform.TipAttached == false)
            {
                MessageBox.Show("Before you can aspirate, dispense, or mix," +
                    " add a tip box to the platform," +
                    " add a 'Get Tip' operation," +
                    " and add a wellplate or tubestand to the platform."
                    , "Error: No Tip Attached", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mixOperationButton_Click(object sender, EventArgs e)
        {
            bool labwareAvailable = false;

            if (platform.TipAttached == true)
            {
                for (int i = 0; i < labwareCount; i++)
                {
                    if (platform.Labwares[i] != null && (platform.Labwares[i].LabwareType == "wellplate" || platform.Labwares[i].LabwareType == "tubestand"))
                    {
                        labwareButtons[i].BackColor = buttonOperationsColor;
                        mixOperation[i] = true;
                        labwareAvailable = true;
                    }
                }

                if (labwareAvailable == false)
                {
                    MessageBox.Show("Before you can aspirate, dispense, or mix" +
                        " add a tip box to the platform," +
                        " add a 'Get Tip' operation," +
                        " and add a wellplate or tubestand to the platform."
                        , "Error: No Available Labware", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (platform.TipAttached == false)
            {
                MessageBox.Show("Before you can aspirate or dispense, or mix" +
                  " add a tip box to the platform," +
                  " add a 'Get Tip' operation," +
                  " and add a wellplate or tubestand to the platform."
                  , "Error: No Tip Attached", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /* Available Labware Buttons
         * -96 wellplate
         * -tubestand
         * -200 uL tipbox
         */
        private void wellplate_Click(object sender, EventArgs e)
        {
            wellplateButton.BackColor = buttonBackgroundColor1;
            tubeStandButton.BackColor = buttonBackgroundColor1;
            tipBoxButton.BackColor = buttonBackgroundColor1;

            for (int i = 0; i < labwareCount; i++)
            {
                if (labwareSelected[i] == true && platform.LabwareOccupied[i] == false)
                {
                    labwareButtons[i].Text = "96 Wellplate";
                    labwareButtons[i].BackColor = buttonSelectedColor;
                    labwareButtons[i].ForeColor = buttonTextColor1;

                    labwareSelected[i] = false;
                    platform.AddLabware(i, "wellplate");
                }
            }
        }
        private void tubeStand_Click(object sender, EventArgs e)
        {
            wellplateButton.BackColor = buttonBackgroundColor1;
            tubeStandButton.BackColor = buttonBackgroundColor1;
            tipBoxButton.BackColor = buttonBackgroundColor1;

            for (int i = 0; i < labwareCount; i++)
            {
                if (labwareSelected[i] == true && platform.LabwareOccupied[i] == false)
                {
                    labwareButtons[i].Text = "5mL Eppendorf Tubes";
                    labwareButtons[i].BackColor = buttonSelectedColor;
                    labwareButtons[i].ForeColor = buttonTextColor1;
                    labwareSelected[i] = false;
                    platform.AddLabware(i, "tubestand");
                }
            }
        }
        private void tipBox_Click(object sender, EventArgs e)
        {
            wellplateButton.BackColor = buttonBackgroundColor1;
            tubeStandButton.BackColor = buttonBackgroundColor1;
            tipBoxButton.BackColor = buttonBackgroundColor1;

            for (int i = 0; i < labwareCount; i++)
            {
                if (labwareSelected[i] == true && platform.LabwareOccupied[i] == false)
                {
                    labwareButtons[i].Text = "200uL Tip Box";
                    labwareButtons[i].BackColor = buttonSelectedColor;
                    labwareButtons[i].ForeColor = buttonTextColor1;
                    labwareSelected[i] = false;
                    platform.AddLabware(i, "tipbox");
                }
            }
        }
        /* Platform Labware Buttons
         * -Labware 1
         * -Labware 2
         * -Labware 3
         * -Labware 4
         */
        private void labware_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            int labwarePosition = -1;

            for (int i = 0; i < labwareCount; i++)
            {
                if (button.Name == "labwareButton1")
                {
                    labwarePosition = 0;
                }
                else if (button.Name == "labwareButton2")
                {
                    labwarePosition = 1;
                }
                else if (button.Name == "labwareButton3")
                {
                    labwarePosition = 2;
                }
                else if (button.Name == "labwareButton4")
                {
                    labwarePosition = 3;
                }
            }

            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1 && platform.LabwareOccupied[labwarePosition] == false)
                {
                    wellplateButton.BackColor = buttonSelectedColor;
                    tubeStandButton.BackColor = buttonSelectedColor;
                    tipBoxButton.BackColor = buttonSelectedColor;
                    labwareSelected[labwarePosition] = true;
                }
                else if (e.Clicks >= 2 && platform.LabwareOccupied[labwarePosition] == true)
                {
                    open_labware_properties(labwarePosition);
                }
                //Get Tip Operation
                if (getTipOperation[labwarePosition] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        getTipOperation[i] = false;
                        if (platform.LabwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (platform.LabwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (OperationsForm operationsForm = new OperationsForm(platform.Labwares[labwarePosition], platform.VolumeInTip, platform.TipCapacity, "getTip"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            platform.Labwares[labwarePosition].Volumes = operationsForm.Volumes;
                            platform.TipAttached = true;
                            platform.VolumeInTip = 0;
                            platform.TipCapacity = operationsForm.MaxVolume;
                            platform.SelectedPosition = operationsForm.SelectedPosition;
                            getTip(labwarePosition);
                        }
                    }
                }
                //Aspirite Volume Operation
                else if (aspirateOperation[labwarePosition] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        aspirateOperation[i] = false;
                        if (platform.LabwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (platform.LabwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (OperationsForm operationsForm = new OperationsForm(platform.Labwares[labwarePosition], platform.VolumeInTip, platform.TipCapacity, "aspirate"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            platform.Labwares[labwarePosition].Volumes = operationsForm.Volumes;
                            platform.VolumeInTip = operationsForm.VolumeMoved;
                            platform.SelectedPosition = operationsForm.SelectedPosition;
                            aspirateVolume(labwarePosition, operationsForm.VolumeMoved);
                        }
                    }
                }
                //Dispense Volume Operation
                else if (dispenseOperation[labwarePosition] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        dispenseOperation[i] = false;
                        if (platform.LabwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (platform.LabwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (OperationsForm operationsForm = new OperationsForm(platform.Labwares[labwarePosition], platform.VolumeInTip, platform.TipCapacity, "dispense"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            platform.Labwares[labwarePosition].Volumes = operationsForm.Volumes;
                            platform.VolumeInTip = operationsForm.VolumeMoved;
                            platform.SelectedPosition = operationsForm.SelectedPosition;
                            dispenseVolume(labwarePosition, operationsForm.VolumeMoved);
                        }
                    }
                }
                //mix Volume Operation
                else if (mixOperation[labwarePosition] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        mixOperation[i] = false;
                        if (platform.LabwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (platform.LabwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (OperationsForm operationsForm = new OperationsForm(platform.Labwares[labwarePosition], platform.VolumeInTip, platform.TipCapacity, "mix"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            platform.SelectedPosition = operationsForm.SelectedPosition;
                            mix(labwarePosition, operationsForm.VolumeMixed, operationsForm.MixCount);
                        }
                    }
                }

            }
            else if (e.Button == MouseButtons.Right && platform.LabwareOccupied[0] == true)
            {
                labwareMenuStrip.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }
        private void labware1PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(platform.Labwares[0]))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    platform.Labwares[0].Volumes = labwarePropertiesForm.Volumes;
                }
            }
        }
        private void removeLabware1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (platform.LabwareOccupied[0] == true)
            {
                labwareButton1.Text = "Labware 1";
                labwareButton1.BackColor = buttonBackgroundColor2;
                labwareButton1.ForeColor = buttonTextColor2;
                platform.RemoveLabware(0);
            }
        }
        private void labware2PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(platform.Labwares[1]))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    platform.Labwares[1].Volumes = labwarePropertiesForm.Volumes;
                }
            }
        }
        private void removeLabware2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (platform.LabwareOccupied[1] == true)
            {
                labwareButton2.Text = "Labware 2";
                labwareButton2.BackColor = buttonBackgroundColor2;
                labwareButton2.ForeColor = buttonTextColor2;
                platform.RemoveLabware(1);
            }
        }
        private void labware3ProperiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(platform.Labwares[2]))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    platform.Labwares[2].Volumes = labwarePropertiesForm.Volumes;
                }
            }
        }
        private void removeLabware3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (platform.LabwareOccupied[2] == true)
            {
                labwareButton3.Text = "Labware 3";
                labwareButton3.BackColor = buttonBackgroundColor2;
                labwareButton3.ForeColor = buttonTextColor2;
                platform.RemoveLabware(2);
            }
        }
        private void labware4PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(platform.Labwares[3]))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    platform.Labwares[3].Volumes = labwarePropertiesForm.Volumes;
                }
            }
        }
        private void removeLabware4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (platform.LabwareOccupied[3] == true)
            {
                labwareButton4.Text = "Labware 4";
                labwareButton4.BackColor = buttonBackgroundColor2;
                labwareButton4.ForeColor = buttonTextColor2;
                platform.RemoveLabware(3);
            }
        }
        private void labware1MenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (platform.LabwareOccupied[0] == false)
            {
                labware1PropertiesToolStripMenuItem.Enabled = false;
                removeLabware1ToolStripMenuItem.Enabled = false;
            }
            else
            {
                labware1PropertiesToolStripMenuItem.Enabled = true;
                removeLabware1ToolStripMenuItem.Enabled = true;
            }
        }
        private void labware2MenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (platform.LabwareOccupied[1] == false)
            {
                labware2PropertiesToolStripMenuItem.Enabled = false;
                removeLabware2ToolStripMenuItem.Enabled = false;
            }
            else
            {
                labware2PropertiesToolStripMenuItem.Enabled = true;
                removeLabware2ToolStripMenuItem.Enabled = true;
            }
        }
        private void labware3MenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (platform.LabwareOccupied[2] == false)
            {
                labware3PropertiesToolStripMenuItem.Enabled = false;
                removeLabware3ToolStripMenuItem.Enabled = false;
            }
            else
            {
                labware3PropertiesToolStripMenuItem.Enabled = true;
                removeLabware3ToolStripMenuItem.Enabled = true;
            }
        }
        private void labware4MenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if (platform.LabwareOccupied[3] == false)
            {
                labware4PropertiesToolStripMenuItem.Enabled = false;
                removeLabware4ToolStripMenuItem.Enabled = false;
            }
            else
            {
                labware4PropertiesToolStripMenuItem.Enabled = true;
                removeLabware4ToolStripMenuItem.Enabled = true;
            }
        }
        private void open_labware_properties(int labwarePosition)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(platform.Labwares[labwarePosition]))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    platform.Labwares[labwarePosition].Volumes = labwarePropertiesForm.Volumes;
                }
            }
        }

        private void manuallyMovePumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ManualControlForm manualControlForm = new ManualControlForm(serialPort, platform))
            {
                if (manualControlForm.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }
    }
}
