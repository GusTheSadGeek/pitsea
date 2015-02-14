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
    public partial class InfoBoxA : Form
    {
        private static InfoBoxA _instance;
        private string txt;
        public InfoBoxA()
        {
            _instance = this;
            InitializeComponent();
            this.Top = Main.Instance.Top + 20;
            this.Left = Main.Instance.Left + 20;
            txt = "";
        }

        public static InfoBoxA Instance
        {
            get { return _instance; }
        }

        public void Message(string msg)
        {
            txt = txt + msg + "\r\n";

            InfoBox1.Text = txt;
            InfoBox1.Update();
        }
    }
}
