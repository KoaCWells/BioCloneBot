namespace BioCloneBot
{
    partial class ManualControlForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.yMinusButton = new System.Windows.Forms.Button();
            this.xMinusButton = new System.Windows.Forms.Button();
            this.xPlusButton = new System.Windows.Forms.Button();
            this.yPlusButton = new System.Windows.Forms.Button();
            this.zPlusButton = new System.Windows.Forms.Button();
            this.zMinusButton = new System.Windows.Forms.Button();
            this.inputDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.travelDistanceTextBox = new System.Windows.Forms.TextBox();
            this.aspirateButton = new System.Windows.Forms.Button();
            this.dispenseButton = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.yMinusButton, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.xMinusButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.xPlusButton, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.yPlusButton, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.zPlusButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.zMinusButton, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.inputDistanceTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.travelDistanceTextBox, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.aspirateButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dispenseButton, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 449);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // yMinusButton
            // 
            this.yMinusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.yMinusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yMinusButton.Location = new System.Drawing.Point(269, 339);
            this.yMinusButton.Name = "yMinusButton";
            this.yMinusButton.Size = new System.Drawing.Size(260, 107);
            this.yMinusButton.TabIndex = 3;
            this.yMinusButton.Text = "y-";
            this.yMinusButton.UseVisualStyleBackColor = true;
            this.yMinusButton.Click += new System.EventHandler(this.moveAxis_onClick);
            // 
            // xMinusButton
            // 
            this.xMinusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xMinusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMinusButton.Location = new System.Drawing.Point(3, 339);
            this.xMinusButton.Name = "xMinusButton";
            this.xMinusButton.Size = new System.Drawing.Size(260, 107);
            this.xMinusButton.TabIndex = 0;
            this.xMinusButton.Text = "x-";
            this.xMinusButton.UseVisualStyleBackColor = true;
            this.xMinusButton.Click += new System.EventHandler(this.moveAxis_onClick);
            // 
            // xPlusButton
            // 
            this.xPlusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xPlusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xPlusButton.Location = new System.Drawing.Point(3, 227);
            this.xPlusButton.Name = "xPlusButton";
            this.xPlusButton.Size = new System.Drawing.Size(260, 106);
            this.xPlusButton.TabIndex = 2;
            this.xPlusButton.Text = "x+";
            this.xPlusButton.UseVisualStyleBackColor = true;
            this.xPlusButton.Click += new System.EventHandler(this.moveAxis_onClick);
            // 
            // yPlusButton
            // 
            this.yPlusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.yPlusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yPlusButton.Location = new System.Drawing.Point(269, 227);
            this.yPlusButton.Name = "yPlusButton";
            this.yPlusButton.Size = new System.Drawing.Size(260, 106);
            this.yPlusButton.TabIndex = 1;
            this.yPlusButton.Text = "y+";
            this.yPlusButton.UseVisualStyleBackColor = true;
            this.yPlusButton.Click += new System.EventHandler(this.moveAxis_onClick);
            // 
            // zPlusButton
            // 
            this.zPlusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zPlusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zPlusButton.Location = new System.Drawing.Point(535, 227);
            this.zPlusButton.Name = "zPlusButton";
            this.zPlusButton.Size = new System.Drawing.Size(262, 106);
            this.zPlusButton.TabIndex = 10;
            this.zPlusButton.Text = "z+";
            this.zPlusButton.UseVisualStyleBackColor = true;
            this.zPlusButton.Click += new System.EventHandler(this.moveAxis_onClick);
            // 
            // zMinusButton
            // 
            this.zMinusButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zMinusButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zMinusButton.Location = new System.Drawing.Point(535, 339);
            this.zMinusButton.Name = "zMinusButton";
            this.zMinusButton.Size = new System.Drawing.Size(262, 107);
            this.zMinusButton.TabIndex = 7;
            this.zMinusButton.Text = "z-";
            this.zMinusButton.UseVisualStyleBackColor = true;
            this.zMinusButton.Click += new System.EventHandler(this.moveAxis_onClick);
            // 
            // inputDistanceTextBox
            // 
            this.inputDistanceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.inputDistanceTextBox.Location = new System.Drawing.Point(269, 155);
            this.inputDistanceTextBox.Name = "inputDistanceTextBox";
            this.inputDistanceTextBox.Size = new System.Drawing.Size(260, 26);
            this.inputDistanceTextBox.TabIndex = 5;
            this.inputDistanceTextBox.Text = "inputDistance";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(270, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 104);
            this.label1.TabIndex = 11;
            this.label1.Text = "Manual Control";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // travelDistanceTextBox
            // 
            this.travelDistanceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.travelDistanceTextBox.Location = new System.Drawing.Point(4, 155);
            this.travelDistanceTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.travelDistanceTextBox.Name = "travelDistanceTextBox";
            this.travelDistanceTextBox.ReadOnly = true;
            this.travelDistanceTextBox.Size = new System.Drawing.Size(258, 26);
            this.travelDistanceTextBox.TabIndex = 12;
            this.travelDistanceTextBox.Text = "Travel Distance (mm):";
            // 
            // aspirateButton
            // 
            this.aspirateButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aspirateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aspirateButton.Location = new System.Drawing.Point(535, 3);
            this.aspirateButton.Name = "aspirateButton";
            this.aspirateButton.Size = new System.Drawing.Size(262, 106);
            this.aspirateButton.TabIndex = 13;
            this.aspirateButton.Text = "aspirate";
            this.aspirateButton.UseVisualStyleBackColor = true;
            this.aspirateButton.Click += new System.EventHandler(this.aspirate_Click);
            // 
            // dispenseButton
            // 
            this.dispenseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dispenseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dispenseButton.Location = new System.Drawing.Point(535, 115);
            this.dispenseButton.Name = "dispenseButton";
            this.dispenseButton.Size = new System.Drawing.Size(262, 106);
            this.dispenseButton.TabIndex = 13;
            this.dispenseButton.Text = "dispense";
            this.dispenseButton.UseVisualStyleBackColor = true;
            this.dispenseButton.Click += new System.EventHandler(this.dispense_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(179, 206);
            this.button6.Margin = new System.Windows.Forms.Padding(2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(173, 64);
            this.button6.TabIndex = 9;
            this.button6.Text = "x+";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // ManualControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 449);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ManualControlForm";
            this.Text = "Manual Control Debug";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button xMinusButton;
        private System.Windows.Forms.Button yPlusButton;
        private System.Windows.Forms.Button xPlusButton;
        private System.Windows.Forms.Button yMinusButton;
        private System.Windows.Forms.TextBox inputDistanceTextBox;
        private System.Windows.Forms.Button zPlusButton;
        private System.Windows.Forms.Button zMinusButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox travelDistanceTextBox;
        private System.Windows.Forms.Button aspirateButton;
        private System.Windows.Forms.Button dispenseButton;
    }
}