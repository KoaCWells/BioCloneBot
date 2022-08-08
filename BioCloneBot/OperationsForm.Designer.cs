namespace BioCloneBot
{
    partial class OperationsForm
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
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.volumeLabelTextBox = new System.Windows.Forms.TextBox();
            this.inputVolumeTextBox = new System.Windows.Forms.TextBox();
            this.mixCountLabelTextBox = new System.Windows.Forms.TextBox();
            this.mixCountInputTextBox = new System.Windows.Forms.TextBox();
            this.airGapCheckBox = new System.Windows.Forms.CheckBox();
            this.operationTextBox = new System.Windows.Forms.TextBox();
            this.confirmationButton = new System.Windows.Forms.Button();
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(1878, 1077);
            this.splitContainer1.SplitterDistance = 800;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.28729F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.71271F));
            this.tableLayoutPanel2.Controls.Add(this.OKButton, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.CancelButton, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.volumeLabelTextBox, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.inputVolumeTextBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.mixCountLabelTextBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.mixCountInputTextBox, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.airGapCheckBox, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.operationTextBox, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.confirmationButton, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1071, 1073);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OKButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.OKButton.Location = new System.Drawing.Point(627, 1025);
            this.OKButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(441, 44);
            this.OKButton.TabIndex = 0;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CancelButton.Location = new System.Drawing.Point(3, 1025);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(618, 44);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // volumeLabelTextBox
            // 
            this.volumeLabelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeLabelTextBox.Location = new System.Drawing.Point(5, 6);
            this.volumeLabelTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.volumeLabelTextBox.Name = "volumeLabelTextBox";
            this.volumeLabelTextBox.ReadOnly = true;
            this.volumeLabelTextBox.Size = new System.Drawing.Size(614, 31);
            this.volumeLabelTextBox.TabIndex = 5;
            this.volumeLabelTextBox.Text = "volumeLabelTextBox";
            // 
            // inputVolumeTextBox
            // 
            this.inputVolumeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.inputVolumeTextBox.Location = new System.Drawing.Point(629, 15);
            this.inputVolumeTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.inputVolumeTextBox.Name = "inputVolumeTextBox";
            this.inputVolumeTextBox.Size = new System.Drawing.Size(437, 31);
            this.inputVolumeTextBox.TabIndex = 1;
            this.inputVolumeTextBox.Text = "inputVolume";
            // 
            // mixCountLabelTextBox
            // 
            this.mixCountLabelTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mixCountLabelTextBox.Location = new System.Drawing.Point(5, 68);
            this.mixCountLabelTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.mixCountLabelTextBox.Name = "mixCountLabelTextBox";
            this.mixCountLabelTextBox.ReadOnly = true;
            this.mixCountLabelTextBox.Size = new System.Drawing.Size(614, 31);
            this.mixCountLabelTextBox.TabIndex = 5;
            this.mixCountLabelTextBox.Text = "mixCountLabelTextBox";
            // 
            // mixCountInputTextBox
            // 
            this.mixCountInputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mixCountInputTextBox.Location = new System.Drawing.Point(629, 68);
            this.mixCountInputTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.mixCountInputTextBox.Name = "mixCountInputTextBox";
            this.mixCountInputTextBox.Size = new System.Drawing.Size(437, 31);
            this.mixCountInputTextBox.TabIndex = 5;
            this.mixCountInputTextBox.Text = "mixCountInputTextBox";
            // 
            // airGapCheckBox
            // 
            this.airGapCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.airGapCheckBox.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.airGapCheckBox, 2);
            this.airGapCheckBox.Location = new System.Drawing.Point(5, 130);
            this.airGapCheckBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.airGapCheckBox.Name = "airGapCheckBox";
            this.airGapCheckBox.Size = new System.Drawing.Size(1061, 50);
            this.airGapCheckBox.TabIndex = 4;
            this.airGapCheckBox.Text = "25 uL Trailing Air Gap";
            this.airGapCheckBox.UseVisualStyleBackColor = true;
            // 
            // operationTextBox
            // 
            this.operationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.operationTextBox, 2);
            this.operationTextBox.Location = new System.Drawing.Point(5, 263);
            this.operationTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.operationTextBox.Name = "operationTextBox";
            this.operationTextBox.ReadOnly = true;
            this.operationTextBox.Size = new System.Drawing.Size(1061, 31);
            this.operationTextBox.TabIndex = 2;
            this.operationTextBox.Text = "operation";
            // 
            // confirmationButton
            // 
            this.confirmationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.confirmationButton, 2);
            this.confirmationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.confirmationButton.Location = new System.Drawing.Point(5, 192);
            this.confirmationButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.confirmationButton.Name = "confirmationButton";
            this.confirmationButton.Size = new System.Drawing.Size(1061, 50);
            this.confirmationButton.TabIndex = 0;
            this.confirmationButton.Text = "Confirm";
            this.confirmationButton.UseVisualStyleBackColor = true;
            this.confirmationButton.Click += new System.EventHandler(this.confirmationButton_Click);
            // 
            // OperationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1878, 1077);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "OperationsForm";
            this.Text = "AspirateForm";
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox inputVolumeTextBox;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button confirmationButton;
        private System.Windows.Forms.TextBox operationTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.CheckBox airGapCheckBox;
        private System.Windows.Forms.TextBox mixCountLabelTextBox;
        private System.Windows.Forms.TextBox mixCountInputTextBox;
        private System.Windows.Forms.TextBox volumeLabelTextBox;
    }
}