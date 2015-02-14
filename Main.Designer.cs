namespace Pitsea
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.SystemComboBox = new System.Windows.Forms.ComboBox();
            this.SystemLabel = new System.Windows.Forms.Label();
            this.StationLabel = new System.Windows.Forms.Label();
            this.StationComboBox = new System.Windows.Forms.ComboBox();
            this.AddSystemButton = new System.Windows.Forms.Button();
            this.AddStationButton = new System.Windows.Forms.Button();
            this.RemoveSystemButton = new System.Windows.Forms.Button();
            this.RemoveStationButton = new System.Windows.Forms.Button();
            this.CalculateAllTradesButton = new System.Windows.Forms.Button();
            this.CargoSlotsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.CapitalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.CapitalLabel = new System.Windows.Forms.Label();
            this.CargoSlotsLabel = new System.Windows.Forms.Label();
            this.TimestampAllButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitWithSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAndExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadEddbStationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GrabDataButton = new System.Windows.Forms.Button();
            this.FindCommodityButton = new System.Windows.Forms.Button();
            this.distanceBox = new System.Windows.Forms.TextBox();
            this.JumpUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.editSystemButton = new System.Windows.Forms.Button();
            this.allSystemsCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.localSystemCheckBox = new System.Windows.Forms.CheckBox();
            this.GoodsTable = new Pitsea.CustomDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.CargoSlotsNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CapitalNumericUpDown)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JumpUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GoodsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // SystemComboBox
            // 
            this.SystemComboBox.FormattingEnabled = true;
            this.SystemComboBox.Location = new System.Drawing.Point(81, 27);
            this.SystemComboBox.Name = "SystemComboBox";
            this.SystemComboBox.Size = new System.Drawing.Size(207, 21);
            this.SystemComboBox.TabIndex = 0;
            this.SystemComboBox.SelectedIndexChanged += new System.EventHandler(this.SystemComboBox_SelectedIndexChanged);
            // 
            // SystemLabel
            // 
            this.SystemLabel.AutoSize = true;
            this.SystemLabel.Location = new System.Drawing.Point(12, 30);
            this.SystemLabel.Name = "SystemLabel";
            this.SystemLabel.Size = new System.Drawing.Size(63, 13);
            this.SystemLabel.TabIndex = 1;
            this.SystemLabel.Text = "Star System";
            // 
            // StationLabel
            // 
            this.StationLabel.AutoSize = true;
            this.StationLabel.Location = new System.Drawing.Point(324, 31);
            this.StationLabel.Name = "StationLabel";
            this.StationLabel.Size = new System.Drawing.Size(40, 13);
            this.StationLabel.TabIndex = 2;
            this.StationLabel.Text = "Station";
            // 
            // StationComboBox
            // 
            this.StationComboBox.FormattingEnabled = true;
            this.StationComboBox.Location = new System.Drawing.Point(361, 27);
            this.StationComboBox.Name = "StationComboBox";
            this.StationComboBox.Size = new System.Drawing.Size(207, 21);
            this.StationComboBox.TabIndex = 3;
            this.StationComboBox.SelectedIndexChanged += new System.EventHandler(this.StationComboBox_SelectedIndexChanged);
            // 
            // AddSystemButton
            // 
            this.AddSystemButton.Location = new System.Drawing.Point(15, 54);
            this.AddSystemButton.Name = "AddSystemButton";
            this.AddSystemButton.Size = new System.Drawing.Size(73, 23);
            this.AddSystemButton.TabIndex = 1;
            this.AddSystemButton.Text = "Add System";
            this.AddSystemButton.UseVisualStyleBackColor = true;
            this.AddSystemButton.Click += new System.EventHandler(this.AddSystemButton_Click);
            // 
            // AddStationButton
            // 
            this.AddStationButton.Location = new System.Drawing.Point(361, 54);
            this.AddStationButton.Name = "AddStationButton";
            this.AddStationButton.Size = new System.Drawing.Size(100, 23);
            this.AddStationButton.TabIndex = 4;
            this.AddStationButton.Text = "Add Station";
            this.AddStationButton.UseVisualStyleBackColor = true;
            this.AddStationButton.Click += new System.EventHandler(this.AddStationButton_Click);
            // 
            // RemoveSystemButton
            // 
            this.RemoveSystemButton.Location = new System.Drawing.Point(187, 54);
            this.RemoveSystemButton.Name = "RemoveSystemButton";
            this.RemoveSystemButton.Size = new System.Drawing.Size(101, 23);
            this.RemoveSystemButton.TabIndex = 2;
            this.RemoveSystemButton.Text = "Remove System";
            this.RemoveSystemButton.UseVisualStyleBackColor = true;
            this.RemoveSystemButton.Click += new System.EventHandler(this.RemoveSystemButton_Click);
            // 
            // RemoveStationButton
            // 
            this.RemoveStationButton.Location = new System.Drawing.Point(467, 54);
            this.RemoveStationButton.Name = "RemoveStationButton";
            this.RemoveStationButton.Size = new System.Drawing.Size(101, 23);
            this.RemoveStationButton.TabIndex = 5;
            this.RemoveStationButton.Text = "Remove Station";
            this.RemoveStationButton.UseVisualStyleBackColor = true;
            this.RemoveStationButton.Click += new System.EventHandler(this.RemoveStationButton_Click);
            // 
            // CalculateAllTradesButton
            // 
            this.CalculateAllTradesButton.Location = new System.Drawing.Point(201, 109);
            this.CalculateAllTradesButton.Name = "CalculateAllTradesButton";
            this.CalculateAllTradesButton.Size = new System.Drawing.Size(180, 23);
            this.CalculateAllTradesButton.TabIndex = 9;
            this.CalculateAllTradesButton.Text = "Calculate Trades";
            this.CalculateAllTradesButton.UseVisualStyleBackColor = true;
            this.CalculateAllTradesButton.Click += new System.EventHandler(this.CalculateAllTradesManifestsAndRoutesButton_Click);
            // 
            // CargoSlotsNumericUpDown
            // 
            this.CargoSlotsNumericUpDown.Location = new System.Drawing.Point(361, 83);
            this.CargoSlotsNumericUpDown.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.CargoSlotsNumericUpDown.Name = "CargoSlotsNumericUpDown";
            this.CargoSlotsNumericUpDown.Size = new System.Drawing.Size(149, 20);
            this.CargoSlotsNumericUpDown.TabIndex = 7;
            // 
            // CapitalNumericUpDown
            // 
            this.CapitalNumericUpDown.Location = new System.Drawing.Point(81, 83);
            this.CapitalNumericUpDown.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.CapitalNumericUpDown.Name = "CapitalNumericUpDown";
            this.CapitalNumericUpDown.Size = new System.Drawing.Size(207, 20);
            this.CapitalNumericUpDown.TabIndex = 6;
            // 
            // CapitalLabel
            // 
            this.CapitalLabel.AutoSize = true;
            this.CapitalLabel.Location = new System.Drawing.Point(36, 85);
            this.CapitalLabel.Name = "CapitalLabel";
            this.CapitalLabel.Size = new System.Drawing.Size(39, 13);
            this.CapitalLabel.TabIndex = 26;
            this.CapitalLabel.Text = "Capital";
            // 
            // CargoSlotsLabel
            // 
            this.CargoSlotsLabel.AutoSize = true;
            this.CargoSlotsLabel.Location = new System.Drawing.Point(294, 85);
            this.CargoSlotsLabel.Name = "CargoSlotsLabel";
            this.CargoSlotsLabel.Size = new System.Drawing.Size(61, 13);
            this.CargoSlotsLabel.TabIndex = 27;
            this.CargoSlotsLabel.Text = "Cargo Slots";
            // 
            // TimestampAllButton
            // 
            this.TimestampAllButton.Location = new System.Drawing.Point(387, 109);
            this.TimestampAllButton.Name = "TimestampAllButton";
            this.TimestampAllButton.Size = new System.Drawing.Size(181, 23);
            this.TimestampAllButton.TabIndex = 28;
            this.TimestampAllButton.Text = "Timestamp All Commodities";
            this.TimestampAllButton.UseVisualStyleBackColor = true;
            this.TimestampAllButton.Click += new System.EventHandler(this.TimestampAllButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(779, 24);
            this.menuStrip1.TabIndex = 29;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDataToolStripMenuItem,
            this.loadDataToolStripMenuItem,
            this.exitWithSaveToolStripMenuItem,
            this.configureCaptureToolStripMenuItem,
            this.saveAndExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveDataToolStripMenuItem
            // 
            this.saveDataToolStripMenuItem.Name = "saveDataToolStripMenuItem";
            this.saveDataToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.saveDataToolStripMenuItem.Text = "&Save Data File";
            this.saveDataToolStripMenuItem.Click += new System.EventHandler(this.saveDataToolStripMenuItem_Click);
            // 
            // loadDataToolStripMenuItem
            // 
            this.loadDataToolStripMenuItem.Name = "loadDataToolStripMenuItem";
            this.loadDataToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.loadDataToolStripMenuItem.Text = "&Open Data File";
            this.loadDataToolStripMenuItem.Click += new System.EventHandler(this.loadDataToolStripMenuItem_Click);
            // 
            // exitWithSaveToolStripMenuItem
            // 
            this.exitWithSaveToolStripMenuItem.Name = "exitWithSaveToolStripMenuItem";
            this.exitWithSaveToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.exitWithSaveToolStripMenuItem.Text = "S&ave and Exit";
            this.exitWithSaveToolStripMenuItem.Click += new System.EventHandler(this.exitWithSaveToolStripMenuItem_Click);
            // 
            // configureCaptureToolStripMenuItem
            // 
            this.configureCaptureToolStripMenuItem.Name = "configureCaptureToolStripMenuItem";
            this.configureCaptureToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.configureCaptureToolStripMenuItem.Text = "Configure Capture";
            this.configureCaptureToolStripMenuItem.Click += new System.EventHandler(this.configureCaptureToolStripMenuItem_Click);
            // 
            // saveAndExitToolStripMenuItem
            // 
            this.saveAndExitToolStripMenuItem.Name = "saveAndExitToolStripMenuItem";
            this.saveAndExitToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.saveAndExitToolStripMenuItem.Text = "E&xit";
            this.saveAndExitToolStripMenuItem.Click += new System.EventHandler(this.saveAndExitToolStripMenuItem_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadEddbStationsToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.testToolStripMenuItem.Text = "test";
            // 
            // loadEddbStationsToolStripMenuItem
            // 
            this.loadEddbStationsToolStripMenuItem.Name = "loadEddbStationsToolStripMenuItem";
            this.loadEddbStationsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.loadEddbStationsToolStripMenuItem.Text = "load eddb stations";
            this.loadEddbStationsToolStripMenuItem.Click += new System.EventHandler(this.loadEddbStationsToolStripMenuItem_Click);
            // 
            // GrabDataButton
            // 
            this.GrabDataButton.Location = new System.Drawing.Point(584, 54);
            this.GrabDataButton.Name = "GrabDataButton";
            this.GrabDataButton.Size = new System.Drawing.Size(121, 23);
            this.GrabDataButton.TabIndex = 30;
            this.GrabDataButton.Text = "Grab Market Data";
            this.GrabDataButton.UseVisualStyleBackColor = true;
            this.GrabDataButton.Click += new System.EventHandler(this.GrabDataButton_Click);
            // 
            // FindCommodityButton
            // 
            this.FindCommodityButton.Location = new System.Drawing.Point(584, 109);
            this.FindCommodityButton.Name = "FindCommodityButton";
            this.FindCommodityButton.Size = new System.Drawing.Size(121, 23);
            this.FindCommodityButton.TabIndex = 31;
            this.FindCommodityButton.Text = "Find Commodity";
            this.FindCommodityButton.UseVisualStyleBackColor = true;
            this.FindCommodityButton.Click += new System.EventHandler(this.FindCommodityButton_Click);
            // 
            // distanceBox
            // 
            this.distanceBox.Location = new System.Drawing.Point(584, 28);
            this.distanceBox.Name = "distanceBox";
            this.distanceBox.Size = new System.Drawing.Size(83, 20);
            this.distanceBox.TabIndex = 32;
            this.distanceBox.TextChanged += new System.EventHandler(this.distanceBox_TextChanged);
            this.distanceBox.Leave += new System.EventHandler(this.distanceBox_Leave);
            // 
            // JumpUpDown
            // 
            this.JumpUpDown.DecimalPlaces = 1;
            this.JumpUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.JumpUpDown.Location = new System.Drawing.Point(583, 83);
            this.JumpUpDown.Name = "JumpUpDown";
            this.JumpUpDown.Size = new System.Drawing.Size(101, 20);
            this.JumpUpDown.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(516, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 34;
            this.label1.Text = "Jump Dist";
            // 
            // editSystemButton
            // 
            this.editSystemButton.Location = new System.Drawing.Point(94, 54);
            this.editSystemButton.Name = "editSystemButton";
            this.editSystemButton.Size = new System.Drawing.Size(87, 23);
            this.editSystemButton.TabIndex = 35;
            this.editSystemButton.Text = "Edit System";
            this.editSystemButton.UseVisualStyleBackColor = true;
            // 
            // allSystemsCheckBox
            // 
            this.allSystemsCheckBox.AutoSize = true;
            this.allSystemsCheckBox.Location = new System.Drawing.Point(294, 54);
            this.allSystemsCheckBox.Name = "allSystemsCheckBox";
            this.allSystemsCheckBox.Size = new System.Drawing.Size(33, 17);
            this.allSystemsCheckBox.TabIndex = 36;
            this.allSystemsCheckBox.Text = "A";
            this.allSystemsCheckBox.UseVisualStyleBackColor = true;
            this.allSystemsCheckBox.CheckedChanged += new System.EventHandler(this.allSystemsCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(673, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 37;
            this.label2.Text = "LS";
            // 
            // localSystemCheckBox
            // 
            this.localSystemCheckBox.AutoSize = true;
            this.localSystemCheckBox.Location = new System.Drawing.Point(294, 29);
            this.localSystemCheckBox.Name = "localSystemCheckBox";
            this.localSystemCheckBox.Size = new System.Drawing.Size(32, 17);
            this.localSystemCheckBox.TabIndex = 38;
            this.localSystemCheckBox.Text = "L";
            this.localSystemCheckBox.UseVisualStyleBackColor = true;
            this.localSystemCheckBox.CheckedChanged += new System.EventHandler(this.localSystemCheckBox_CheckedChanged);
            // 
            // GoodsTable
            // 
            this.GoodsTable.AllowUserToAddRows = false;
            this.GoodsTable.AllowUserToDeleteRows = false;
            this.GoodsTable.AllowUserToResizeColumns = false;
            this.GoodsTable.AllowUserToResizeRows = false;
            this.GoodsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GoodsTable.Location = new System.Drawing.Point(12, 138);
            this.GoodsTable.Name = "GoodsTable";
            this.GoodsTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.GoodsTable.Size = new System.Drawing.Size(755, 712);
            this.GoodsTable.TabIndex = 0;
            this.GoodsTable.TabStop = false;
            this.GoodsTable.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CommoditiesGrid_CellContentClick);
            this.GoodsTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CommoditiesGrid_CellEndEdit);
            // 
            // Main
            // 
            this.AcceptButton = this.CalculateAllTradesButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 862);
            this.Controls.Add(this.localSystemCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.allSystemsCheckBox);
            this.Controls.Add(this.editSystemButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.JumpUpDown);
            this.Controls.Add(this.distanceBox);
            this.Controls.Add(this.FindCommodityButton);
            this.Controls.Add(this.GrabDataButton);
            this.Controls.Add(this.TimestampAllButton);
            this.Controls.Add(this.GoodsTable);
            this.Controls.Add(this.CargoSlotsLabel);
            this.Controls.Add(this.CapitalLabel);
            this.Controls.Add(this.CapitalNumericUpDown);
            this.Controls.Add(this.CargoSlotsNumericUpDown);
            this.Controls.Add(this.CalculateAllTradesButton);
            this.Controls.Add(this.RemoveStationButton);
            this.Controls.Add(this.RemoveSystemButton);
            this.Controls.Add(this.AddStationButton);
            this.Controls.Add(this.AddSystemButton);
            this.Controls.Add(this.StationComboBox);
            this.Controls.Add(this.StationLabel);
            this.Controls.Add(this.SystemLabel);
            this.Controls.Add(this.SystemComboBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Pitsea";
            this.Deactivate += new System.EventHandler(this.Main_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.CargoSlotsNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CapitalNumericUpDown)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.JumpUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GoodsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SystemComboBox;
        private System.Windows.Forms.Label SystemLabel;
        private System.Windows.Forms.Label StationLabel;
        private System.Windows.Forms.ComboBox StationComboBox;
        private System.Windows.Forms.Button AddSystemButton;
        private System.Windows.Forms.Button AddStationButton;
        private System.Windows.Forms.Button RemoveSystemButton;
        private System.Windows.Forms.Button RemoveStationButton;
        private System.Windows.Forms.Button CalculateAllTradesButton;
        private System.Windows.Forms.NumericUpDown CargoSlotsNumericUpDown;
        private System.Windows.Forms.NumericUpDown CapitalNumericUpDown;
        private System.Windows.Forms.Label CapitalLabel;
        private System.Windows.Forms.Label CargoSlotsLabel;
        private CustomDataGridView GoodsTable;
        private System.Windows.Forms.Button TimestampAllButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitWithSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAndExitToolStripMenuItem;
        private System.Windows.Forms.Button GrabDataButton;
        private System.Windows.Forms.Button FindCommodityButton;
        private System.Windows.Forms.ToolStripMenuItem configureCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadEddbStationsToolStripMenuItem;
        private System.Windows.Forms.TextBox distanceBox;
        private System.Windows.Forms.NumericUpDown JumpUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button editSystemButton;
        private System.Windows.Forms.CheckBox allSystemsCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox localSystemCheckBox;
    }
}

