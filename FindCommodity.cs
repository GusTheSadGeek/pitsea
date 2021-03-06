﻿using System;
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
            this.Top = Main.Instance.Top + 40;
            this.Left = Main.Instance.Left + 40; 
            InitializeComponent();
            gameData = gd;
            grabCommodityData = gcd;
            commodityComboBox.Items.Clear();

        }


        private void DisplayFoundCommodity(bool buy=true)
        {
            StarSystem currentSystem = Main.Instance.GetSeletedSystem();
            
            string com = commodityComboBox.SelectedItem as string;

            bindingTable = new DataTable();

            bindingTable.Columns.Add("Star System");
            bindingTable.Columns.Add("Station");
            bindingTable.Columns.Add("SellPrice");
            bindingTable.Columns.Add("BuyPrice");
            bindingTable.Columns.Add("Supply");
            bindingTable.Columns.Add("LastUpdated");
            bindingTable.Columns.Add("Distance");

            bindingTable.Columns["Star System"].DataType = System.Type.GetType("System.String");
            bindingTable.Columns["Station"].DataType = System.Type.GetType("System.String");
            bindingTable.Columns["BuyPrice"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["SellPrice"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["Supply"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["Supply"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["LastUpdated"].DataType = System.Type.GetType("System.DateTime");
            bindingTable.Columns["Distance"].DataType = System.Type.GetType("System.Int64");


            foreach (StarSystem ss in gameData.StarSystems)
            {
                foreach (Station station in ss.Stations)
                {
                    Commodity commodity = station.Commodities.Find(x => x.NiceName == com);
                    if ((commodity != null) && ( (buy && (commodity.BuyPrice>0)) || (!buy && (commodity.SellPrice>0))))
                    {
                        DataRow newRow = bindingTable.NewRow();
                        newRow["Star System"] = ss.Name;
                        newRow["Station"] = station.Name;
                        newRow["SellPrice"] = commodity.SellPrice;
                        newRow["BuyPrice"] = commodity.BuyPrice;
                        newRow["Supply"] = commodity.Supply;
                        newRow["LastUpdated"] = commodity.LastUpdated;
                        newRow["Distance"] = ss.distanceFrom(currentSystem);
                        bindingTable.Rows.Add(newRow);
                    }
                }
            }
            dataGridView.DataSource = bindingTable;

            dataGridView.AutoResizeColumns();

        }

        private void FindCommodity_Load(object sender, EventArgs e)
        {
            foreach (CommodityType commtype in gameData.CommodityTypes.OrderBy(X => X.name).ToList())
            {
                if (commtype.name != null)
                {
                    commodityComboBox.Items.Add(commtype.name);
                }
            }
        }

        private void FindButton_Click(object sender, EventArgs e)
        {
            DisplayFoundCommodity(true);
        }

        private void FindButtonSell_Click(object sender, EventArgs e)
        {
            DisplayFoundCommodity(false);
        }
    }
}
