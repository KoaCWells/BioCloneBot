using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BioCloneBot
{
    public partial class OperationsForm : Form
    {
        private string labwareType;
        private string mode;
        private bool operationConfirmed;
        private bool[,] wellsSelected;
        private double maxVolume;
        private double volumeInTip;
        private double tipCapacity;
        private double volumeMoved;
        private double mixVolume;
        private double[][] volumes;
        private int row;
        private int col;
        private int tableLayoutElementSize;
        private int mixCount;
        private int[] selectedPosition;
        private Labware labware;
        private Bitmap emptyButtonBackground = null;
        private Bitmap fullButtonBackground = null;
        private Bitmap selectedButtonBackground = null;
        private TableLayoutPanel labwareReservoirsTableLayout = new TableLayoutPanel();
        private Button[,] reservoirButtons = null;
        public double[][] Volumes
        {
            get { return volumes; }
        }
        public double MaxVolume
        {
            get { return maxVolume; }
        }
        public double VolumeMoved
        {
            get { return volumeMoved; }
        }
        public double VolumeMixed
        {
            get { return mixVolume; }
        }
        public int MixCount
        {
            get { return mixCount; }
        }
        public int[] SelectedPosition
        {
            get { return selectedPosition; }
            set { selectedPosition = value; }
        }
        public OperationsForm(Labware labware, double volumeInTip, double tipCapacity, string mode)
        {
            InitializeComponent();
            if (mode == "aspirate")
            {
                this.Text = "Aspirate Operation";
                confirmationButton.Text = "Aspirate";
                volumeLabelTextBox.Text = "Volume to aspirate:";
                inputVolumeTextBox.Text = "0.0";
                inputVolumeTextBox.Visible = true;
                mixCountLabelTextBox.Visible = false;
                mixCountInputTextBox.Visible = false;
                airGapCheckBox.Visible = true;
            }
            else if (mode == "dispense")
            {
                this.Text = "Dispense Operation";
                confirmationButton.Text = "Dispense";
                volumeLabelTextBox.Text = "Volume to dispense:";
                inputVolumeTextBox.Text = "0.0";
                inputVolumeTextBox.Visible = true;
                mixCountLabelTextBox.Visible = false;
                mixCountInputTextBox.Visible = false;
                airGapCheckBox.Visible = true;
            }
            else if (mode == "getTip")
            {
                this.Text = "Attach Tip Operation";
                confirmationButton.Text = "Attach Tip";
                inputVolumeTextBox.Visible = false;
                volumeLabelTextBox.Visible = false;
                mixCountLabelTextBox.Visible = false;
                mixCountInputTextBox.Visible = false;
                airGapCheckBox.Visible = false;
            }
            else if (mode == "mix")
            {
                this.Text = "Mix Operation";
                confirmationButton.Text = "Mix";
                volumeLabelTextBox.Text = "Volume to mix:";
                inputVolumeTextBox.Text = "0.0";
                mixCountLabelTextBox.Text = "Number of mixes:";
                mixCountInputTextBox.Text = "1";
                inputVolumeTextBox.Visible = true;
                mixCountLabelTextBox.Visible = true;
                mixCountInputTextBox.Visible = true;
                airGapCheckBox.Visible = false;
            }

            this.ClientSize = new Size(1000, 560);
            this.DoubleBuffered = true;
            this.labware = labware;
            labwareReservoirsTableLayout = new TableLayoutPanel();
            labwareType = labware.LabwareType;
            this.mode = mode;
            operationConfirmed = false;
            maxVolume = labware.MaxVolume;
            this.volumeInTip = volumeInTip;
            this.tipCapacity = tipCapacity;
            volumeMoved = 0.0;
            volumes = new double[labware.Row][];
            for (int i = 0; i < labware.Row; i++)
            {
                volumes[i] = labware.Volumes[i].Clone() as double[];
            }
            selectedPosition = new int[2] { -1, -1 };

            if (labwareType == "wellplate")
            {
                emptyButtonBackground = global::BioCloneBot.Properties.Resources.emptyWell;
                fullButtonBackground = global::BioCloneBot.Properties.Resources.fullWell;
                selectedButtonBackground = global::BioCloneBot.Properties.Resources.selectedWell;
                row = 8;
                col = 12;
                tableLayoutElementSize = 60;
            }
            else if (labwareType == "tubestand")
            {
                emptyButtonBackground = global::BioCloneBot.Properties.Resources.emptyTube;
                fullButtonBackground = global::BioCloneBot.Properties.Resources.fullTube;
                selectedButtonBackground = global::BioCloneBot.Properties.Resources.selectedTube;
                row = 4;
                col = 6;
                tableLayoutElementSize = 120;
            }
            else if (labwareType == "tipbox")
            {
                emptyButtonBackground = global::BioCloneBot.Properties.Resources.tipUnavailable;
                fullButtonBackground = global::BioCloneBot.Properties.Resources.tipAvailable;
                selectedButtonBackground = global::BioCloneBot.Properties.Resources.tipSelected;
                row = 8;
                col = 12;
                tableLayoutElementSize = 60;
                inputVolumeTextBox.Hide();
                tableLayoutPanel2.SetColumnSpan(confirmationButton, 2);
            }
            //sets each wellButton.Text to match the corresponding volume of the well in the (col, row)
            //and updates wells that have a volume greater than 0 uL to display the fullWell image
            //i and j are swapped because GetControlPosition elements are selected via (col, row)

            labwareReservoirsTableLayout.RowCount = row;
            labwareReservoirsTableLayout.ColumnCount = col;
            labwareReservoirsTableLayout.Dock = DockStyle.Fill;
            labwareReservoirsTableLayout.Padding = new Padding(40, 40, 40, 40);
            splitContainer1.Panel1.Controls.Add(labwareReservoirsTableLayout);
            reservoirButtons = new Button[row, col];
            wellsSelected = new bool[row, col];
            inputVolumeTextBox.Text = "";
            operationTextBox.Text = "";

            for (int i = 0; i < row; i++)
            {
                labwareReservoirsTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, tableLayoutElementSize));
                for (int j = 0; j < col; j++)
                {
                    reservoirButtons[i, j] = new Button();
                    reservoirButtons[i, j].Click += reservoirSelection_Click;
                    reservoirButtons[i, j].Dock = DockStyle.Fill;
                    reservoirButtons[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    labwareReservoirsTableLayout.Controls.Add(reservoirButtons[i, j], j, i);
                    labwareReservoirsTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, tableLayoutElementSize));
                    wellsSelected[i, j] = false;

                    if (volumes[i][j] > 0)
                    {
                        reservoirButtons[i, j].BackgroundImage = fullButtonBackground;
                    }
                    else if (volumes[i][j] == 0)
                    {
                        reservoirButtons[i, j].BackgroundImage = emptyButtonBackground;
                    }

                    if (labwareType != "tipbox")
                    {
                        reservoirButtons[i, j].Text = volumes[i][j].ToString();
                    }
                }
            }
        }
      
        private void reservoirSelection_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string[] buttonPosition = labwareReservoirsTableLayout.GetCellPosition(button).ToString().Split(',');
            int[] reservoirSelectedPosition = new int[2];

            reservoirSelectedPosition[0] = Int32.Parse(buttonPosition[1]);
            reservoirSelectedPosition[1] = Int32.Parse(buttonPosition[0]);

            if (selectedPosition[0] == -1 && selectedPosition[1] == -1)
            {
                selectedPosition[0] = reservoirSelectedPosition[0];
                selectedPosition[1] = reservoirSelectedPosition[1];
                button.BackgroundImage = selectedButtonBackground;
                wellsSelected[selectedPosition[0], selectedPosition[1]] = true;
            }

            else if (selectedPosition[0] != -1 && selectedPosition[1] != -1)
            {
                //Unselect the previous selection if there is one
                wellsSelected[selectedPosition[0],selectedPosition[1]] = false;

                if (volumes[selectedPosition[0]][selectedPosition[1]] == 0)
                {
                    reservoirButtons[selectedPosition[0], selectedPosition[1]].BackgroundImage = emptyButtonBackground;
                }
                else if (volumes[selectedPosition[0]][selectedPosition[1]] > 0)
                {
                    reservoirButtons[selectedPosition[0], selectedPosition[1]].BackgroundImage = fullButtonBackground;
                }

                button.BackgroundImage = selectedButtonBackground;

                selectedPosition[0] = reservoirSelectedPosition[0];
                selectedPosition[1] = reservoirSelectedPosition[1];
                wellsSelected[selectedPosition[0], selectedPosition[1]] = true;
            }

        }
        private void confirmationButton_Click(object sender, EventArgs e)
        {
            double setVolume = 0.0;
            //If nothing is selected, error
            if(selectedPosition[0] == -1 && selectedPosition[1] == -1)
            {
                MessageBox.Show("First select a source or destination.", "Error: Nothing Selected.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (operationConfirmed == false)
                {
                    //Aspirate Operation
                    if (mode == "aspirate")
                    {
                        //If inputVolume is empty or if a none numerical value is entered, error
                        if (inputVolumeTextBox.Text == "" || !Double.TryParse(inputVolumeTextBox.Text, out _))
                        {
                            MessageBox.Show("Volume must be a decimal number.", "Error: Invalid Volume", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        //If inputVolume is a valid number
                        else
                        {
                            setVolume = Convert.ToDouble(inputVolumeTextBox.Text);
                            //If setVolume exceeds tipCapacity, error
                            if (setVolume + volumeInTip > tipCapacity)
                            {
                                MessageBox.Show("Pipette tip maximum capacity is: " + tipCapacity + "uL", " Error: Exceeded Maximum Volume", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            //If setVolume exceeds volume inside of the selected well, aspirate well volume instead of setVolume
                            else if (setVolume > volumes[selectedPosition[0]][selectedPosition[1]])
                            {
                                volumeMoved = volumes[selectedPosition[0]][selectedPosition[1]];
                                operationTextBox.Text = volumeMoved + "uL aspirated.";
                                volumes[selectedPosition[0]][selectedPosition[1]] = 0.0;
                                reservoirButtons[selectedPosition[0], selectedPosition[1]].Text = volumes[selectedPosition[0]][selectedPosition[1]].ToString();
                                operationConfirmed = true;
                            }
                            //If setVolume is equal to or less than well volume, subtract setVolume from selected well volume
                            else if (setVolume <= volumes[selectedPosition[0]][selectedPosition[1]])
                            {
                                volumeMoved = setVolume;
                                operationTextBox.Text = volumeMoved + "uL aspirated.";
                                volumes[selectedPosition[0]][selectedPosition[1]] -= Double.Parse(inputVolumeTextBox.Text);
                                reservoirButtons[selectedPosition[0], selectedPosition[1]].Text = volumes[selectedPosition[0]][selectedPosition[1]].ToString();
                                operationConfirmed = true;
                            }
                        }
                    }
                    //Dispense Operation
                    else if (mode == "dispense")
                    {
                        //If inputVolume is empty or if a none numerical value is entered, error
                        if (inputVolumeTextBox.Text == "" || !Double.TryParse(inputVolumeTextBox.Text, out _))
                        {
                            MessageBox.Show("Volume must be a decimal number.", "Error: Invalid Volume", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            setVolume = Convert.ToDouble(inputVolumeTextBox.Text);
                            //If setVolume > tipCapacity, error
                            if (setVolume > tipCapacity)
                            {
                                MessageBox.Show("You cannot dispense more than tip capacity: " + tipCapacity + "uL", " Error: Exceeded Tip Capacity", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            //If setVolume > volumeInTip, error
                            else if (setVolume > volumeInTip)
                            {
                                MessageBox.Show("You cannot dispense more than the volume currently inside the tip: " + volumeInTip + "uL", " Error: Exceeded Available Volume", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            //If setVolume + selected well volume exceeds maxVolume of well, error
                            else if (setVolume + volumes[selectedPosition[0]][selectedPosition[1]] > maxVolume)
                            {
                                MessageBox.Show("Maximum volume exceeded: " + maxVolume + "uL", " Error: Exceeded Maximum Volume", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                volumeMoved = setVolume;
                                operationTextBox.Text = volumeMoved + "uL dispensed.";
                                volumes[selectedPosition[0]][selectedPosition[1]] += Double.Parse(inputVolumeTextBox.Text);
                                reservoirButtons[selectedPosition[0], selectedPosition[1]].Text = volumes[selectedPosition[0]][selectedPosition[1]].ToString();
                                volumeInTip -= setVolume;
                                operationConfirmed = true;
                            }
                        }

                    }
                    //Get Tip Operation
                    else if (mode == "getTip")
                    {
                        operationTextBox.Text = inputVolumeTextBox.Text + "Tip attached.";
                        volumes[selectedPosition[0]][selectedPosition[1]] = 0.0;
                        operationConfirmed = true;
                    }
                    else if(mode == "mix")
                    {
                        if (inputVolumeTextBox.Text == "" || !Double.TryParse(inputVolumeTextBox.Text, out _) || !int.TryParse(mixCountInputTextBox.Text, out _))
                        {
                            MessageBox.Show("Volume must be a decimal number and mix count must be an integer.", "Error: Invalid Volume", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            operationTextBox.Text = "Mixing " + inputVolumeTextBox.Text + "uL " + mixCountInputTextBox.Text + " times.";
                            mixVolume = Convert.ToDouble(inputVolumeTextBox.Text);
                            mixCount = Convert.ToInt32(mixCountInputTextBox.Text);
                            operationConfirmed = true;
                        }
                    }

                    //Reset background for selected reservoirButton
                    if (volumes[selectedPosition[0]][selectedPosition[1]] == 0.0)
                    {
                        reservoirButtons[selectedPosition[0], selectedPosition[1]].BackgroundImage = emptyButtonBackground;
                    }
                    else if (volumes[selectedPosition[0]][selectedPosition[1]] > 0.0)
                    {
                        reservoirButtons[selectedPosition[0], selectedPosition[1]].BackgroundImage = fullButtonBackground;
                    }
                }
            }
            
        }
        //Closes the LabwarePropertiesForm, returning an OK saving the changed volumes for the labwares object passed to the form
        private void OKButton_Click(object sender, EventArgs e)
        {
            if (selectedPosition[0] == -1 && selectedPosition[1] == -1)
            {
                MessageBox.Show("Select one of the options and click confirm. Otherwise, click cancel.", "Error: Nothing Selected.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (operationConfirmed == true)
            {
                if ((mode == "aspirate" || mode == "dispense") && airGapCheckBox.Checked)
                {
                    volumeMoved += 25.00;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
