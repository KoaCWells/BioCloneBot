namespace BioCloneBot
{
    partial class LabwarePropertiesForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.setVolumeButton = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.inputVolumesTextBox = new System.Windows.Forms.TextBox();
            this.emptyRemoveButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1MinSize = 800;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(1307, 684);
            this.splitContainer1.SplitterDistance = 800;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.setVolumeButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.selectAllButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.inputVolumesTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.emptyRemoveButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.OKButton, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.cancelButton, 0, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(500, 680);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // setVolumeButton
            // 
            this.setVolumeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.setVolumeButton.Location = new System.Drawing.Point(3, 2);
            this.setVolumeButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.setVolumeButton.Name = "setVolumeButton";
            this.setVolumeButton.Size = new System.Drawing.Size(244, 35);
            this.setVolumeButton.TabIndex = 17;
            this.setVolumeButton.Text = "Set Volume";
            this.setVolumeButton.UseVisualStyleBackColor = true;
            this.setVolumeButton.Click += new System.EventHandler(this.setVolumesButton_Click);
            // 
            // selectAllButton
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.selectAllButton, 2);
            this.selectAllButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectAllButton.Location = new System.Drawing.Point(4, 43);
            this.selectAllButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(492, 31);
            this.selectAllButton.TabIndex = 21;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseVisualStyleBackColor = true;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // inputVolumesTextBox
            // 
            this.inputVolumesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.inputVolumesTextBox.Location = new System.Drawing.Point(253, 8);
            this.inputVolumesTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.inputVolumesTextBox.Name = "inputVolumesTextBox";
            this.inputVolumesTextBox.Size = new System.Drawing.Size(244, 22);
            this.inputVolumesTextBox.TabIndex = 22;
            // 
            // emptyRemoveButton
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.emptyRemoveButton, 2);
            this.emptyRemoveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emptyRemoveButton.Location = new System.Drawing.Point(3, 80);
            this.emptyRemoveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.emptyRemoveButton.Name = "emptyRemoveButton";
            this.emptyRemoveButton.Size = new System.Drawing.Size(494, 35);
            this.emptyRemoveButton.TabIndex = 18;
            this.emptyRemoveButton.Text = "Empty/Remove";
            this.emptyRemoveButton.UseVisualStyleBackColor = true;
            this.emptyRemoveButton.Click += new System.EventHandler(this.emptyRemoveButton_Click);
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Location = new System.Drawing.Point(253, 644);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(244, 34);
            this.OKButton.TabIndex = 18;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(3, 644);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(244, 34);
            this.cancelButton.TabIndex = 23;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // LabwarePropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 684);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LabwarePropertiesForm";
            this.Text = "Form1";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LabwarePropertiesForm_KeyUp);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button setVolumeButton;
        private System.Windows.Forms.Button emptyRemoveButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.TextBox inputVolumesTextBox;
        private System.Windows.Forms.Button cancelButton;
    }
}