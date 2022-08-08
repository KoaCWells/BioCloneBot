
using System.Collections.Generic;
using System.Windows.Forms;

namespace BioCloneBot
{
    partial class ControlForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveExperimentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.manuallyMovePumpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripDropDownButton();
            this.home = new System.Windows.Forms.Button();
            this.closeSerialPort = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.protocolLabel = new System.Windows.Forms.Label();
            this.commandListTextBox = new System.Windows.Forms.TextBox();
            this.startExperiment = new System.Windows.Forms.Button();
            this.saveProtocolButton = new System.Windows.Forms.Button();
            this.loadSample = new System.Windows.Forms.Button();
            this.loadProtocolButton = new System.Windows.Forms.Button();
            this.clearProtocolButton = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.operationsLabel = new System.Windows.Forms.Label();
            this.homeDeviceOperationButton = new System.Windows.Forms.Button();
            this.getTipOperationButton = new System.Windows.Forms.Button();
            this.removeTipOperationButton = new System.Windows.Forms.Button();
            this.aspirateOperationButton = new System.Windows.Forms.Button();
            this.dispenseOperationButton = new System.Windows.Forms.Button();
            this.mixOperationButton = new System.Windows.Forms.Button();
            this.operationsTBD2Button = new System.Windows.Forms.Button();
            this.operationsTBD3Button = new System.Windows.Forms.Button();
            this.operationsTBD4Button = new System.Windows.Forms.Button();
            this.operationsTBD5Button = new System.Windows.Forms.Button();
            this.operationsTBD6Button = new System.Windows.Forms.Button();
            this.operationsTBD7Button = new System.Windows.Forms.Button();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.labwareLabel = new System.Windows.Forms.Label();
            this.wellplateButton = new System.Windows.Forms.Button();
            this.tubeStandButton = new System.Windows.Forms.Button();
            this.tipBoxButton = new System.Windows.Forms.Button();
            this.labwareTBD1Button = new System.Windows.Forms.Button();
            this.labwareTBD2Button = new System.Windows.Forms.Button();
            this.labwareTBD3Button = new System.Windows.Forms.Button();
            this.labwareTBD4Button = new System.Windows.Forms.Button();
            this.labwareTBD5Button = new System.Windows.Forms.Button();
            this.labwareTBD6Button = new System.Windows.Forms.Button();
            this.labwareTBD7Button = new System.Windows.Forms.Button();
            this.labwareTBD8Button = new System.Windows.Forms.Button();
            this.labwareTBD9Button = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.platformLabel = new System.Windows.Forms.Label();
            this.thermocyclerButton = new System.Windows.Forms.Button();
            this.trashButton = new System.Windows.Forms.Button();
            this.labwareButton1 = new System.Windows.Forms.Button();
            this.labwareMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labware1PropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLabware1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labwareButton2 = new System.Windows.Forms.Button();
            this.labware2MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labware2PropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLabware2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labwareButton3 = new System.Windows.Forms.Button();
            this.labware3MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labware3PropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLabware3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labwareButton4 = new System.Windows.Forms.Button();
            this.labware4MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labware4PropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeLabware4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.labwareMenuStrip.SuspendLayout();
            this.labware2MenuStrip.SuspendLayout();
            this.labware3MenuStrip.SuspendLayout();
            this.labware4MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripDropDownButton1,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(2973, 34);
            this.toolStrip1.TabIndex = 8;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveExperimentToolStripMenuItem});
            this.toolStripButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(56, 29);
            this.toolStripButton1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.openToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(251, 34);
            this.openToolStripMenuItem.Text = "Open Experiment";
            // 
            // saveExperimentToolStripMenuItem
            // 
            this.saveExperimentToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.saveExperimentToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.saveExperimentToolStripMenuItem.Name = "saveExperimentToolStripMenuItem";
            this.saveExperimentToolStripMenuItem.Size = new System.Drawing.Size(251, 34);
            this.saveExperimentToolStripMenuItem.Text = "Save Experiment";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manuallyMovePumpToolStripMenuItem});
            this.toolStripDropDownButton1.ForeColor = System.Drawing.Color.White;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(94, 29);
            this.toolStripDropDownButton1.Text = "Settings";
            // 
            // manuallyMovePumpToolStripMenuItem
            // 
            this.manuallyMovePumpToolStripMenuItem.Name = "manuallyMovePumpToolStripMenuItem";
            this.manuallyMovePumpToolStripMenuItem.Size = new System.Drawing.Size(288, 34);
            this.manuallyMovePumpToolStripMenuItem.Text = "Manually move pump";
            this.manuallyMovePumpToolStripMenuItem.Click += new System.EventHandler(this.manuallyMovePumpToolStripMenuItem_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.ForeColor = System.Drawing.Color.White;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(67, 29);
            this.toolStripButton3.Text = "Help";
            // 
            // home
            // 
            this.home.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.home.AutoSize = true;
            this.home.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.home.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.home.ForeColor = System.Drawing.Color.White;
            this.home.Location = new System.Drawing.Point(5, 1401);
            this.home.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.home.MinimumSize = new System.Drawing.Size(237, 150);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(294, 162);
            this.home.TabIndex = 15;
            this.home.Text = "Home Device";
            this.home.UseVisualStyleBackColor = false;
            this.home.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // closeSerialPort
            // 
            this.closeSerialPort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.closeSerialPort.AutoSize = true;
            this.closeSerialPort.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.closeSerialPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.closeSerialPort.ForeColor = System.Drawing.Color.White;
            this.closeSerialPort.Location = new System.Drawing.Point(309, 1401);
            this.closeSerialPort.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.closeSerialPort.MinimumSize = new System.Drawing.Size(237, 150);
            this.closeSerialPort.Name = "closeSerialPort";
            this.closeSerialPort.Size = new System.Drawing.Size(295, 162);
            this.closeSerialPort.TabIndex = 16;
            this.closeSerialPort.Text = "Reconnect Arduino";
            this.closeSerialPort.UseVisualStyleBackColor = false;
            this.closeSerialPort.Click += new System.EventHandler(this.reconnect_Arduino_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(2973, 1751);
            this.splitContainer1.SplitterDistance = 613;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 18;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.protocolLabel, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.commandListTextBox, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.startExperiment, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.saveProtocolButton, 0, 5);
            this.tableLayoutPanel4.Controls.Add(this.loadSample, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.loadProtocolButton, 1, 5);
            this.tableLayoutPanel4.Controls.Add(this.closeSerialPort, 1, 4);
            this.tableLayoutPanel4.Controls.Add(this.home, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.clearProtocolButton, 1, 2);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 6;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(609, 1747);
            this.tableLayoutPanel4.TabIndex = 21;
            // 
            // protocolLabel
            // 
            this.protocolLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.SetColumnSpan(this.protocolLabel, 2);
            this.protocolLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.protocolLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.protocolLabel.Location = new System.Drawing.Point(5, 0);
            this.protocolLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.protocolLabel.Name = "protocolLabel";
            this.protocolLabel.Size = new System.Drawing.Size(599, 174);
            this.protocolLabel.TabIndex = 21;
            this.protocolLabel.Text = "Protocol Queue";
            this.protocolLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // commandListTextBox
            // 
            this.commandListTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tableLayoutPanel4.SetColumnSpan(this.commandListTextBox, 2);
            this.commandListTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.commandListTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandListTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.commandListTextBox.Location = new System.Drawing.Point(5, 180);
            this.commandListTextBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.commandListTextBox.Multiline = true;
            this.commandListTextBox.Name = "commandListTextBox";
            this.commandListTextBox.ReadOnly = true;
            this.commandListTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commandListTextBox.Size = new System.Drawing.Size(599, 861);
            this.commandListTextBox.TabIndex = 18;
            // 
            // startExperiment
            // 
            this.startExperiment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startExperiment.AutoSize = true;
            this.startExperiment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.startExperiment.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.startExperiment.ForeColor = System.Drawing.Color.White;
            this.startExperiment.Location = new System.Drawing.Point(5, 1053);
            this.startExperiment.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.startExperiment.MinimumSize = new System.Drawing.Size(237, 150);
            this.startExperiment.Name = "startExperiment";
            this.startExperiment.Size = new System.Drawing.Size(294, 162);
            this.startExperiment.TabIndex = 19;
            this.startExperiment.Text = "Start Experiment";
            this.startExperiment.UseVisualStyleBackColor = false;
            this.startExperiment.Click += new System.EventHandler(this.startExperimentButton_Click);
            // 
            // saveProtocolButton
            // 
            this.saveProtocolButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.saveProtocolButton.AutoSize = true;
            this.saveProtocolButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.saveProtocolButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saveProtocolButton.ForeColor = System.Drawing.Color.White;
            this.saveProtocolButton.Location = new System.Drawing.Point(5, 1575);
            this.saveProtocolButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.saveProtocolButton.MinimumSize = new System.Drawing.Size(237, 150);
            this.saveProtocolButton.Name = "saveProtocolButton";
            this.saveProtocolButton.Size = new System.Drawing.Size(294, 166);
            this.saveProtocolButton.TabIndex = 22;
            this.saveProtocolButton.Text = "Save Protocol";
            this.saveProtocolButton.UseVisualStyleBackColor = false;
            this.saveProtocolButton.Click += new System.EventHandler(this.saveProtocolButton_Click);
            // 
            // loadSample
            // 
            this.loadSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadSample.AutoSize = true;
            this.loadSample.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.loadSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.loadSample.ForeColor = System.Drawing.Color.White;
            this.loadSample.Location = new System.Drawing.Point(5, 1227);
            this.loadSample.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.loadSample.MinimumSize = new System.Drawing.Size(237, 150);
            this.loadSample.Name = "loadSample";
            this.loadSample.Size = new System.Drawing.Size(294, 162);
            this.loadSample.TabIndex = 20;
            this.loadSample.Text = "Load Sample Experiment";
            this.loadSample.UseVisualStyleBackColor = false;
            this.loadSample.Click += new System.EventHandler(this.loadSample_Click);
            // 
            // loadProtocolButton
            // 
            this.loadProtocolButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loadProtocolButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.loadProtocolButton.ForeColor = System.Drawing.Color.White;
            this.loadProtocolButton.Location = new System.Drawing.Point(309, 1575);
            this.loadProtocolButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.loadProtocolButton.Name = "loadProtocolButton";
            this.loadProtocolButton.Size = new System.Drawing.Size(295, 166);
            this.loadProtocolButton.TabIndex = 23;
            this.loadProtocolButton.Text = "Load Protocol";
            this.loadProtocolButton.UseVisualStyleBackColor = false;
            this.loadProtocolButton.Click += new System.EventHandler(this.loadProtocolButton_Click);
            // 
            // clearProtocolButton
            // 
            this.clearProtocolButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clearProtocolButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.clearProtocolButton.ForeColor = System.Drawing.Color.White;
            this.clearProtocolButton.Location = new System.Drawing.Point(309, 1053);
            this.clearProtocolButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.clearProtocolButton.Name = "clearProtocolButton";
            this.clearProtocolButton.Size = new System.Drawing.Size(295, 162);
            this.clearProtocolButton.TabIndex = 24;
            this.clearProtocolButton.Text = "Clear Protocol";
            this.clearProtocolButton.UseVisualStyleBackColor = false;
            this.clearProtocolButton.Click += new System.EventHandler(this.clearProtocolButton_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(2357, 1751);
            this.splitContainer2.SplitterDistance = 223;
            this.splitContainer2.SplitterWidth = 3;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.operationsLabel, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.homeDeviceOperationButton, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.getTipOperationButton, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.removeTipOperationButton, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.aspirateOperationButton, 0, 4);
            this.tableLayoutPanel5.Controls.Add(this.dispenseOperationButton, 0, 5);
            this.tableLayoutPanel5.Controls.Add(this.mixOperationButton, 0, 6);
            this.tableLayoutPanel5.Controls.Add(this.operationsTBD2Button, 0, 7);
            this.tableLayoutPanel5.Controls.Add(this.operationsTBD3Button, 0, 8);
            this.tableLayoutPanel5.Controls.Add(this.operationsTBD4Button, 0, 9);
            this.tableLayoutPanel5.Controls.Add(this.operationsTBD5Button, 0, 10);
            this.tableLayoutPanel5.Controls.Add(this.operationsTBD6Button, 0, 11);
            this.tableLayoutPanel5.Controls.Add(this.operationsTBD7Button, 0, 12);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 13;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(219, 1747);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // operationsLabel
            // 
            this.operationsLabel.AutoSize = true;
            this.operationsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.operationsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.operationsLabel.Location = new System.Drawing.Point(3, 0);
            this.operationsLabel.Name = "operationsLabel";
            this.operationsLabel.Size = new System.Drawing.Size(213, 174);
            this.operationsLabel.TabIndex = 20;
            this.operationsLabel.Text = "Operations";
            this.operationsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // homeDeviceOperationButton
            // 
            this.homeDeviceOperationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.homeDeviceOperationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.homeDeviceOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.homeDeviceOperationButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.homeDeviceOperationButton.Location = new System.Drawing.Point(3, 178);
            this.homeDeviceOperationButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.homeDeviceOperationButton.Name = "homeDeviceOperationButton";
            this.homeDeviceOperationButton.Size = new System.Drawing.Size(213, 123);
            this.homeDeviceOperationButton.TabIndex = 7;
            this.homeDeviceOperationButton.Text = "Home Device";
            this.homeDeviceOperationButton.UseVisualStyleBackColor = false;
            this.homeDeviceOperationButton.Click += new System.EventHandler(this.homeDeviceOperationButton_Click);
            // 
            // getTipOperationButton
            // 
            this.getTipOperationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.getTipOperationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.getTipOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.getTipOperationButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.getTipOperationButton.Location = new System.Drawing.Point(3, 309);
            this.getTipOperationButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.getTipOperationButton.Name = "getTipOperationButton";
            this.getTipOperationButton.Size = new System.Drawing.Size(213, 123);
            this.getTipOperationButton.TabIndex = 9;
            this.getTipOperationButton.Text = "Get Tip";
            this.getTipOperationButton.UseVisualStyleBackColor = false;
            this.getTipOperationButton.Click += new System.EventHandler(this.getTipOperationButton_Click);
            // 
            // removeTipOperationButton
            // 
            this.removeTipOperationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeTipOperationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.removeTipOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.removeTipOperationButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.removeTipOperationButton.Location = new System.Drawing.Point(3, 440);
            this.removeTipOperationButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.removeTipOperationButton.Name = "removeTipOperationButton";
            this.removeTipOperationButton.Size = new System.Drawing.Size(213, 123);
            this.removeTipOperationButton.TabIndex = 10;
            this.removeTipOperationButton.Text = "Remove Tip";
            this.removeTipOperationButton.UseVisualStyleBackColor = false;
            this.removeTipOperationButton.Click += new System.EventHandler(this.removeTipOperationButton_Click);
            // 
            // aspirateOperationButton
            // 
            this.aspirateOperationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.aspirateOperationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.aspirateOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.aspirateOperationButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.aspirateOperationButton.Location = new System.Drawing.Point(3, 571);
            this.aspirateOperationButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.aspirateOperationButton.Name = "aspirateOperationButton";
            this.aspirateOperationButton.Size = new System.Drawing.Size(213, 123);
            this.aspirateOperationButton.TabIndex = 11;
            this.aspirateOperationButton.Text = "Aspirate";
            this.aspirateOperationButton.UseVisualStyleBackColor = false;
            this.aspirateOperationButton.Click += new System.EventHandler(this.aspirateOperationButton_Click);
            // 
            // dispenseOperationButton
            // 
            this.dispenseOperationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dispenseOperationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dispenseOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.dispenseOperationButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.dispenseOperationButton.Location = new System.Drawing.Point(3, 702);
            this.dispenseOperationButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dispenseOperationButton.Name = "dispenseOperationButton";
            this.dispenseOperationButton.Size = new System.Drawing.Size(213, 123);
            this.dispenseOperationButton.TabIndex = 12;
            this.dispenseOperationButton.Text = "Dispense";
            this.dispenseOperationButton.UseVisualStyleBackColor = false;
            this.dispenseOperationButton.Click += new System.EventHandler(this.dispenseOperationButton_Click);
            // 
            // mixOperationButton
            // 
            this.mixOperationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mixOperationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.mixOperationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mixOperationButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.mixOperationButton.Location = new System.Drawing.Point(3, 833);
            this.mixOperationButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.mixOperationButton.Name = "mixOperationButton";
            this.mixOperationButton.Size = new System.Drawing.Size(213, 123);
            this.mixOperationButton.TabIndex = 13;
            this.mixOperationButton.Text = "Mix";
            this.mixOperationButton.UseVisualStyleBackColor = false;
            this.mixOperationButton.Click += new System.EventHandler(this.mixOperationButton_Click);
            // 
            // operationsTBD2Button
            // 
            this.operationsTBD2Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.operationsTBD2Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.operationsTBD2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationsTBD2Button.ForeColor = System.Drawing.Color.White;
            this.operationsTBD2Button.Location = new System.Drawing.Point(3, 964);
            this.operationsTBD2Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.operationsTBD2Button.Name = "operationsTBD2Button";
            this.operationsTBD2Button.Size = new System.Drawing.Size(213, 123);
            this.operationsTBD2Button.TabIndex = 14;
            this.operationsTBD2Button.Text = "TBD";
            this.operationsTBD2Button.UseVisualStyleBackColor = false;
            // 
            // operationsTBD3Button
            // 
            this.operationsTBD3Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.operationsTBD3Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.operationsTBD3Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationsTBD3Button.ForeColor = System.Drawing.Color.White;
            this.operationsTBD3Button.Location = new System.Drawing.Point(3, 1095);
            this.operationsTBD3Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.operationsTBD3Button.Name = "operationsTBD3Button";
            this.operationsTBD3Button.Size = new System.Drawing.Size(213, 123);
            this.operationsTBD3Button.TabIndex = 15;
            this.operationsTBD3Button.Text = "TBD";
            this.operationsTBD3Button.UseVisualStyleBackColor = false;
            // 
            // operationsTBD4Button
            // 
            this.operationsTBD4Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.operationsTBD4Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.operationsTBD4Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationsTBD4Button.ForeColor = System.Drawing.Color.White;
            this.operationsTBD4Button.Location = new System.Drawing.Point(3, 1226);
            this.operationsTBD4Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.operationsTBD4Button.Name = "operationsTBD4Button";
            this.operationsTBD4Button.Size = new System.Drawing.Size(213, 123);
            this.operationsTBD4Button.TabIndex = 16;
            this.operationsTBD4Button.Text = "TBD";
            this.operationsTBD4Button.UseVisualStyleBackColor = false;
            // 
            // operationsTBD5Button
            // 
            this.operationsTBD5Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.operationsTBD5Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.operationsTBD5Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationsTBD5Button.ForeColor = System.Drawing.Color.White;
            this.operationsTBD5Button.Location = new System.Drawing.Point(3, 1357);
            this.operationsTBD5Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.operationsTBD5Button.Name = "operationsTBD5Button";
            this.operationsTBD5Button.Size = new System.Drawing.Size(213, 123);
            this.operationsTBD5Button.TabIndex = 17;
            this.operationsTBD5Button.Text = "TBD";
            this.operationsTBD5Button.UseVisualStyleBackColor = false;
            // 
            // operationsTBD6Button
            // 
            this.operationsTBD6Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.operationsTBD6Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.operationsTBD6Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationsTBD6Button.ForeColor = System.Drawing.Color.White;
            this.operationsTBD6Button.Location = new System.Drawing.Point(3, 1488);
            this.operationsTBD6Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.operationsTBD6Button.Name = "operationsTBD6Button";
            this.operationsTBD6Button.Size = new System.Drawing.Size(213, 123);
            this.operationsTBD6Button.TabIndex = 18;
            this.operationsTBD6Button.Text = "TBD";
            this.operationsTBD6Button.UseVisualStyleBackColor = false;
            // 
            // operationsTBD7Button
            // 
            this.operationsTBD7Button.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.operationsTBD7Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.operationsTBD7Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.operationsTBD7Button.ForeColor = System.Drawing.Color.White;
            this.operationsTBD7Button.Location = new System.Drawing.Point(3, 1619);
            this.operationsTBD7Button.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.operationsTBD7Button.Name = "operationsTBD7Button";
            this.operationsTBD7Button.Size = new System.Drawing.Size(213, 124);
            this.operationsTBD7Button.TabIndex = 19;
            this.operationsTBD7Button.Text = "TBD";
            this.operationsTBD7Button.UseVisualStyleBackColor = false;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer3.Size = new System.Drawing.Size(2131, 1751);
            this.splitContainer3.SplitterDistance = 223;
            this.splitContainer3.SplitterWidth = 3;
            this.splitContainer3.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.labwareLabel, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.wellplateButton, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tubeStandButton, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tipBoxButton, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.labwareTBD1Button, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.labwareTBD2Button, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.labwareTBD3Button, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.labwareTBD4Button, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.labwareTBD5Button, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.labwareTBD6Button, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.labwareTBD7Button, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.labwareTBD8Button, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.labwareTBD9Button, 0, 12);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tableLayoutPanel2.MinimumSize = new System.Drawing.Size(243, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 13;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(243, 1747);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // labwareLabel
            // 
            this.labwareLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labwareLabel.AutoSize = true;
            this.labwareLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labwareLabel.Location = new System.Drawing.Point(39, 50);
            this.labwareLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.labwareLabel.Name = "labwareLabel";
            this.labwareLabel.Size = new System.Drawing.Size(165, 74);
            this.labwareLabel.TabIndex = 0;
            this.labwareLabel.Text = "Available Labware";
            // 
            // wellplateButton
            // 
            this.wellplateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.wellplateButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wellplateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.wellplateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.wellplateButton.Location = new System.Drawing.Point(5, 180);
            this.wellplateButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.wellplateButton.Name = "wellplateButton";
            this.wellplateButton.Size = new System.Drawing.Size(233, 119);
            this.wellplateButton.TabIndex = 0;
            this.wellplateButton.Text = "96 Wellplate";
            this.wellplateButton.UseVisualStyleBackColor = false;
            this.wellplateButton.Click += new System.EventHandler(this.wellplate_Click);
            // 
            // tubeStandButton
            // 
            this.tubeStandButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tubeStandButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tubeStandButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tubeStandButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.tubeStandButton.Location = new System.Drawing.Point(5, 311);
            this.tubeStandButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tubeStandButton.Name = "tubeStandButton";
            this.tubeStandButton.Size = new System.Drawing.Size(233, 119);
            this.tubeStandButton.TabIndex = 1;
            this.tubeStandButton.Text = "5mL Eppendorf Tubes";
            this.tubeStandButton.UseVisualStyleBackColor = false;
            this.tubeStandButton.Click += new System.EventHandler(this.tubeStand_Click);
            // 
            // tipBoxButton
            // 
            this.tipBoxButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.tipBoxButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tipBoxButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tipBoxButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.tipBoxButton.Location = new System.Drawing.Point(5, 442);
            this.tipBoxButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tipBoxButton.Name = "tipBoxButton";
            this.tipBoxButton.Size = new System.Drawing.Size(233, 119);
            this.tipBoxButton.TabIndex = 2;
            this.tipBoxButton.Text = "200 uL Tip Box";
            this.tipBoxButton.UseVisualStyleBackColor = false;
            this.tipBoxButton.Click += new System.EventHandler(this.tipBox_Click);
            // 
            // labwareTBD1Button
            // 
            this.labwareTBD1Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.labwareTBD1Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwareTBD1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareTBD1Button.ForeColor = System.Drawing.Color.White;
            this.labwareTBD1Button.Location = new System.Drawing.Point(5, 573);
            this.labwareTBD1Button.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labwareTBD1Button.Name = "labwareTBD1Button";
            this.labwareTBD1Button.Size = new System.Drawing.Size(233, 119);
            this.labwareTBD1Button.TabIndex = 3;
            this.labwareTBD1Button.Text = "TBD";
            this.labwareTBD1Button.UseVisualStyleBackColor = false;
            // 
            // labwareTBD2Button
            // 
            this.labwareTBD2Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.labwareTBD2Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwareTBD2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareTBD2Button.ForeColor = System.Drawing.Color.White;
            this.labwareTBD2Button.Location = new System.Drawing.Point(5, 704);
            this.labwareTBD2Button.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labwareTBD2Button.Name = "labwareTBD2Button";
            this.labwareTBD2Button.Size = new System.Drawing.Size(233, 119);
            this.labwareTBD2Button.TabIndex = 4;
            this.labwareTBD2Button.Text = "TBD";
            this.labwareTBD2Button.UseVisualStyleBackColor = false;
            // 
            // labwareTBD3Button
            // 
            this.labwareTBD3Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.labwareTBD3Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwareTBD3Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareTBD3Button.ForeColor = System.Drawing.Color.White;
            this.labwareTBD3Button.Location = new System.Drawing.Point(5, 835);
            this.labwareTBD3Button.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labwareTBD3Button.Name = "labwareTBD3Button";
            this.labwareTBD3Button.Size = new System.Drawing.Size(233, 119);
            this.labwareTBD3Button.TabIndex = 5;
            this.labwareTBD3Button.Text = "TBD";
            this.labwareTBD3Button.UseVisualStyleBackColor = false;
            // 
            // labwareTBD4Button
            // 
            this.labwareTBD4Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.labwareTBD4Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwareTBD4Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareTBD4Button.ForeColor = System.Drawing.Color.White;
            this.labwareTBD4Button.Location = new System.Drawing.Point(5, 966);
            this.labwareTBD4Button.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labwareTBD4Button.Name = "labwareTBD4Button";
            this.labwareTBD4Button.Size = new System.Drawing.Size(233, 119);
            this.labwareTBD4Button.TabIndex = 6;
            this.labwareTBD4Button.Text = "TBD";
            this.labwareTBD4Button.UseVisualStyleBackColor = false;
            // 
            // labwareTBD5Button
            // 
            this.labwareTBD5Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.labwareTBD5Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwareTBD5Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareTBD5Button.ForeColor = System.Drawing.Color.White;
            this.labwareTBD5Button.Location = new System.Drawing.Point(5, 1097);
            this.labwareTBD5Button.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labwareTBD5Button.Name = "labwareTBD5Button";
            this.labwareTBD5Button.Size = new System.Drawing.Size(233, 119);
            this.labwareTBD5Button.TabIndex = 7;
            this.labwareTBD5Button.Text = "TBD";
            this.labwareTBD5Button.UseVisualStyleBackColor = false;
            // 
            // labwareTBD6Button
            // 
            this.labwareTBD6Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.labwareTBD6Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwareTBD6Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareTBD6Button.ForeColor = System.Drawing.Color.White;
            this.labwareTBD6Button.Location = new System.Drawing.Point(5, 1228);
            this.labwareTBD6Button.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labwareTBD6Button.Name = "labwareTBD6Button";
            this.labwareTBD6Button.Size = new System.Drawing.Size(233, 119);
            this.labwareTBD6Button.TabIndex = 8;
            this.labwareTBD6Button.Text = "TBD";
            this.labwareTBD6Button.UseVisualStyleBackColor = false;
            // 
            // labwareTBD7Button
            // 
            this.labwareTBD7Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.labwareTBD7Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwareTBD7Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareTBD7Button.ForeColor = System.Drawing.Color.White;
            this.labwareTBD7Button.Location = new System.Drawing.Point(5, 1359);
            this.labwareTBD7Button.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labwareTBD7Button.Name = "labwareTBD7Button";
            this.labwareTBD7Button.Size = new System.Drawing.Size(233, 119);
            this.labwareTBD7Button.TabIndex = 9;
            this.labwareTBD7Button.Text = "TBD";
            this.labwareTBD7Button.UseVisualStyleBackColor = false;
            // 
            // labwareTBD8Button
            // 
            this.labwareTBD8Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.labwareTBD8Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwareTBD8Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareTBD8Button.ForeColor = System.Drawing.Color.White;
            this.labwareTBD8Button.Location = new System.Drawing.Point(5, 1490);
            this.labwareTBD8Button.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labwareTBD8Button.Name = "labwareTBD8Button";
            this.labwareTBD8Button.Size = new System.Drawing.Size(233, 119);
            this.labwareTBD8Button.TabIndex = 10;
            this.labwareTBD8Button.Text = "TBD";
            this.labwareTBD8Button.UseVisualStyleBackColor = false;
            // 
            // labwareTBD9Button
            // 
            this.labwareTBD9Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.labwareTBD9Button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labwareTBD9Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareTBD9Button.ForeColor = System.Drawing.Color.White;
            this.labwareTBD9Button.Location = new System.Drawing.Point(5, 1621);
            this.labwareTBD9Button.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.labwareTBD9Button.Name = "labwareTBD9Button";
            this.labwareTBD9Button.Size = new System.Drawing.Size(233, 120);
            this.labwareTBD9Button.TabIndex = 11;
            this.labwareTBD9Button.Text = "TBD";
            this.labwareTBD9Button.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel1.Controls.Add(this.platformLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.thermocyclerButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.trashButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labwareButton1, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labwareButton2, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.labwareButton3, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.labwareButton4, 1, 9);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 13;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1901, 1747);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // platformLabel
            // 
            this.platformLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.platformLabel, 2);
            this.platformLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.platformLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.platformLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.platformLabel.Location = new System.Drawing.Point(5, 0);
            this.platformLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.platformLabel.Name = "platformLabel";
            this.platformLabel.Size = new System.Drawing.Size(1891, 174);
            this.platformLabel.TabIndex = 6;
            this.platformLabel.Text = "BioCloneBot Platform";
            this.platformLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // thermocyclerButton
            // 
            this.thermocyclerButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.thermocyclerButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.thermocyclerButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.thermocyclerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.thermocyclerButton.ForeColor = System.Drawing.Color.White;
            this.thermocyclerButton.Location = new System.Drawing.Point(288, 249);
            this.thermocyclerButton.Margin = new System.Windows.Forms.Padding(5, 6, 100, 6);
            this.thermocyclerButton.MaximumSize = new System.Drawing.Size(723, 373);
            this.thermocyclerButton.MinimumSize = new System.Drawing.Size(562, 373);
            this.thermocyclerButton.Name = "thermocyclerButton";
            this.tableLayoutPanel1.SetRowSpan(this.thermocyclerButton, 4);
            this.thermocyclerButton.Size = new System.Drawing.Size(562, 373);
            this.thermocyclerButton.TabIndex = 0;
            this.thermocyclerButton.Text = "Thermocycler";
            this.thermocyclerButton.UseVisualStyleBackColor = false;
            // 
            // trashButton
            // 
            this.trashButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.trashButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.trashButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.trashButton.ForeColor = System.Drawing.Color.White;
            this.trashButton.Location = new System.Drawing.Point(1050, 249);
            this.trashButton.Margin = new System.Windows.Forms.Padding(100, 6, 5, 6);
            this.trashButton.MaximumSize = new System.Drawing.Size(723, 373);
            this.trashButton.MinimumSize = new System.Drawing.Size(562, 373);
            this.trashButton.Name = "trashButton";
            this.tableLayoutPanel1.SetRowSpan(this.trashButton, 4);
            this.trashButton.Size = new System.Drawing.Size(562, 373);
            this.trashButton.TabIndex = 1;
            this.trashButton.Text = "Trash";
            this.trashButton.UseVisualStyleBackColor = false;
            // 
            // labwareButton1
            // 
            this.labwareButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labwareButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.labwareButton1.ContextMenuStrip = this.labwareMenuStrip;
            this.labwareButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labwareButton1.Location = new System.Drawing.Point(288, 773);
            this.labwareButton1.Margin = new System.Windows.Forms.Padding(5, 6, 100, 6);
            this.labwareButton1.MaximumSize = new System.Drawing.Size(723, 373);
            this.labwareButton1.MinimumSize = new System.Drawing.Size(562, 373);
            this.labwareButton1.Name = "labwareButton1";
            this.tableLayoutPanel1.SetRowSpan(this.labwareButton1, 4);
            this.labwareButton1.Size = new System.Drawing.Size(562, 373);
            this.labwareButton1.TabIndex = 2;
            this.labwareButton1.Text = "Labware 1";
            this.labwareButton1.UseVisualStyleBackColor = false;
            this.labwareButton1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labware_MouseDown);
            // 
            // labwareMenuStrip
            // 
            this.labwareMenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.labwareMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labware1PropertiesToolStripMenuItem,
            this.removeLabware1ToolStripMenuItem});
            this.labwareMenuStrip.Name = "rightClickMenuStrip";
            this.labwareMenuStrip.Size = new System.Drawing.Size(250, 68);
            this.labwareMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.labware1MenuStrip_Opening);
            // 
            // labware1PropertiesToolStripMenuItem
            // 
            this.labware1PropertiesToolStripMenuItem.Name = "labware1PropertiesToolStripMenuItem";
            this.labware1PropertiesToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.labware1PropertiesToolStripMenuItem.Text = "Labware 1 Properties";
            this.labware1PropertiesToolStripMenuItem.Click += new System.EventHandler(this.labware1PropertiesToolStripMenuItem_Click);
            // 
            // removeLabware1ToolStripMenuItem
            // 
            this.removeLabware1ToolStripMenuItem.Name = "removeLabware1ToolStripMenuItem";
            this.removeLabware1ToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.removeLabware1ToolStripMenuItem.Text = "Remove Labware 1";
            this.removeLabware1ToolStripMenuItem.Click += new System.EventHandler(this.removeLabware1ToolStripMenuItem_Click);
            // 
            // labwareButton2
            // 
            this.labwareButton2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labwareButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.labwareButton2.ContextMenuStrip = this.labware2MenuStrip;
            this.labwareButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareButton2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labwareButton2.Location = new System.Drawing.Point(1050, 773);
            this.labwareButton2.Margin = new System.Windows.Forms.Padding(100, 6, 5, 6);
            this.labwareButton2.MaximumSize = new System.Drawing.Size(723, 373);
            this.labwareButton2.MinimumSize = new System.Drawing.Size(562, 373);
            this.labwareButton2.Name = "labwareButton2";
            this.tableLayoutPanel1.SetRowSpan(this.labwareButton2, 4);
            this.labwareButton2.Size = new System.Drawing.Size(562, 373);
            this.labwareButton2.TabIndex = 3;
            this.labwareButton2.Text = "Labware 2";
            this.labwareButton2.UseVisualStyleBackColor = false;
            this.labwareButton2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labware_MouseDown);
            // 
            // labware2MenuStrip
            // 
            this.labware2MenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.labware2MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labware2PropertiesToolStripMenuItem,
            this.removeLabware2ToolStripMenuItem});
            this.labware2MenuStrip.Name = "labware2MenuStrip";
            this.labware2MenuStrip.Size = new System.Drawing.Size(250, 68);
            this.labware2MenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.labware2MenuStrip_Opening);
            // 
            // labware2PropertiesToolStripMenuItem
            // 
            this.labware2PropertiesToolStripMenuItem.Name = "labware2PropertiesToolStripMenuItem";
            this.labware2PropertiesToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.labware2PropertiesToolStripMenuItem.Text = "Labware 2 Properties";
            this.labware2PropertiesToolStripMenuItem.Click += new System.EventHandler(this.labware2PropertiesToolStripMenuItem_Click);
            // 
            // removeLabware2ToolStripMenuItem
            // 
            this.removeLabware2ToolStripMenuItem.Name = "removeLabware2ToolStripMenuItem";
            this.removeLabware2ToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.removeLabware2ToolStripMenuItem.Text = "Remove Labware 2";
            this.removeLabware2ToolStripMenuItem.Click += new System.EventHandler(this.removeLabware2ToolStripMenuItem_Click);
            // 
            // labwareButton3
            // 
            this.labwareButton3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labwareButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.labwareButton3.ContextMenuStrip = this.labware3MenuStrip;
            this.labwareButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareButton3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labwareButton3.Location = new System.Drawing.Point(288, 1298);
            this.labwareButton3.Margin = new System.Windows.Forms.Padding(5, 6, 100, 6);
            this.labwareButton3.MaximumSize = new System.Drawing.Size(723, 373);
            this.labwareButton3.MinimumSize = new System.Drawing.Size(562, 373);
            this.labwareButton3.Name = "labwareButton3";
            this.tableLayoutPanel1.SetRowSpan(this.labwareButton3, 4);
            this.labwareButton3.Size = new System.Drawing.Size(562, 373);
            this.labwareButton3.TabIndex = 4;
            this.labwareButton3.Text = "Labware 3";
            this.labwareButton3.UseVisualStyleBackColor = false;
            this.labwareButton3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labware_MouseDown);
            // 
            // labware3MenuStrip
            // 
            this.labware3MenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.labware3MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labware3PropertiesToolStripMenuItem,
            this.removeLabware3ToolStripMenuItem});
            this.labware3MenuStrip.Name = "contextMenuStrip2";
            this.labware3MenuStrip.Size = new System.Drawing.Size(250, 68);
            this.labware3MenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.labware3MenuStrip_Opening);
            // 
            // labware3PropertiesToolStripMenuItem
            // 
            this.labware3PropertiesToolStripMenuItem.Name = "labware3PropertiesToolStripMenuItem";
            this.labware3PropertiesToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.labware3PropertiesToolStripMenuItem.Text = "Labware 3 Properties";
            this.labware3PropertiesToolStripMenuItem.Click += new System.EventHandler(this.labware3ProperiesToolStripMenuItem_Click);
            // 
            // removeLabware3ToolStripMenuItem
            // 
            this.removeLabware3ToolStripMenuItem.Name = "removeLabware3ToolStripMenuItem";
            this.removeLabware3ToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.removeLabware3ToolStripMenuItem.Text = "Remove Labware 3";
            this.removeLabware3ToolStripMenuItem.Click += new System.EventHandler(this.removeLabware3ToolStripMenuItem_Click);
            // 
            // labwareButton4
            // 
            this.labwareButton4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labwareButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(20)))));
            this.labwareButton4.ContextMenuStrip = this.labware4MenuStrip;
            this.labwareButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labwareButton4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.labwareButton4.Location = new System.Drawing.Point(1050, 1298);
            this.labwareButton4.Margin = new System.Windows.Forms.Padding(100, 6, 5, 6);
            this.labwareButton4.MaximumSize = new System.Drawing.Size(723, 373);
            this.labwareButton4.MinimumSize = new System.Drawing.Size(562, 373);
            this.labwareButton4.Name = "labwareButton4";
            this.tableLayoutPanel1.SetRowSpan(this.labwareButton4, 4);
            this.labwareButton4.Size = new System.Drawing.Size(562, 373);
            this.labwareButton4.TabIndex = 5;
            this.labwareButton4.Text = "Labware 4";
            this.labwareButton4.UseVisualStyleBackColor = false;
            this.labwareButton4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labware_MouseDown);
            // 
            // labware4MenuStrip
            // 
            this.labware4MenuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.labware4MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labware4PropertiesToolStripMenuItem,
            this.removeLabware4ToolStripMenuItem});
            this.labware4MenuStrip.Name = "contextMenuStrip2";
            this.labware4MenuStrip.Size = new System.Drawing.Size(250, 68);
            this.labware4MenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.labware4MenuStrip_Opening);
            // 
            // labware4PropertiesToolStripMenuItem
            // 
            this.labware4PropertiesToolStripMenuItem.Name = "labware4PropertiesToolStripMenuItem";
            this.labware4PropertiesToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.labware4PropertiesToolStripMenuItem.Text = "Labware 4 Properties";
            this.labware4PropertiesToolStripMenuItem.Click += new System.EventHandler(this.labware4PropertiesToolStripMenuItem_Click);
            // 
            // removeLabware4ToolStripMenuItem
            // 
            this.removeLabware4ToolStripMenuItem.Name = "removeLabware4ToolStripMenuItem";
            this.removeLabware4ToolStripMenuItem.Size = new System.Drawing.Size(249, 32);
            this.removeLabware4ToolStripMenuItem.Text = "Remove Labware 4";
            this.removeLabware4ToolStripMenuItem.Click += new System.EventHandler(this.removeLabware4ToolStripMenuItem_Click);
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2973, 1785);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "ControlForm";
            this.Text = "BioCloneBot";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.labwareMenuStrip.ResumeLayout(false);
            this.labware2MenuStrip.ResumeLayout(false);
            this.labware3MenuStrip.ResumeLayout(false);
            this.labware4MenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveExperimentToolStripMenuItem;
        private System.Windows.Forms.Button home;
        private System.Windows.Forms.Button closeSerialPort;

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button labwareButton3;
        private System.Windows.Forms.Button labwareButton4;
        private System.Windows.Forms.Button labwareButton1;
        private System.Windows.Forms.Button labwareButton2;
        private System.Windows.Forms.Button thermocyclerButton;
        private System.Windows.Forms.Button trashButton;
        private System.Windows.Forms.Label platformLabel;
        private System.Windows.Forms.TextBox commandListTextBox;
        private System.Windows.Forms.Button startExperiment;
        private System.Windows.Forms.Button loadSample;
        private System.Windows.Forms.ContextMenuStrip labwareMenuStrip;
        private ToolStripMenuItem labware1PropertiesToolStripMenuItem;
        private ToolStripMenuItem removeLabware1ToolStripMenuItem;
        private ContextMenuStrip labware2MenuStrip;
        private ContextMenuStrip labware3MenuStrip;
        private ContextMenuStrip labware4MenuStrip;
        private ToolStripMenuItem labware2PropertiesToolStripMenuItem;
        private ToolStripMenuItem removeLabware2ToolStripMenuItem;
        private ToolStripMenuItem labware3PropertiesToolStripMenuItem;
        private ToolStripMenuItem removeLabware3ToolStripMenuItem;
        private ToolStripMenuItem labware4PropertiesToolStripMenuItem;
        private ToolStripMenuItem removeLabware4ToolStripMenuItem;
        private TableLayoutPanel tableLayoutPanel4;
        private Label protocolLabel;
        private TableLayoutPanel tableLayoutPanel5;
        private Button operationsTBD7Button;
        private Label operationsLabel;
        private Button operationsTBD6Button;
        private Button homeDeviceOperationButton;
        private Button operationsTBD5Button;
        private Button getTipOperationButton;
        private Button operationsTBD4Button;
        private Button removeTipOperationButton;
        private Button operationsTBD3Button;
        private Button aspirateOperationButton;
        private Button operationsTBD2Button;
        private Button dispenseOperationButton;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private TableLayoutPanel tableLayoutPanel2;
        private Label labwareLabel;
        private Button tipBoxButton;
        private Button wellplateButton;
        private Button tubeStandButton;
        private Button labwareTBD9Button;
        private Button labwareTBD3Button;
        private Button labwareTBD2Button;
        private Button labwareTBD1Button;
        private Button labwareTBD4Button;
        private Button labwareTBD5Button;
        private Button labwareTBD6Button;
        private Button labwareTBD7Button;
        private Button labwareTBD8Button;
        private Button saveProtocolButton;
        private ToolStripMenuItem manuallyMovePumpToolStripMenuItem;
        private Button loadProtocolButton;
        private Button clearProtocolButton;
        private Button mixOperationButton;
    }
}

