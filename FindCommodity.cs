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
    public partial class FindCommodity : Form
    {
        GameData gameData;
        GameData grabCommodityData;
        private DataTable bindingTable;

        public FindCommodity(GameData gd, GameData gcd)
        {
            InitializeComponent();
            gameData = gd;
            grabCommodityData = gcd;
            commodityComboBox.Items.Clear();

        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            string com = commodityComboBox.SelectedItem as string;

            bindingTable = new DataTable();

            bindingTable.Columns.Add("Star System");
            bindingTable.Columns.Add("Station");
            bindingTable.Columns.Add("SellPrice");
            bindingTable.Columns.Add("BuyPrice");
            bindingTable.Columns.Add("Supply");
            bindingTable.Columns.Add("LastUpdated");

            bindingTable.Columns["Star System"].DataType = System.Type.GetType("System.String");
            bindingTable.Columns["Station"].DataType = System.Type.GetType("System.String");
            bindingTable.Columns["BuyPrice"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["SellPrice"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["Supply"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["Supply"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["LastUpdated"].DataType = System.Type.GetType("System.DateTime");


            foreach (StarSystem ss in gameData.StarSystems)
            {
                foreach (Station station in ss.Stations)
                {
                    Commodity commodity = station.Commodities.Find(x => x.NiceName == com);
                    if (commodity != null)
                    {
                        DataRow newRow = bindingTable.NewRow();
                        newRow["Star System"] = ss.Name;
                        newRow["Station"] = station.Name;
                        newRow["SellPrice"] = commodity.SellPrice;
                        newRow["BuyPrice"] = commodity.BuyPrice;
                        newRow["Supply"] = commodity.Supply;
                        newRow["LastUpdated"] = commodity.LastUpdated;
                        bindingTable.Rows.Add(newRow);
                    }
                }
            }
            dataGridView.DataSource = bindingTable;

        }

        private void FindCommodity_Load(object sender, EventArgs e)
        {
            foreach (GrabCommodityDatum datum in grabCommodityData.CommodityList.OrderBy(o => o.Name).ToList())
            {
                if (datum.Name != null)
                {
                    commodityComboBox.Items.Add(datum.NiceName);
                }
            }
        }
    }
}
