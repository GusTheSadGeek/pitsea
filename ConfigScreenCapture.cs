using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pitsea
{
    public partial class ConfigScreenCapture : Form
    {
        private Form myForm;
        private GameData gameData;

        public ConfigScreenCapture(GameData gd)
        {
            InitializeComponent();
            gameData = gd;
        }

        private void button1_Click(object sender, EventArgs e)
        {
//            Rectangle cropRect = new Rectangle(50, 295, 1250, 750);

            myForm = new Form();
            myForm.StartPosition = FormStartPosition.Manual; 

            myForm.BackColor = Color.White;
            myForm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            myForm.Bounds = Screen.PrimaryScreen.Bounds;
            myForm.TopMost = true;

            myForm.Left = gameData.SaveGameData.captureRect.Left-8;
            myForm.Width = gameData.SaveGameData.captureRect.Width+16;
            myForm.Top = gameData.SaveGameData.captureRect.Top-20;
            myForm.Height = gameData.SaveGameData.captureRect.Height+28;
            myForm.BackColor = Color.Gainsboro;
            myForm.TransparencyKey = Color.Gainsboro;

            myForm.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
//            Point topleft(
//            myForm.RectangleToScreen(myForm.ClientRectangle)
            gameData.SaveGameData.captureRect = myForm.RectangleToScreen(myForm.ClientRectangle);

                //new Rectangle(myForm.Left+(myForm.Width-myForm.ClientRectangle.Width)/2,
                //            myForm.Top + (myForm.Height - myForm.ClientRectangle.Height) / 2,
                //            myForm.ClientRectangle.Width,
                //            myForm.ClientRectangle.Height);

            myForm.Hide();

            this.Close();
        }
    }
}
