using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pitsea
{
    public partial class AddStationDialog : Form
    {
        private Station result;

        public Station Result
        {
            get { return result; }
        }

        public AddStationDialog(string systemName)
        {
            InitializeComponent();
            this.Icon = new Icon("Graphics\\Pitsea.ico");
            SystemTextBox.Text = systemName;
            result = null;
        }

        private void AddStationButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StationTextBox.Text))
            {
                MessageBox.Show("Please provide a station name.", "Error: Station name needed.", MessageBoxButtons.OK);
                return;
            }

            result = new Station();
            result.Name = StationTextBox.Text;
            long distance;
            long.TryParse(distanceBox.Text, out distance);
            result.DistanceToStar = distance;
            this.Close();
        }
    }
}
