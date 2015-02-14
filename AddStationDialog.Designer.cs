namespace Pitsea
{
    partial class AddStationDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddStationDialog));
            this.SystemLabel = new System.Windows.Forms.Label();
            this.SystemTextBox = new System.Windows.Forms.TextBox();
            this.StationLabel = new System.Windows.Forms.Label();
            this.StationTextBox = new System.Windows.Forms.TextBox();
            this.AddStationButton = new System.Windows.Forms.Button();
            this.distanceBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SystemLabel
            // 
            this.SystemLabel.AutoSize = true;
            this.SystemLabel.Location = new System.Drawing.Point(13, 13);
            this.SystemLabel.Name = "SystemLabel";
            this.SystemLabel.Size = new System.Drawing.Size(41, 13);
            this.SystemLabel.TabIndex = 0;
            this.SystemLabel.Text = "System";
            // 
            // SystemTextBox
            // 
            this.SystemTextBox.Enabled = false;
            this.SystemTextBox.Location = new System.Drawing.Point(60, 10);
            this.SystemTextBox.Name = "SystemTextBox";
            this.SystemTextBox.Size = new System.Drawing.Size(250, 20);
            this.SystemTextBox.TabIndex = 1;
            // 
            // StationLabel
            // 
            this.StationLabel.AutoSize = true;
            this.StationLabel.Location = new System.Drawing.Point(13, 40);
            this.StationLabel.Name = "StationLabel";
            this.StationLabel.Size = new System.Drawing.Size(40, 13);
            this.StationLabel.TabIndex = 2;
            this.StationLabel.Text = "Station";
            // 
            // StationTextBox
            // 
            this.StationTextBox.Location = new System.Drawing.Point(60, 37);
            this.StationTextBox.Name = "StationTextBox";
            this.StationTextBox.Size = new System.Drawing.Size(250, 20);
            this.StationTextBox.TabIndex = 3;
            // 
            // AddStationButton
            // 
            this.AddStationButton.Location = new System.Drawing.Point(60, 91);
            this.AddStationButton.Name = "AddStationButton";
            this.AddStationButton.Size = new System.Drawing.Size(250, 23);
            this.AddStationButton.TabIndex = 4;
            this.AddStationButton.Text = "Add Station";
            this.AddStationButton.UseVisualStyleBackColor = true;
            this.AddStationButton.Click += new System.EventHandler(this.AddStationButton_Click);
            // 
            // distanceBox
            // 
            this.distanceBox.Location = new System.Drawing.Point(60, 63);
            this.distanceBox.Name = "distanceBox";
            this.distanceBox.Size = new System.Drawing.Size(223, 20);
            this.distanceBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Distance";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "LS";
            // 
            // AddStationDialog
            // 
            this.AcceptButton = this.AddStationButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 116);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.distanceBox);
            this.Controls.Add(this.AddStationButton);
            this.Controls.Add(this.StationTextBox);
            this.Controls.Add(this.StationLabel);
            this.Controls.Add(this.SystemTextBox);
            this.Controls.Add(this.SystemLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddStationDialog";
            this.Text = "Add a Station";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SystemLabel;
        private System.Windows.Forms.TextBox SystemTextBox;
        private System.Windows.Forms.Label StationLabel;
        private System.Windows.Forms.TextBox StationTextBox;
        private System.Windows.Forms.Button AddStationButton;
        private System.Windows.Forms.TextBox distanceBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}