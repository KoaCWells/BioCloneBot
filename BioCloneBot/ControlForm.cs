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
        private double xDest;
        private double yDest;
        private double zDest;
        private double syringeVol;
        private bool[] labwareOccupied;
        private bool[] labwareSelected;
        private bool[] aspirateOperation;
        private bool[] dispenseOperation;
        private bool[] getTipOperation;

        private bool tipAttached;

        private Platform platform = new Platform();
        private List<string> deviceCommands;
        private List<Labware> labwares;


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
            labwares = new List<Labware>(new Labware[labwareCount]);
            labwareOccupied = new bool[labwareCount];
            labwareSelected = new bool[labwareCount];
            aspirateOperation = new bool[labwareCount];
            dispenseOperation = new bool[labwareCount];
            getTipOperation = new bool[labwareCount];
            
            for(int i = 0; i < labwareCount; i++)
            {
                labwares[i] = null;
                labwareOccupied[i] = false;
                labwareSelected[i] = false;
                aspirateOperation[i] = false;
                dispenseOperation[i] = false;
                getTipOperation[i] = false;
            }

            labwareMenuStrip = new ContextMenuStrip();
            deviceCommands = new List<string>();

            labwareButtons[0] = labwareButton1;
            labwareButtons[1] = labwareButton2;
            labwareButtons[2] = labwareButton3;
            labwareButtons[3] = labwareButton4;

            numberOfCommands = 0;
            syringeVol = 0;
            tipAttached = false;

            initialize_Serial_Port();
        }

        private void initialize_Serial_Port()
        {
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            string response = "";
            bool handshake = true;
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
        private void update_Command_List()
        {
            if(this.deviceCommands.Count > 0)
            {
                this.commandList.Clear();
                foreach (String command in this.deviceCommands)
                {
                    //Adds a new line then the item from the array to the textbox
                    commandList.Text = String.Join("\n", this.deviceCommands);
                }
            }
        }
        //device commands
        private void home_Device()
        {
            deviceCommands[numberOfCommands] = "0000%";
            numberOfCommands++;
            xLocation = 0.0;
            yLocation = 0.0;
            zLocation = 0.0;

            update_Command_List();
        }
        private void move_Pump(double x_dest, double y_dest, double z_dest)
        {
            double x_travel = 0;
            double y_travel = 0;
            double z_travel = 0;

            if (x_dest > xLocation)
            {
                xDir = 0;
                x_travel = x_dest - xLocation;
                //digitalWrite(X_DIR, LOW);
            }
            else if (x_dest < xLocation)
            {
                xDir = 1;
                x_travel = xLocation - x_dest;
                //digitalWrite(X_DIR, HIGH);
            }
            if (y_dest > yLocation)
            {
                yDir = 0;
                y_travel = y_dest - yLocation;
                //digitalWrite(Y_DIR, LOW);
            }
            else if (y_dest < yLocation)
            {
                yDir = 1;
                y_travel = yLocation - y_dest;
                //digitalWrite(Y_DIR, HIGH);
            }
            if (z_dest > zLocation)
            {
                zDir = 0;
                z_travel = z_dest - zLocation;
                //digitalWrite(Z_DIR, LOW);
            }
            else if (z_dest < zLocation)
            {
                zDir = 1;
                z_travel = zLocation - z_dest;
                //digitalWrite(Z_DIR, HIGH);
            }
            deviceCommands[numberOfCommands] = "0001" + xDir + yDir + zDir + x_travel + y_travel + z_travel + "%";
            numberOfCommands++;
            update_Command_List();
        }

        private void open_labware_properties(int labwarePosition)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(labwares[labwarePosition].labwareType, labwares[labwarePosition].maxVolume, labwares[labwarePosition].volumes))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    labwares[labwarePosition].volumes = labwarePropertiesForm.volumes;
                }
            }
        }
        //device control buttons
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

        /*
        * List of possible device commands
        * -0000 homeDevice: homes x, y, z, and p stepper motors
        * -0001 movePump: calculates and sends x_dir, y_dir, z_dir, x_location, y_location, z_location (XXX.XX location in mm)
        * -0010 getTip: moves to tip box and gets curr tip
        * -0011 remove tip: moves to tip trash and removes tip
        * -0100 aspirate volume: aspirate volume in ul (XXX.XX ul)
        * -0101 dispense volume: dispense volume in ul + 50 ul (XXX.XX ul)
        * -0110 set microstepping: adjust microstepping and timing of all motors
        * -1110 enable stepper motors: sets sleep pin of all motor drivers to HIGH
        * -1111 disable stepper motors: sets sleep pin of all motor drivers to LOW
        */
        
        //platform buttons
        private void thermocycler_Click(object sender, EventArgs e)
        {
            
        }
        private void trash_Click(object sender, EventArgs e)
        {

        }

        private void labware1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1 && labwareOccupied[0] == false)
                {
                    wellplateButton.BackColor = buttonSelectedColor;
                    tubeStandButton.BackColor = buttonSelectedColor;
                    tipBoxButton.BackColor = buttonSelectedColor;
                    labwareSelected[0] = true;
                }
                else if (e.Clicks >= 2 && labwareOccupied[0] == true)
                {
                    open_labware_properties(0);
                }

                else if (aspirateOperation[0] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        aspirateOperation[i] = false;
                        if(labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if(labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[0], "aspirate"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[0].volumes = operationsForm.volumes;
                        }
                    }
                }
                else if (dispenseOperation[0] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        dispenseOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[0], "dispense"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[0].volumes = operationsForm.volumes;
                        }
                    }
                }

                else if (getTipOperation[0] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        getTipOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[0], "getTip"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[0].volumes = operationsForm.volumes;
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right && labwareOccupied[0] == true)
            {
                labwareMenuStrip.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }

        private void labware2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1 && labwareOccupied[1] == false)
                {
                    wellplateButton.BackColor = buttonSelectedColor;
                    tubeStandButton.BackColor = buttonSelectedColor;
                    tipBoxButton.BackColor = buttonSelectedColor;
                    labwareSelected[1] = true;
                }
                else if (e.Clicks >= 2 && labwareOccupied[1] == true)
                {
                    open_labware_properties(1);
                }
                else if (aspirateOperation[1] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        aspirateOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[1], "aspirate"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[1].volumes = operationsForm.volumes;
                        }
                    }
                }
                else if (dispenseOperation[1] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        dispenseOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[1], "dispense"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[1].volumes = operationsForm.volumes;
                        }
                    }
                }
            }
            else if (getTipOperation[1] == true)
            {
                for (int i = 0; i < labwareCount; i++)
                {
                    getTipOperation[i] = false;
                    if (labwareOccupied[i] == true)
                    {
                        labwareButtons[i].BackColor = buttonSelectedColor;
                        labwareButtons[i].ForeColor = buttonTextColor1;
                    }
                    else if (labwareOccupied[i] == false)
                    {
                        labwareButtons[i].BackColor = buttonBackgroundColor2;
                        labwareButtons[i].ForeColor = buttonTextColor2;
                    }
                }

                using (Operations operationsForm = new Operations(labwares[1], "getTip"))
                {
                    if (operationsForm.ShowDialog() == DialogResult.OK)
                    {
                        labwares[1].volumes = operationsForm.volumes;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right && labwareOccupied[1] == true)
            {
                labwareMenuStrip.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }
        private void labware3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1 && labwareOccupied[2] == false)
                {
                    wellplateButton.BackColor = buttonSelectedColor;
                    tubeStandButton.BackColor = buttonSelectedColor;
                    tipBoxButton.BackColor = buttonSelectedColor;
                    labwareSelected[2] = true;
                }
                else if (e.Clicks >= 2 && labwareOccupied[2] == true)
                {
                    open_labware_properties(2);
                }
                else if (aspirateOperation[2] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        getTipOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[2], "aspirate"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[2].volumes = operationsForm.volumes;
                        }
                    }
                }
                else if (dispenseOperation[2] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        dispenseOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[2], "dispense"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[2].volumes = operationsForm.volumes;
                        }
                    }
                }
                else if (getTipOperation[2] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        getTipOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[2], "getTip"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[2].volumes = operationsForm.volumes;
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right && labwareOccupied[2] == true)
            {
                labwareMenuStrip.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }
        private void labware4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 1 && labwareOccupied[3] == false)
                {
                    wellplateButton.BackColor = buttonSelectedColor;
                    tubeStandButton.BackColor = buttonSelectedColor;
                    tipBoxButton.BackColor = buttonSelectedColor;
                    labwareSelected[3] = true;
                }
                else if (e.Clicks >= 2 && labwareOccupied[3] == true)
                {
                    open_labware_properties(3);
                }
                else if (aspirateOperation[3] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        aspirateOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[3], "aspirate"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[3].volumes = operationsForm.volumes;
                        }
                    }
                }
                else if (dispenseOperation[3] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        dispenseOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[3], "dispense"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[3].volumes = operationsForm.volumes;
                        }
                    }
                }
                else if (getTipOperation[3] == true)
                {
                    for (int i = 0; i < labwareCount; i++)
                    {
                        getTipOperation[i] = false;
                        if (labwareOccupied[i] == true)
                        {
                            labwareButtons[i].BackColor = buttonSelectedColor;
                            labwareButtons[i].ForeColor = buttonTextColor1;
                        }
                        else if (labwareOccupied[i] == false)
                        {
                            labwareButtons[i].BackColor = buttonBackgroundColor2;
                            labwareButtons[i].ForeColor = buttonTextColor2;
                        }
                    }

                    using (Operations operationsForm = new Operations(labwares[3], "getTip"))
                    {
                        if (operationsForm.ShowDialog() == DialogResult.OK)
                        {
                            labwares[3].volumes = operationsForm.volumes;
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right && labwareOccupied[3] == true)
            {
                labwareMenuStrip.Show(this, new Point(Cursor.Position.X, Cursor.Position.Y));//places the menu at the pointer position
            }
        }

        //labware buttons
        private void wellplate_Click(object sender, EventArgs e)
        {
            wellplateButton.BackColor = buttonBackgroundColor1;
            tubeStandButton.BackColor = buttonBackgroundColor1;
            tipBoxButton.BackColor = buttonBackgroundColor1;

            for(int i = 0; i < labwares.Count(); i++)
            {
                if (labwareSelected[i] == true && labwareOccupied[i] == false)
                {
                    labwareButtons[i].Text = "96 Wellplate";
                    labwareButtons[i].BackColor = buttonSelectedColor;
                    labwareButtons[i].ForeColor = buttonTextColor1;

                    labwareSelected[i] = false;
                    labwareOccupied[i] = true;
                    labwares[i] = new Wellplate();
                }
            }
        }
        private void tubeStand_Click(object sender, EventArgs e)
        {
            wellplateButton.BackColor = buttonBackgroundColor1;
            tubeStandButton.BackColor = buttonBackgroundColor1;
            tipBoxButton.BackColor = buttonBackgroundColor1;

            for (int i = 0; i < labwares.Count(); i++)
            {
                if (labwareSelected[i] == true && labwareOccupied[i] == false)
                {
                    labwareButtons[i].Text = "5mL Eppendorf Tubes";
                    labwareButtons[i].BackColor = buttonSelectedColor;
                    labwareButtons[i].ForeColor = buttonTextColor1;
                    labwareSelected[i] = false;
                    labwareOccupied[i] = true;
                    labwares[i] = new Tubestand();
                }
            }
        }
        private void tipBox_Click(object sender, EventArgs e)
        {
            wellplateButton.BackColor = buttonBackgroundColor1;
            tubeStandButton.BackColor = buttonBackgroundColor1;
            tipBoxButton.BackColor = buttonBackgroundColor1;

            for (int i = 0; i < labwares.Count(); i++)
            {
                if (labwareSelected[i] == true && labwareOccupied[i] == false)
                {
                    labwareButtons[i].Text = "200uL Tip Box";
                    labwareButtons[i].BackColor = buttonSelectedColor;
                    labwareButtons[i].ForeColor = buttonTextColor1;
                    labwareSelected[i] = false;
                    labwareOccupied[i] = true;
                    labwares[i] = new Tipbox();
                }
            }
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {
            //this.commandList.AutoSize = false;
           // this.commandList.Size = new System.Drawing.Size(289, 600);
        }

        private void startExperiment_Click(object sender, EventArgs e)
        {
            DialogResult msg = MessageBox.Show("The device will now begin homing. Please make sure the platform is empty before continuing.", "Homing Device" +
                "", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
        }

        private void loadSample_Click(object sender, EventArgs e)
        {
            if (this.deviceCommands.Count == 0)
            {
                this.deviceCommands.Add("0000%");
                this.deviceCommands.Add("0001" + "110 " + "200.00" + "200.00" + "000.00" + "%");
                this.deviceCommands.Add("0001" + "001" + "000.00" + "000.00" + "100.00" + "%");
                update_Command_List();
            }

            else
            {
                MessageBox.Show("Sample experiments can only be loaded if there are no operations in the Protocol Queue.", "Error: Cannot Load Sample Experiment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void labware1PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(labwares[0].labwareType, labwares[0].maxVolume, labwares[0].volumes))
            {
                if(labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    labwares[0].volumes = labwarePropertiesForm.volumes;
                }
            }    
        }
        private void removeLabware1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (labwareOccupied[0] == true)
            {
                labwareButton1.Text = "Labware 1";
                labwareButton1.BackColor = buttonBackgroundColor2;
                labwareButton1.ForeColor = buttonTextColor2;
                labwareOccupied[0] = false;
                labwares[0] = null;
            }
        }
        private void labware2PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(labwares[1].labwareType, labwares[1].maxVolume, labwares[1].volumes))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    labwares[1].volumes = labwarePropertiesForm.volumes;
                }
            }
        }
        private void removeLabware2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (labwareOccupied[1] == true)
            {
                labwareButton2.Text = "Labware 2";
                labwareButton2.BackColor = buttonBackgroundColor2;
                labwareButton2.ForeColor = buttonTextColor2;
                labwareOccupied[1] = false;
                labwares[1] = null;
            }
        }
        private void labware3ProperiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(labwares[2].labwareType, labwares[2].maxVolume, labwares[2].volumes))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    labwares[2].volumes = labwarePropertiesForm.volumes;
                }
            }
        }
        private void removeLabware3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (labwareOccupied[2] == true)
            {
                labwareButton3.Text = "Labware 3";
                labwareButton3.BackColor = buttonBackgroundColor2;
                labwareButton3.ForeColor = buttonTextColor2;
                labwareOccupied[2] = false;
                labwares[2] = null;
            }
        }
        private void labware4PropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (LabwarePropertiesForm labwarePropertiesForm = new LabwarePropertiesForm(labwares[3].labwareType, labwares[3].maxVolume, labwares[3].volumes))
            {
                if (labwarePropertiesForm.ShowDialog() == DialogResult.OK)
                {
                    labwares[3].volumes = labwarePropertiesForm.volumes;
                }
            }
        }
        private void removeLabware4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (labwareOccupied[3] == true)
            {
                labwareButton4.Text = "Labware 4";
                labwareButton4.BackColor = buttonBackgroundColor2;
                labwareButton4.ForeColor = buttonTextColor2;
                labwareOccupied[3] = false;
                labwares[3] = null;
            }
        }

        private void labware1MenuStrip_Opening(object sender, CancelEventArgs e)
        {
            if(labwareOccupied[0] == false)
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
            if (labwareOccupied[1] == false)
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
            if (labwareOccupied[2] == false)
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
            if (labwareOccupied[3] == false)
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

        private void homeDeviceButton_Click(object sender, EventArgs e)
        {
            this.deviceCommands.Add("0000%\n");
            update_Command_List();
        }

        private void getTipButton_Click(object sender, EventArgs e)
        {
            bool labwareAvailable = false;

            for (int i = 0; i < labwares.Count; i++)
            {
                if (labwares[i] != null && (labwares[i].labwareType == "tipbox"))
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

        private void removeTipButton_Click(object sender, EventArgs e)
        {

        }

        private void aspirateButton_Click(object sender, EventArgs e)
        {
            bool labwareAvailable = false;

            for (int i = 0; i < labwares.Count; i++)
            {
                if (labwares[i] != null && (labwares[i].labwareType == "wellplate" || labwares[i].labwareType == "tubestand"))
                {
                    labwareButtons[i].BackColor = buttonOperationsColor;
                    aspirateOperation[i] = true;
                    labwareAvailable = true;
                }
            }

            if (labwareAvailable == false)
            {
                MessageBox.Show("Before you can aspirate or dispense, first add a wellplate or tubestand to the platform.", "Error: No Available Labware", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dispenseButton_Click(object sender, EventArgs e)
        {
            bool labwareAvailable = false;

            for (int i = 0; i < labwares.Count; i++)
            {
                if (labwares[i] != null && (labwares[i].labwareType == "wellplate" || labwares[i].labwareType == "tubestand"))
                {
                    labwareButtons[i].BackColor = buttonOperationsColor;
                    dispenseOperation[i] = true;
                    labwareAvailable = true;
                }
            }

            if (labwareAvailable == false)
            {
                MessageBox.Show("Before you can aspirate or dispense, first add a wellplate or tubestand to the platform.", "Error: No Available Labware", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
