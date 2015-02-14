namespace Pitsea
{
    partial class AddSystemDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddSystemDialog));
            this.SystemLabel = new System.Windows.Forms.Label();
            this.SystemTextBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.coord_x = new System.Windows.Forms.TextBox();
            this.coord_y = new System.Windows.Forms.TextBox();
            this.coord_z = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SystemLabel
            // 
            this.SystemLabel.AutoSize = true;
            this.SystemLabel.Location = new System.Drawing.Point(12, 9);
            this.SystemLabel.Name = "SystemLabel";
            this.SystemLabel.Size = new System.Drawing.Size(41, 13);
            this.SystemLabel.TabIndex = 0;
            this.SystemLabel.Text = "System";
            // 
            // SystemTextBox
            // 
            this.SystemTextBox.Location = new System.Drawing.Point(53, 6);
            this.SystemTextBox.Name = "SystemTextBox";
            this.SystemTextBox.Size = new System.Drawing.Size(250, 20);
            this.SystemTextBox.TabIndex = 1;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(53, 117);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(250, 23);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Add System";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // coord_x
            // 
            this.coord_x.Location = new System.Drawing.Point(53, 32);
            this.coord_x.Name = "coord_x";
            this.coord_x.Size = new System.Drawing.Size(176, 20);
            this.coord_x.TabIndex = 3;
            // 
            // coord_y
            // 
            this.coord_y.Location = new System.Drawing.Point(53, 58);
            this.coord_y.Name = "coord_y";
            this.coord_y.Size = new System.Drawing.Size(176, 20);
            this.coord_y.TabIndex = 4;
            // 
            // coord_z
            // 
            this.coord_z.Location = new System.Drawing.Point(53, 84);
            this.coord_z.Name = "coord_z";
            this.coord_z.Size = new System.Drawing.Size(176, 20);
            this.coord_z.TabIndex = 5;
            this.coord_z.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Z";
            // 
            // AddSystemDialog
            // 
            this.AcceptButton = this.AddButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 152);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.coord_z);
            this.Controls.Add(this.coord_y);
            this.Controls.Add(this.coord_x);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.SystemTextBox);
            this.Controls.Add(this.SystemLabel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddSystemDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Add a System";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label SystemLabel;
        private System.Windows.Forms.TextBox SystemTextBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.TextBox coord_x;
        private System.Windows.Forms.TextBox coord_y;
        private System.Windows.Forms.TextBox coord_z;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}