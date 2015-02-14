namespace Pitsea
{
    partial class GrabData
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
            if (disposing && engine != null)
            {
                engine.Dispose();
            }

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
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.InfoBox = new System.Windows.Forms.TextBox();
            this.StationName = new System.Windows.Forms.TextBox();
            this.SystemName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, -2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 52);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(813, 750);
            this.textBox1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Snap";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // InfoBox
            // 
            this.InfoBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoBox.Location = new System.Drawing.Point(90, -2);
            this.InfoBox.Name = "InfoBox";
            this.InfoBox.ReadOnly = true;
            this.InfoBox.Size = new System.Drawing.Size(38, 26);
            this.InfoBox.TabIndex = 3;
            // 
            // StationName
            // 
            this.StationName.Location = new System.Drawing.Point(162, 0);
            this.StationName.Name = "StationName";
            this.StationName.Size = new System.Drawing.Size(292, 20);
            this.StationName.TabIndex = 4;
            // 
            // SystemName
            // 
            this.SystemName.Location = new System.Drawing.Point(162, 29);
            this.SystemName.Name = "SystemName";
            this.SystemName.Size = new System.Drawing.Size(292, 20);
            this.SystemName.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(460, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Station Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(461, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "System Name";
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(593, 1);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(172, 23);
            this.SubmitButton.TabIndex = 8;
            this.SubmitButton.Text = "REPLACE ALL DATA";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(593, 27);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(172, 23);
            this.UpdateButton.TabIndex = 9;
            this.UpdateButton.Text = "Update - replace scanned data";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // ConfigButton
            // 
            this.ConfigButton.Location = new System.Drawing.Point(81, 27);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(75, 23);
            this.ConfigButton.TabIndex = 10;
            this.ConfigButton.Text = "Config";
            this.ConfigButton.UseVisualStyleBackColor = true;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // GrabData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 803);
            this.Controls.Add(this.ConfigButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SystemName);
            this.Controls.Add(this.StationName);
            this.Controls.Add(this.InfoBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "GrabData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Grab Data";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GrabData_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox InfoBox;
        private System.Windows.Forms.TextBox StationName;
        private System.Windows.Forms.TextBox SystemName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Button ConfigButton;
    }
}

