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
        private Rectangle selectionBox = new Rectangle();
        private Point startPoint = new Point(0, 0);
        private Point endPoint = new Point(0, 0);
        private Bitmap emptyButtonBackground = null;
        private Bitmap fullButtonBackground = null;
        private Bitmap selectedButtonBackground = null;
        private TableLayoutPanel labwareReservoirs = new TableLayoutPanel();

        private string labwareType = "";
        private bool[,] wellsSelected;
        private double maxVolume;
        private int row = 0;
        private int col = 0;

        public double[,] volumes { get; set; }

        public LabwarePropertiesForm(string labwareType, double maxVolume, double[,] volumes)
        {
            InitializeComponent();
            this.labwareType = labwareType;
            this.maxVolume = maxVolume;



            if (labwareType == "wellplate")
            {
                this.row = 8;
                this.col = 12;
                this.volumes = new double[row, col];
                this.wellsSelected = new bool[row, col];
                this.emptyButtonBackground = global::BioCloneBot.Properties.Resources.emptyWell;
                this.fullButtonBackground = global::BioCloneBot.Properties.Resources.fullWell;
                this.selectedButtonBackground = global::BioCloneBot.Properties.Resources.selectedWell;

                setVolumeButton.Text = "Set Volume";
                emptyRemoveButton.Text = "Empty Wells";
            }
            else if(labwareType == "tubestand")
            {
                this.row = 4;
                this.col = 6;
                this.volumes = new double[row, col];
                this.wellsSelected = new bool[row, col];
                this.emptyButtonBackground = global::BioCloneBot.Properties.Resources.emptyWell;
                this.fullButtonBackground = global::BioCloneBot.Properties.Resources.fullWell;
                this.selectedButtonBackground = global::BioCloneBot.Properties.Resources.selectedWell;

                setVolumeButton.Text = "Add Volume";
                emptyRemoveButton.Text = "Remove Tubes";


                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        if(i%2 != 0 && j%2 != 0)
                        {
                            labwareVolumesTableLayout.GetControlFromPosition(j, i).Hide();
                            labwareVolumesTableLayout.GetControlFromPosition(j, i).Text = "";
                        }
                    }
                }
            }
            else if(labwareType == "tipbox")
            {
                this.row = 8;
                this.col = 12;
                this.volumes = new double[row, col];
                this.wellsSelected = new bool[row, col];
                this.emptyButtonBackground = global::BioCloneBot.Properties.Resources.tipUnavailable;
                this.fullButtonBackground = global::BioCloneBot.Properties.Resources.tipAvailable;
                this.selectedButtonBackground = global::BioCloneBot.Properties.Resources.tipSelected;

                setVolumeButton.Text = "Add Tips";
                emptyRemoveButton.Text = "Remove Tips";
                inputVolumesTextBox.Hide();
            }
            //sets each wellButton.Text to match the corresponding volume of the well in the (col, row)
            //and updates wells that have a volume greater than 0 uL to display the fullWell image
            //i and j are swapped because GetControlPosition elements are selected via (col, row)\
            
            for (int i = 0; i < row; i++)
            {

                for (int j = 0; j < col; j++)
                {
                    labwareVolumesTableLayout.GetControlFromPosition(j, i).Click += volumeLeftClickButton;
                    this.volumes[i, j] = volumes[i, j];

                    if (volumes[i, j] > 0)
                    {
                        labwareVolumesTableLayout.GetControlFromPosition(j, i).BackgroundImage = fullButtonBackground;
                        if(this.labwareType == "wellplate")
                        {
                            labwareVolumesTableLayout.GetControlFromPosition(j, i).Text = this.volumes[i,j].ToString();
                        }
                        else if(this.labwareType == "tubestand")
                        {   
                            if(i % 2 == 0 && j % 2 == 0)
                            {
                                labwareVolumesTableLayout.SetRowSpan(labwareVolumesTableLayout.GetControlFromPosition(j, i), 2);
                                labwareVolumesTableLayout.SetColumnSpan(labwareVolumesTableLayout.GetControlFromPosition(j, i), 2);
                            }
                        }
                    }
                    else if (volumes[i, j] == 0.0)
                    {
                        labwareVolumesTableLayout.GetControlFromPosition(j, i).BackgroundImage = emptyButtonBackground;
                        {
                            if (labwareType == "wellplate")
                            {
                                labwareVolumesTableLayout.GetControlFromPosition(j, i).Text = this.volumes[i, j].ToString();
                            }
                            else if (this.labwareType == "tubestand")
                            {
                                if (i % 2 == 0 && j % 2 == 0)
                                {
                                    labwareVolumesTableLayout.SetRowSpan(labwareVolumesTableLayout.GetControlFromPosition(j, i), 2);
                                    labwareVolumesTableLayout.SetColumnSpan(labwareVolumesTableLayout.GetControlFromPosition(j, i), 2);
                                }
                            }
                        }
                    }
                    
                    if(labwareType == "tubestand")
                    {
                        if(j%2 == 0)
                        {
                            j++;
                        }
                    }
                }
                if(labwareType == "tubestand")
                {
                    if (i % 2 == 0)
                    {
                        i++;
                    }
                }
            }
            this.DoubleBuffered = true;
        }

        private void volumeLeftClickButton(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.BackgroundImage = selectedButtonBackground;
            string[] buttonPosition = labwareVolumesTableLayout.GetCellPosition(button).ToString().Split(',');
            wellsSelected[Int32.Parse(buttonPosition[1]), Int32.Parse(buttonPosition[0])] = true;
        }

        //The following four functions are all used to draw the selection rectangle
        //MouseDown detects when the user left clicks inside of the tableLayoutPanel
        //stores the (x,y) coordinate where the user left clicks in startPoint
        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                startPoint.X = e.X;
                startPoint.Y = e.Y;
                this.Invalidate();
            }
        }

        //MouseMove detects when the cursor moves while left click is held down
        //calculates distance between startPoint and current (x,y) location of
        //cursor to re-draw selection rubberband. Since C# Rectangle objects are
        //drawn from the top left (x, y) point, there are four scenarios for updating
        //the rectangle depending on where the cursor moves from startPoint
        // -down and right from startPoint
        // -down and left from startPoint
        // -up and right from startPoint
        // -up and left from startPoint
        private void tableLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //down and right
                if (startPoint.X < e.X && startPoint.Y < e.Y)
                {
                    selectionBox.X = startPoint.X;
                    selectionBox.Y = startPoint.Y;
                    selectionBox.Width = e.X - startPoint.X;
                    selectionBox.Height = e.Y - startPoint.Y;
                }
                //down and left
                else if (startPoint.X >= e.X && startPoint.Y < e.Y)
                {
                    selectionBox.X = e.X;
                    selectionBox.Y = startPoint.Y;
                    selectionBox.Width = startPoint.X - e.X;
                    selectionBox.Height = e.Y - startPoint.Y;
                }
                //up and right
                else if (startPoint.X < e.X && startPoint.Y >= e.Y)
                {
                    selectionBox.X = startPoint.X;
                    selectionBox.Y = e.Y;
                    selectionBox.Width = e.X - startPoint.X;
                    selectionBox.Height = startPoint.Y - e.Y;
                }
                //up and left
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

        private void tableLayoutPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            endPoint.X = e.X;
            endPoint.Y = e.Y;
            for (int i = 0; i < row ; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if(isInside(labwareVolumesTableLayout.GetControlFromPosition(j, i).Location.X, labwareVolumesTableLayout.GetControlFromPosition(j, i).Location.Y))
                    {
                        labwareVolumesTableLayout.GetControlFromPosition(j, i).BackgroundImage = selectedButtonBackground;
                        wellsSelected[i, j] = true;
                    }
                }
            }
            selectionBox.Width = 0;
            selectionBox.Height = 0;
            this.Invalidate();
            splitContainer1.Panel1.Refresh();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
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
                if(x > startPoint.X && x < endPoint.X && y > startPoint.Y && y < endPoint.Y)
                {
                    return true;
                }
            }
            else if(startPoint.X > endPoint.X && startPoint.Y < endPoint.Y)
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
            double setVolume = 1.0;

            if (this.labwareType != "tipbox")
            {
                setVolume = Convert.ToDouble(inputVolumesTextBox.Text);
            }

            if (setVolume > maxVolume)
            {
                DialogResult msg = MessageBox.Show("Maximum volume exceeded: " + maxVolume + "uL", " Error: Exceeded Maximum Volume", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (wellsSelected[i, j] == true && Convert.ToDouble(setVolume) > 0)
                        {
                            this.volumes[i, j] = Convert.ToDouble(setVolume);
                            labwareVolumesTableLayout.GetControlFromPosition(j, i).BackgroundImage = fullButtonBackground;
                            wellsSelected[i, j] = false;

                            if (this.labwareType != "tipbox")
                            {
                                labwareVolumesTableLayout.GetControlFromPosition(j, i).Text = this.volumes[i, j].ToString();
                            }
                        }
                        else if (wellsSelected[i, j] == true && Convert.ToDouble(setVolume) == 0)
                        {
                            this.volumes[i, j] = Convert.ToDouble(setVolume);
                            labwareVolumesTableLayout.GetControlFromPosition(j, i).BackgroundImage = emptyButtonBackground;
                            wellsSelected[i, j] = false;

                            if (this.labwareType != "tipbox")
                            {
                                labwareVolumesTableLayout.GetControlFromPosition(j, i).Text = this.volumes[i, j].ToString();
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
                    labwareVolumesTableLayout.GetControlFromPosition(j, i).BackgroundImage = selectedButtonBackground;
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
                        this.volumes[i, j] = 0.0;
                        labwareVolumesTableLayout.GetControlFromPosition(j, i).BackgroundImage = emptyButtonBackground;
                        wellsSelected[i, j] = false;

                        if (this.labwareType != "tipbox")
                        {
                            labwareVolumesTableLayout.GetControlFromPosition(j, i).Text = this.volumes[i, j].ToString();
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
                            labwareVolumesTableLayout.GetControlFromPosition(j, i).BackgroundImage = fullButtonBackground;
                        }
                        else
                        {
                            labwareVolumesTableLayout.GetControlFromPosition(j, i).BackgroundImage = emptyButtonBackground;
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
    }
}
