using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pitsea
{
    public partial class AddSystemDialog : Form
    {
        private StarSystem result;

        public StarSystem Result
        {
            get { return result; }
        }

        public AddSystemDialog()
        {
            InitializeComponent();
            this.Icon = new Icon("Graphics\\Pitsea.ico");
            result = null;
        }

        public void SetEdit(StarSystem s)
        {
            result = s;
            this.AddButton.Text = "Edit System";
            this.Text = "Edit a Star Sytem";
            this.SystemTextBox.Text = s.Name;
            this.coord_x.Text = s.Coordinate.x.ToString();
            this.coord_y.Text = s.Coordinate.y.ToString();
            this.coord_z.Text = s.Coordinate.z.ToString();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SystemTextBox.Text))
            {
                MessageBox.Show("Please provide a name for the system.", "Error: System name needed", MessageBoxButtons.OK);
                return;
            }

            StarSystem ss = GameData.Instance.StarSystems.Find(s => s.Name == SystemTextBox.Text);
            if (ss == null)
            {
                result = new StarSystem();
                result.Name = SystemTextBox.Text;
                result.Id = getNewStarId();
            }

            double.TryParse(this.coord_x.Text, out result.Coordinate.x);

            double.TryParse(this.coord_y.Text, out result.Coordinate.y);

            double.TryParse(this.coord_z.Text, out result.Coordinate.z);

            
            this.Close();
        }


        Int64 getNewStarId()
        {
            Int64 lowest = -1;
            foreach (StarSystem s in GameData.Instance.StarSystems)
            {
                if (s.Id < lowest)
                {
                    lowest = s.Id;
                }
            }
            return --lowest;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
