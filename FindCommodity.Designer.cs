namespace Pitsea
{
    partial class FindCommodity
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
            this.label1 = new System.Windows.Forms.Label();
            this.commodityComboBox = new System.Windows.Forms.ComboBox();
            this.dataGridView = new Pitsea.CustomDataGridView();
            this.FindButton = new System.Windows.Forms.Button();
            this.FindButtonSell = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Commodity";
            // 
            // commodityComboBox
            // 
            this.commodityComboBox.FormattingEnabled = true;
            this.commodityComboBox.Location = new System.Drawing.Point(109, 11);
            this.commodityComboBox.Name = "commodityComboBox";
            this.commodityComboBox.Size = new System.Drawing.Size(280, 21);
            this.commodityComboBox.TabIndex = 2;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(2, 39);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView.Size = new System.Drawing.Size(742, 552);
            this.dataGridView.TabIndex = 3;
            // 
            // FindButton
            // 
            this.FindButton.Location = new System.Drawing.Point(395, 9);
            this.FindButton.Name = "FindButton";
            this.FindButton.Size = new System.Drawing.Size(129, 23);
            this.FindButton.TabIndex = 4;
            this.FindButton.Text = "Find Commodity to BUY";
            this.FindButton.UseVisualStyleBackColor = true;
            this.FindButton.Click += new System.EventHandler(this.FindButton_Click);
            // 
            // FindButtonSell
            // 
            this.FindButtonSell.Location = new System.Drawing.Point(549, 9);
            this.FindButtonSell.Name = "FindButtonSell";
            this.FindButtonSell.Size = new System.Drawing.Size(131, 23);
            this.FindButtonSell.TabIndex = 5;
            this.FindButtonSell.Text = "Find Commodity to SELL";
            this.FindButtonSell.UseVisualStyleBackColor = true;
            this.FindButtonSell.Click += new System.EventHandler(this.FindButtonSell_Click);
            // 
            // FindCommodity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 614);
            this.Controls.Add(this.FindButtonSell);
            this.Controls.Add(this.FindButton);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.commodityComboBox);
            this.Controls.Add(this.label1);
            this.Name = "FindCommodity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FindCommodity";
            this.Load += new System.EventHandler(this.FindCommodity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox commodityComboBox;
        private CustomDataGridView dataGridView;
        private System.Windows.Forms.Button FindButton;
        private System.Windows.Forms.Button FindButtonSell;
    }
}