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
    public partial class ControlForm : Form
    {
        
        //the global variables
        private int xDir;
        private int yDir;
        private int zDir;
        private int numberOfCommands;
        private int labwareCount;
        private double xLocation;
        private double yLocation;
        private double zLocation;
        //private double xDest;
        //private double yDest;
        //private double zDest;
        //private double volumeMoved;
        private bool[] labwareSelected;
        private bool[] aspirateOperation;
        private bool[] dispenseOperation;
        private bool[] getTipOperation;

        //create new Platform object with 4 labware slots
        private Platform platform;
        private List<string> deviceCommands;
        private List<string> protocolList;
        private Button[] labwareButtons = new Button[4];
        private Color formBackgroundColor = Color.FromArgb(33,33,33);
        private Color buttonSelectedColor = Color.FromArgb(24, 225, 204);
        private Color buttonBackgroundColor1 = Color.FromArgb(194, 148, 255);
        private Color buttonBackgroundColor2 = Color.FromArgb(18, 18, 20);
        private Color buttonTextColor1 = Color.FromArgb(18, 18, 20);
        private Color buttonTextColor2 = Color.FromArgb(194, 148, 255);
        private Color buttonOperationsColor = Color.FromArgb(177, 255, 154);

        //During Form1 initilization, goes through each available serial port one by one sending the "ping" message. If "pong" is received back, the Arduino has been found then
        //opens a serial connection with the Arduino. If not found, displays an error message to try again.
        public ControlForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;

            labwareCount = 4;
            platform = new Platform(labwareCount);

            labwareSelected = new bool[labwareCount];
            aspirateOperation = new bool[labwareCount];
            dispenseOperation = new bool[labwareCount];
            getTipOperation = new bool[labwareCount];
            xLocation = -1.0;
            yLocation = -1.0;
            zLocation = -1.0;
            //xDest = -1.0;
            //yDest = -1.0;
            //zDest = -1.0;
            //volumeMoved = -1.0;
            
            for(int i = 0; i < labwareCount; i++)
            {
                labwareSelected[i] = false;
                aspirateOperation[i] = false;
                dispenseOperation[i] = false;
                getTipOperation[i] = false;
            }

            labwareMenuStrip = new ContextMenuStrip();
            deviceCommands = new List<string>();
            protocolList = new List<string>();
            labwareButtons[0] = labwareButton1;
            labwareButtons[1] = labwareButton2;
            labwareButtons[2] = labwareButton3;
            labwareButtons[3] = labwareButton4;

            initialize_Serial_Port();
        }
        /* Helper Functions
         * -initialize_Serial_Port
         * -updateCommand_List
         * 
         * 
         * 
         */
        private void initialize_Serial_Port()
        {
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
        private void updateCommand_List()
        {
            if(protocolList.Count > 0)
            {
                commandList.Clear();
                for(int i = 0; i < protocolList.Count(); i++)
                {
                    commandList.Text += (i+1) + ". " + protocolList[i] + ".\r\n";
                }
            }
        }
        private void movePump(double xDest, double yDest, double zDest)
        {
            double xTravel = 0;
            double yTravel = 0;
            double zTravel = 0;

            if (xDest > xLocation)
            {
                xDir = 0;
                xTravel = xDest - xLocation;
            }
            else if (xDest < xLocation)
            {
                xDir = 1;
                xTravel = xLocation - xDest;
            }
            if (yDest > yLocation)
            {
                yDir = 0;
                yTravel = yDest - yLocation;
            }
            else if (yDest < yLocation)
            {
                yDir = 1;
                yTravel = yLocation - yDest;
            }
            if (zDest > zLocation)
            {
                zDir = 0;
                zTravel = zDest - zLocation;
            }
            else if (zDest < zLocation)
            {
                zDir = 1;
                zTravel = zLocation - zDest;
            }
            if (!(xLocation == xDest && yLocation == yDest && zLocation == zDest))
            {
                xLocation = xDest;
                yLocation = yDest;
                zLocation = zDest;
                deviceCommands.Add("0001" + xDir + yDir + zDir + xTravel + yTravel + zTravel + "%");
                numberOfCommands++;
            }
        }
        private double[] calculateTravelDistance(int position, int[] wellLocation)
        {
            double[] destination = new double[3];
            double elementSeparation = platform.Labwares[position].ReservoirSeparation;
            double[] topLeftCorner = platform.Labwares[position].TopLeftCorner;
            double[] startLocation = platform.Labwares[position].StartLocation;

            destination[0] = topLeftCorner[0] + startLocation[0] + elementSeparation * wellLocation[0];
            destination[1] = topLeftCorner[1] + startLocation[1] + elementSeparation * wellLocation[1];
            destination[2] = platform.ZMax - platform.Labwares[position].Dimensions[2] - 5.0;

            return destination;
        }
        private void getTip(int labwarePosition)
        {
            //movePump(xLocation, yLocation, 0.0);
            //deviceCommands.Add(move over)
            //deviceCommands.Add(move down)
            //deviceCommands.Add(move down)
            //deviceCommands.Add(move up all the way)
            double[] destination = calculateTravelDistance(labwarePosition, platform.SelectedPosition);
            platform.Operations.Add(new Operation("gettip", xLocation, yLocation, zLocation, destination[0], destination[1], destination[2], 
                labwarePosition, platform.SelectedPosition, platform.Labwares[labwarePosition]));
            numberOfCommands = numberOfCommands + 5;
            xLocation = destination[0];
            yLocation = destination[1];
            zLocation = 0.0;
            protocolList.Add("Get Tip from " + platform.Labwares[labwarePosition].LabwareType + " in position " + (labwarePosition + 1));
            updateCommand_List();
        }
        private void removeTip()
        {
            //deviceCommands.Add(move up);
            //deviceCommands.Add(move over);
            //deviceCommands.Add(eject tip);
            platform.Operations.Add(new Operation("removetip", xLocation, yLocation, zLocation, platform.TrashLocation[0], platform.TrashLocation[1]));
            numberOfCommands = numberOfCommands + 5;
            xLocation = platform.TrashLocation[0];
            yLocation = platform.TrashLocation[1];
            zLocation = 0.0;
            protocolList.Add("Remove tip");
            updateCommand_List();
        }
        private void aspirateVolume(int labwarePosition, double volumeAspirated)
        {
            /*
            movePump(xLocation, yLocation, 0.0); //move up
            calculateTravelDistance(labwarePosition, platform.SelectedPosition); //move over and down
            movePump(xLocation, yLocation, zLocation - 5.0);
            //deviceCommands.Add(move over);
            //deviceCommands.Add(move down);
            //deviceCommands.Add(suck volume);
            //deviceCommands.Add(move above labware)
            numberOfCommands = numberOfCommands + 5;
            
            updateCommand_List();
            deviceCommands.Add("0010" + volume + "%");
            numberOfCommands++;
            */

            double[] destination = calculateTravelDistance(labwarePosition, platform.SelectedPosition);
            platform.Operations.Add(new Operation("aspirate", volumeAspirated, xLocation, yLocation, zLocation, destination[0], destination[1], destination[2],
                labwarePosition, platform.SelectedPosition, platform.Labwares[labwarePosition]));
            numberOfCommands = numberOfCommands + 5;
            protocolList.Add("Aspirated " + volumeAspirated + "uL from " + platform.Labwares[labwarePosition].LabwareType + " in position " + (labwarePosition + 1));
            updateCommand_List();
        }
        private void dispenseVolume(int labwarePosition, double volumeDispensed)
        {
            //movePump(xLocation, yLocation, 0.0);
            //deviceCommands.Add(move up)
            //deviceCommands.Add(move over);
            //deviceCommands.Add(move down);
            //deviceCommands.Add(dispense volume);
            //deviceCommands.Add(move above labware);
            //updateCommand_List();
            //deviceCommands.Add("0011" + volumeDispensed + "%");
            //numberOfCommands++;

            double[] destination = calculateTravelDistance(labwarePosition, platform.SelectedPosition);
            platform.Operations.Add(new Operation("aspirate", volumeDispensed, xLocation, yLocation, zLocation, destination[0], destination[1], destination[2],
                labwarePosition, platform.SelectedPosition, platform.Labwares[labwarePosition]));
            numberOfCommands = numberOfCommands + 5;
            protocolList.Add("Dispensed " + volumeDispensed + "uL to " + platform.Labwares[labwarePosition].LabwareType + " in position " + (labwarePosition + 1));
            updateCommand_List();
        }
        /* BioCloneBot Control Buttons
         * 
         * 
         */
        private void startExperimentButton_Click(object sender, EventArgs e)
        {
            int count = 0;
            MessageBox.Show("Click OK to confirm and start the experiment", "Starting Experiment", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //DialogResult msg = MessageBox.Show("The device will now begin homing. Please make sure the platform is empty before continuing.", "Homing Device" +
            //   "", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            bool commandCompleted = false;
            for(int i = 0; i < platform.Operations.Count; i++)
            {
                for(int j = 0; j < platform.Operations[i].Steps.Count; j++)
                {
                    try
                    {
                        if (serialPort1.IsOpen)
                        {
                            commandCompleted = false;
                            serialPort1.Write(platform.Operations[i].Steps[j]);
                            serialMessage.Clear();
                            while(commandCompleted == false)
                            {
                                //System.Threading.Thread.Sleep(10000);
                                //serialPort1.Write(platform.Operations[i].Steps[j]);
                                if (serialPort1.ReadExisting() == "complete")
                                {
                                    commandCompleted = true;
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
        private void homeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    MessageBox.Show("BioCloneBot will now start homing. Please make sure the platform is empty before continuining.", "Homing Device", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    serialPort1.Write("0000%");
                    xLocation = 0.0;
                    yLocation = 0.0;
                    zLocation = 0.0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void saveProtocolButton_Click(object sender, EventArgs e)
        {

        }
        private void loadSample_Click(object sender, EventArgs e)
        {
            double[,] wellplateVolume1 = new double[8, 12];
            double[,] wellplateVolume2 = new double[8, 12];
            double[,] tubestandVolume = new double[4, 6];
            double[,] tipboxTips = new double[8, 12];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    if (i < 4 && j < 6)
                    {
                        tubestandVolume[i, j] = 500.0;
                    }
                    wellplateVolume1[i, j] = 100.00;
                    wellplateVolume2[i, j] = 0.0;
                    tipboxTips[i, j] = 1.0;
                }
            }

            if (this.deviceCommands.Count == 0)
            {

                /*
                this.deviceCommands.Add("0000%");
                this.deviceCommands.Add("0001" + "110 " + "200.00" + "200.00" + "000.00" + "%");
                this.deviceCommands.Add("0001" + "001" + "000.00" + "000.00" + "100.00" + "%");
                updateCommand_List();
                */
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
        private void open_Serial_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                initialize_Serial_Port();
                DialogResult msg = MessageBox.Show("Serial port opened.", "Serial port opened.", MessageBoxButtons.OK);
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
        private void sendBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Write(serialMessage.Text);
                    serialMessage.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /* Operations Buttons
         * -homeDeviceButton_Click
         * -getTipButton_Click
         * -removeTipButton_Click
         * -aspirateButton_Click
         * -dispenseButton_Click
         * -0110 set microstepping: adjust microstepping and timing of all motors
         * -1110 enable stepper motors: sets sleep pin of all motor drivers to HIGH
         * -1111 disable stepper motors: sets sleep pin of all motor drivers to LOW
         */
        private void homeDeviceButton_Click(object sender, EventArgs e)
        {
            platform.Operations.Add(new Operation("home"));
            //platform.addOperation(z)
            //deviceCommands.Add("0000%");
            protocolList.Add("Home Device");
            numberOfCommands++;
            xLocation = 0.0;
            yLocation = 0.0;
            zLocation = 0.0;

            updateCommand_List();
        }
        private void getTipButton_Click(object sender, EventArgs e)
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
        private void removeTipButton_Click(object sender, EventArgs e)
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
        private void aspirateButton_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Before you can aspirate or dispense," +
                        " add a tip box to the platform," +
                        " add a 'Get Tip' operation," +
                        " and add a wellplate or tubestand to the platform."
                        , "Error: No Available Labware", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (platform.TipAttached == false)
            {
                MessageBox.Show("Before you can aspirate or dispense," +
                  " add a tip box to the platform," +
                  " add a 'Get Tip' operation," +
                  " and add a wellplate or tubestand to the platform."
                  , "Error: No Tip Attached", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dispenseButton_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Before you can aspirate or dispense," +
                        " add a tip box to the platform," +
                        " add a 'Get Tip' operation," +
                        " and add a wellplate or tubestand to the platform."
                        , "Error: No Available Labware", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (platform.TipAttached == false)
                {
                    MessageBox.Show("Before you can aspirate or dispense," +
                      " add a tip box to the platform," +
                      " add a 'Get Tip' operation," +
                      " and add a wellplate or tubestand to the platform."
                      , "Error: No Tip Attached", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
         * -Thermocycler - to be added
         * -Trash - to be added
         * -Labware 1
         * -Labware 2
         * -Labware 3
         * -Labware 4
         */
        private void thermocycler_Click(object sender, EventArgs e)
        {
            
        }
        private void trash_Click(object sender, EventArgs e)
        {

        }
        private void labware_MouseDown(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            int labwarePosition = -1;

            for(int i = 0; i < labwareCount; i++)
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
            /*
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(platform.Labwares[labwarePosition].labwareType,
                platform.Labwares[labwarePosition].maxVolume,
                platform.Labwares[labwarePosition].volumes))
            */
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(platform.Labwares[labwarePosition]))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    platform.Labwares[labwarePosition].Volumes = labwarePropertiesForm.Volumes;
                }
            }
        }
    }
}
