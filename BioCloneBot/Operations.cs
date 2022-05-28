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
    public partial class Operations : Form
    {
        private Bitmap emptyButtonBackground;
        private Bitmap fullButtonBackground;
        private Bitmap selectedButtonBackground;
        private TableLayoutPanel labwareReservoirsTableLayout;
        private Button[,] reservoirButtons;
        private Labware labwareSelected;
        private string labwareType;
        private string mode;
        private bool[,] wellsSelected;
        private double maxVolume;
        private int row;
        private int col;
        private int tableLayoutElementSize;
        private int[] selectedPosition;

        public double[,] volumes { get; set; }

        public Operations(Labware labware, string mode)
        {
            InitializeComponent();
            this.ClientSize = new Size(1000, 560);
            this.DoubleBuffered = true;
            labwareReservoirsTableLayout = new TableLayoutPanel();
            labwareSelected = new Labware();
            labwareType = labware.LabwareType;
            this.mode = mode;
            maxVolume = labware.MaxVolume;
            volumes = labware.Volumes;
            selectedPosition = new int[2];

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
                volumeInputTextBox.Hide();
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
            volumeInputTextBox.Text = "";
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

                    if (volumes[i, j] > 0)
                    {
                        reservoirButtons[i, j].BackgroundImage = fullButtonBackground;
                    }
                    else if (volumes[i, j] == 0)
                    {
                        reservoirButtons[i, j].BackgroundImage = emptyButtonBackground;
                    }

                    if (labwareType != "tipbox")
                    {
                        reservoirButtons[i, j].Text = volumes[i, j].ToString();
                    }
                }
            }

            if(mode == "aspirate")
            {
                confirmationButton.Text = "Aspirate";
            }

            else if(mode == "dispense")
            {
                confirmationButton.Text = "Dispense";
            }

            else if(mode == "getTip")
            {
                confirmationButton.Text = "Attach Tip";
            }
        }
        private void reservoirSelection_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string[] buttonPosition = labwareReservoirsTableLayout.GetCellPosition(button).ToString().Split(',');
            int[] reservoirSelectedPosition = new int[2];

            reservoirSelectedPosition[0] = Int32.Parse(buttonPosition[1]);
            reservoirSelectedPosition[1] = Int32.Parse(buttonPosition[0]);

            //TO DO
            //1. ensure aspirate does not exceed 200 uL inside of the tip
            //2. ensure aspirate does not exceed the volume inside of the well
            //3. ensure dispense does not dispense a volume greater than what is in the tip
            //4. ensure dispense does not overfill a well or tube
            //5. ensure only one tip can be attached at a time
            //deselects the previously selected reservoir

            wellsSelected[selectedPosition[0], selectedPosition[1]] = false;
            if (volumes[selectedPosition[0], selectedPosition[1]] == 0)
            {
                reservoirButtons[selectedPosition[0], selectedPosition[1]].BackgroundImage = emptyButtonBackground;
            }
            else if (volumes[selectedPosition[0], selectedPosition[1]] > 0)
            {
                reservoirButtons[selectedPosition[0], selectedPosition[1]].BackgroundImage = fullButtonBackground;
            }

            button.BackgroundImage = selectedButtonBackground;

            selectedPosition[0] = reservoirSelectedPosition[0];
            selectedPosition[1] = reservoirSelectedPosition[1];
            wellsSelected[selectedPosition[0], selectedPosition[1]] = true;
        }

        //Closes the LabwarePropertiesForm, returning an OK saving the changed volumes for the labwares object passed to the form
        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void confirmationButton_Click(object sender, EventArgs e)
        {
            if(mode == "aspirate")
            {
                operationTextBox.Text = volumeInputTextBox.Text + "uL aspirated.";
                volumes[selectedPosition[0], selectedPosition[1]] -= Double.Parse(volumeInputTextBox.Text);
                reservoirButtons[selectedPosition[0], selectedPosition[1]].Text = volumes[selectedPosition[0], selectedPosition[1]].ToString();
            }

            else if(mode == "dispense")
            {
                operationTextBox.Text = volumeInputTextBox.Text + "uL dispensed.";
                volumes[selectedPosition[0], selectedPosition[1]] += Double.Parse(volumeInputTextBox.Text);
                reservoirButtons[selectedPosition[0], selectedPosition[1]].Text = volumes[selectedPosition[0], selectedPosition[1]].ToString();
            }

            else if(mode == "getTip")
            {
                operationTextBox.Text = volumeInputTextBox.Text + "Tip attached.";
                volumes[selectedPosition[0], selectedPosition[1]] = 0.0;
            }

            if (volumes[selectedPosition[0], selectedPosition[1]] == 0.0)
            {
                reservoirButtons[selectedPosition[0], selectedPosition[1]].BackgroundImage = emptyButtonBackground;
            }
            else if (volumes[selectedPosition[0], selectedPosition[1]] > 0.0)
            {
                reservoirButtons[selectedPosition[0], selectedPosition[1]].BackgroundImage = fullButtonBackground;
            }
        }
    }
}
