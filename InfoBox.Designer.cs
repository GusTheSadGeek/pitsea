namespace Pitsea
{
    partial class InfoBoxA
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
            this.InfoBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // InfoBox1
            // 
            this.InfoBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoBox1.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoBox1.Location = new System.Drawing.Point(-1, -2);
            this.InfoBox1.Multiline = true;
            this.InfoBox1.Name = "InfoBox1";
            this.InfoBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.InfoBox1.Size = new System.Drawing.Size(401, 285);
            this.InfoBox1.TabIndex = 2;
            // 
            // InfoBoxA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 287);
            this.Controls.Add(this.InfoBox1);
            this.Name = "InfoBoxA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "InfoBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InfoBox1;
    }
}