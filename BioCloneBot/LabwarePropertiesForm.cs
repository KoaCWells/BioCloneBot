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
    public partial class LabwarePropertiesForm : Form
    {
        private string labwareType;
        private bool[,] wellsSelected;
        private double maxVolume;
        private double[,] volumes;
        private int row;
        private int col;
        private int tableLayoutElementSize;

        private Rectangle selectionBox;
        private Point startPoint;
        private Point endPoint;
        private Bitmap emptyButtonBackground = null;
        private Bitmap fullButtonBackground = null;
        private Bitmap selectedButtonBackground = null;
        private TableLayoutPanel labwareReservoirsTableLayout = new TableLayoutPanel();
        private Button[,] reservoirButtons = null;

        public double[,] Volumes
        {
            get { return volumes; }
        }

        public LabwarePropertiesForm(Labware labware)
        {
            InitializeComponent();
            if (labware.LabwareType == "wellplate")
            {
                this.Text = "96 Wellplate Properties";
            }
            else if (labware.LabwareType == "")
            {
                this.Text = labware.LabwareType + "5mL Eppendorf Tubestand Properties";
            }
            else if (labware.LabwareType == "getTip")
            {
                this.Text = labware.LabwareType + "200uL Tipbox Properties";
            }
            
            this.ClientSize = new Size(1000, 560);
            selectionBox = new Rectangle();
            startPoint = new Point(0, 0);
            endPoint = new Point(0, 0);
            labwareType = labware.LabwareType;
            maxVolume = labware.MaxVolume;
            volumes = labware.Volumes;

            if (labwareType == "wellplate")
            {
                emptyButtonBackground = global::BioCloneBot.Properties.Resources.emptyWell;
                fullButtonBackground = global::BioCloneBot.Properties.Resources.fullWell;
                selectedButtonBackground = global::BioCloneBot.Properties.Resources.selectedWell;
                row = 8;
                col = 12;
                wellsSelected = new bool[row, col];
                tableLayoutElementSize = 60;
                setVolumeButton.Text = "Set Volume";
                emptyRemoveButton.Text = "Empty Wells";
            }
            else if (labwareType == "tubestand")
            {
                emptyButtonBackground = global::BioCloneBot.Properties.Resources.emptyTube;
                fullButtonBackground = global::BioCloneBot.Properties.Resources.fullTube;
                selectedButtonBackground = global::BioCloneBot.Properties.Resources.selectedTube;
                row = 4;
                col = 6;
                wellsSelected = new bool[row, col];
                tableLayoutElementSize = 120;
                setVolumeButton.Text = "Add Volume";
                emptyRemoveButton.Text = "Remove Tubes";
            }
            else if (labwareType == "tipbox")
            {
                emptyButtonBackground = global::BioCloneBot.Properties.Resources.tipUnavailable;
                fullButtonBackground = global::BioCloneBot.Properties.Resources.tipAvailable;
                selectedButtonBackground = global::BioCloneBot.Properties.Resources.tipSelected;
                row = 8;
                col = 12;
                wellsSelected = new bool[row, col];
                tableLayoutElementSize = 60;
                setVolumeButton.Text = "Add Tips";
                emptyRemoveButton.Text = "Remove Tips";
                inputVolumesTextBox.Hide();
                tableLayoutPanel2.SetColumnSpan(setVolumeButton, 2);
            }
            //sets each wellButton.Text to match the corresponding volume of the well in the (col, row)
            //and updates wells that have a volume greater than 0 uL to display the fullWell image
            //i and j are swapped because GetControlPosition elements are selected via (col, row)\

            labwareReservoirsTableLayout.RowCount = row;
            labwareReservoirsTableLayout.ColumnCount = col;
            labwareReservoirsTableLayout.Dock = DockStyle.Fill;
            labwareReservoirsTableLayout.Padding = new Padding(40, 40, 40, 40);
            labwareReservoirsTableLayout.MouseDown += reservoirsTableLayoutPanel_MouseDown;
            labwareReservoirsTableLayout.MouseMove += reservoirsTableLayoutPanel_MouseMove;
            labwareReservoirsTableLayout.MouseUp += reservoirsTableLayoutPanel_MouseUp;
            labwareReservoirsTableLayout.Paint += reservoirsTableLayoutPanel_Paint;
            splitContainer1.Panel1.Controls.Add(labwareReservoirsTableLayout);
            reservoirButtons = new Button[row, col];

            for (int i = 0; i < row; i++)
            {
                labwareReservoirsTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, tableLayoutElementSize));
                for (int j = 0; j < col; j++)
                {
                    reservoirButtons[i, j] = new Button();
                    reservoirButtons[i, j].Click += volumeLeftClickButton;
                    reservoirButtons[i, j].Dock = DockStyle.Fill;
                    reservoirButtons[i, j].BackgroundImageLayout = ImageLayout.Stretch;
                    labwareReservoirsTableLayout.Controls.Add(reservoirButtons[i, j], j, i);
                    labwareReservoirsTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, tableLayoutElementSize));

                    if (volumes[i, j] > 0)
                    {
                        reservoirButtons[i, j].BackgroundImage = fullButtonBackground;
                    }
                    else if (volumes[i, j] == 0)
                    {
                        reservoirButtons[i, j].BackgroundImage = emptyButtonBackground;
                    }

                    if(labwareType != "tipbox")
                    {
                        reservoirButtons[i, j].Text = volumes[i, j].ToString();
                    }
                }
                this.DoubleBuffered = true;
            }
        }

        private void volumeLeftClickButton(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackgroundImage = selectedButtonBackground;
            string[] buttonPosition = labwareReservoirsTableLayout.GetCellPosition(button).ToString().Split(',');
            wellsSelected[Int32.Parse(buttonPosition[1]), Int32.Parse(buttonPosition[0])] = true;
        }

        //Runs when left click occurs inside of tableLayoutPanel
        //stores initial click (x,y) location in startPoint
        private void reservoirsTableLayoutPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint.X = e.X;
                startPoint.Y = e.Y;
                this.Invalidate();
            }
        }

        //Calculates distance between startPoint and current (x,y) location of
        //cursor to re-draw selection rubberband. Since C# Rectangle objects are
        //drawn from the top left (x, y) point, there are four scenarios for updating
        //the rectangle depending on where the cursor moves from startPoint
        private void reservoirsTableLayoutPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //down and right from startPoint
                if (startPoint.X < e.X && startPoint.Y < e.Y)
                {
                    selectionBox.X = startPoint.X;
                    selectionBox.Y = startPoint.Y;
                    selectionBox.Width = e.X - startPoint.X;
                    selectionBox.Height = e.Y - startPoint.Y;
                }
                //down and left from startPoint
                else if (startPoint.X >= e.X && startPoint.Y < e.Y)
                {
                    selectionBox.X = e.X;
                    selectionBox.Y = startPoint.Y;
                    selectionBox.Width = startPoint.X - e.X;
                    selectionBox.Height = e.Y - startPoint.Y;
                }
                //up and right from startPoint
                else if (startPoint.X < e.X && startPoint.Y >= e.Y)
                {
                    selectionBox.X = startPoint.X;
                    selectionBox.Y = e.Y;
                    selectionBox.Width = e.X - startPoint.X;
                    selectionBox.Height = startPoint.Y - e.Y;
                }
                //up and left from startPoint
                else if (startPoint.X >= e.X && startPoint.Y >= e.Y)
                {
                    selectionBox.X = e.X;
                    selectionBox.Y = e.Y;
                    selectionBox.Width = startPoint.X - e.X;
                    selectionBox.Height = startPoint.Y - e.Y;
                }
                this.Invalidate();
                splitContainer1.Panel1.Refresh();
            }
        }

        private void reservoirsTableLayoutPanel_MouseUp(object sender, MouseEventArgs e)
        {
            endPoint.X = e.X;
            endPoint.Y = e.Y;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (isInside(reservoirButtons[i, j].Location.X, reservoirButtons[i, j].Location.Y))
                    {
                        reservoirButtons[i, j].BackgroundImage = selectedButtonBackground;
                        wellsSelected[i, j] = true;
                    }
                }
            }
            selectionBox.Width = 0;
            selectionBox.Height = 0;
            this.Invalidate();
            splitContainer1.Panel1.Refresh();
        }

        private void reservoirsTableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(Color.Gray))
            using (Pen pen = new Pen(brush))
                e.Graphics.DrawRectangle(pen, this.selectionBox);
        }

        private bool isInside(int x, int y)
        {
            //increases x and y to half the width and height of the well images
            //this way the well is selected through the center point instead of
            //the top left corner
            x = x + 37;
            y = y + 37;
            if (startPoint.X < endPoint.X && startPoint.Y < endPoint.Y)
            {
                if (x > startPoint.X && x < endPoint.X && y > startPoint.Y && y < endPoint.Y)
                {
                    return true;
                }
            }
            else if (startPoint.X > endPoint.X && startPoint.Y < endPoint.Y)
            {
                if (x < startPoint.X && x > endPoint.X && y > startPoint.Y && y < endPoint.Y)
                {
                    return true;
                }
            }
            else if (startPoint.X < endPoint.X && startPoint.Y > endPoint.Y)
            {
                if (x > startPoint.X && x < endPoint.X && y < startPoint.Y && y > endPoint.Y)
                {
                    return true;
                }
            }
            else if (startPoint.X > endPoint.X && startPoint.Y > endPoint.Y)
            {
                if (x < startPoint.X && x > endPoint.X && y < startPoint.Y && y > endPoint.Y)
                {
                    return true;
                }
            }
            return false;
        }

        //sets volume of the selected wells according to what the user inputs into the textbox
        private void setVolumesButton_Click(object sender, EventArgs e)
        {
            double setVolume = 0.0;

            if (labwareType != "tipbox")
            {
                try
                {
                    setVolume = Convert.ToDouble(inputVolumesTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Volume must be a decimal number.", "Error: Invalid Volume Entered", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(labwareType == "tipbox")
            {
                setVolume = 1.0;
            }

            if (setVolume > maxVolume)
            {
                MessageBox.Show("Maximum volume exceeded: " + maxVolume + "uL", " Error: Exceeded Maximum Volume", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (wellsSelected[i, j] == true && setVolume > 0)
                        {
                           volumes[i, j] = Convert.ToDouble(setVolume);
                            reservoirButtons[i, j].BackgroundImage = fullButtonBackground;
                            wellsSelected[i, j] = false;

                            if (labwareType != "tipbox")
                            {
                                reservoirButtons[i, j].Text = volumes[i, j].ToString();
                            }
                        }
                        else if (wellsSelected[i, j] == true && setVolume == 0)
                        {
                            volumes[i, j] = setVolume;
                            reservoirButtons[i, j].BackgroundImage = emptyButtonBackground;
                            wellsSelected[i, j] = false;

                            if (labwareType != "tipbox")
                            {
                                reservoirButtons[i, j].Text = volumes[i, j].ToString();
                            }
                        }
                    }
                }
            }
        }

        //selects all wells upon button click
        private void selectAllButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    reservoirButtons[i, j].BackgroundImage = selectedButtonBackground;
                    wellsSelected[i, j] = true;
                }
            }
        }

        private void emptyRemoveButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (wellsSelected[i, j] == true)
                    {
                        volumes[i, j] = 0.0;
                        reservoirButtons[i, j].BackgroundImage = emptyButtonBackground;
                        wellsSelected[i, j] = false;

                        if (labwareType != "tipbox")
                        {
                            reservoirButtons[i, j].Text = volumes[i, j].ToString();
                        }
                    }
                }
            }
        }

        //clears all selected wells by detection of the ESC key
        private void LabwarePropertiesForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (volumes[i, j] > 0)
                        {
                            reservoirButtons[i, j].BackgroundImage = fullButtonBackground;
                        }
                        else
                        {
                            reservoirButtons[i, j].BackgroundImage = emptyButtonBackground;
                        }
                        wellsSelected[i, j] = false;
                    }
                }
            }
        }

        //Closes the LabwarePropertiesForm, returning an OK saving the changed volumes for the labwares object passed to the form
        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
