using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Pitsea
{
    public partial class Main : Form
    {
        private GameData gameData;

        private DataTable bindingTable;

        public Main()
        {
            this.Left = Screen.PrimaryScreen.Bounds.Width + 20;
            this.Top = 20;

            InitializeComponent();
            this.Icon = new Icon("Graphics\\Pitsea.ico");

            gameData = new GameData();

            SystemComboBox.Items.Clear();
            StationComboBox.Items.Clear();
            GrabDataButton.Enabled = false;
        }

        private void AddSystemButton_Click(object sender, System.EventArgs e)
        {
            AddSystemDialog dialog = new AddSystemDialog();
            dialog.ShowDialog();

            if (dialog.Result == null)
                return;

            StarSystem newSystem = dialog.Result;
            gameData.StarSystems.Add(newSystem);
            SystemComboBox.Items.Add(newSystem.Name);
            SystemComboBox.SelectedIndex = SystemComboBox.Items.Count - 1;
        }
        private void RemoveSystemButton_Click(object sender, System.EventArgs e)
        {
            if (gameData.StarSystems.Count == 0)
                return;

            int selectedIndex = SystemComboBox.SelectedIndex;

            SystemComboBox.Items.RemoveAt(selectedIndex);
            gameData.StarSystems.RemoveAt(selectedIndex);

            if (gameData.StarSystems.Count > 0)
                SystemComboBox.SelectedIndex = Math.Min(selectedIndex, Math.Max(gameData.StarSystems.Count - 1, 0));
            else
            {
                SystemComboBox.Text = string.Empty;
                StationComboBox.Items.Clear();
                StationComboBox.Text = string.Empty;
                BindCommodities();
            }
        }
        private void SystemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SystemComboBox.SelectedIndex < 0)
                return;

            StarSystem selectedSystem = gameData.StarSystems[SystemComboBox.SelectedIndex];

            StationComboBox.Items.Clear();

            foreach (Station station in selectedSystem.Stations)
                StationComboBox.Items.Add(station.Name);

            if (StationComboBox.Items.Count > 0)
                StationComboBox.SelectedIndex = 0;
            else
            {
                StationComboBox.Text = string.Empty;
                BindCommodities();
            }
            GrabDataButton.Enabled = (StationComboBox.SelectedIndex >= 0);
        }

        private void AddStationButton_Click(object sender, EventArgs e)
        {
            if (SystemComboBox.Items.Count == 0)
                return;

            AddStationDialog dialog = new AddStationDialog(gameData.StarSystems[SystemComboBox.SelectedIndex].Name);
            dialog.ShowDialog();

            if (dialog.Result == null)
                return;

            gameData.StarSystems[SystemComboBox.SelectedIndex].Stations.Add(dialog.Result);
            StationComboBox.Items.Add(dialog.Result.Name);

            StationComboBox.SelectedIndex = StationComboBox.Items.Count - 1;
        }
        private void RemoveStationButton_Click(object sender, EventArgs e)
        {
            if (gameData.StarSystems.Count == 0 || gameData.StarSystems[SystemComboBox.SelectedIndex].Stations.Count == 0)
                return;

            int selectedStationIndex = StationComboBox.SelectedIndex;
            StarSystem selectedSystem = gameData.StarSystems[SystemComboBox.SelectedIndex];

            StationComboBox.Items.RemoveAt(selectedStationIndex);
            selectedSystem.Stations.RemoveAt(selectedStationIndex);

            if (selectedSystem.Stations.Count > 0)
                StationComboBox.SelectedIndex = Math.Min(selectedStationIndex, Math.Max(selectedSystem.Stations.Count - 1, 0));
            else
            {
                StationComboBox.Text = string.Empty;
                BindCommodities();
            }
        }
        private void StationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCommodities();
            GrabDataButton.Enabled = (SystemComboBox.SelectedIndex >= 0);
        }

        private void BindCommodities()
        {
            bindingTable = new DataTable();


//            bindingTable.coRowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing

            bindingTable.Columns.Add("Index");
            bindingTable.Columns.Add("Category");
            bindingTable.Columns.Add("Commodity");
            bindingTable.Columns.Add("SellPrice");
            bindingTable.Columns.Add("BuyPrice");
            bindingTable.Columns.Add("Supply");
            bindingTable.Columns.Add("LastUpdated");
            bindingTable.Columns.Add("PC");

            bindingTable.Columns["Index"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["BuyPrice"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["SellPrice"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["Supply"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["Supply"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["LastUpdated"].DataType = System.Type.GetType("System.DateTime");
            bindingTable.Columns["PC"].DataType = System.Type.GetType("System.Boolean");

            GoodsTable.Columns.Clear();

            if (SystemComboBox.SelectedIndex < 0 || gameData.StarSystems.Count == 0)
                return;

            StarSystem selectedSystem = gameData.StarSystems[SystemComboBox.SelectedIndex];

            if (StationComboBox.SelectedIndex < 0 || selectedSystem.Stations.Count == 0)
                return;

            Station selectedStation = selectedSystem.Stations[StationComboBox.SelectedIndex];

            int index = 0;




            foreach (Commodity commodity in selectedStation.Commodities)
            {
                DataRow newRow = bindingTable.NewRow();
                newRow["Index"] = index;
                newRow["Category"] = commodity.Cat;
                newRow["Commodity"] = commodity.Name;
                newRow["SellPrice"] = commodity.SellPrice;
                newRow["BuyPrice"] = commodity.BuyPrice;
                newRow["Supply"] = commodity.Supply;
                newRow["LastUpdated"] = commodity.LastUpdated;
                newRow["PC"] = commodity.PriceCheckRequired;
                bindingTable.Rows.Add(newRow);
                index++;
            }

            GoodsTable.DataSource = bindingTable;

            GoodsTable.Columns["Index"].Visible = false;
            GoodsTable.Columns["PC"].Visible = false;

            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.Name = "Delete";
            deleteColumn.HeaderText = "Delete";
            deleteColumn.Text = "Delete";
            deleteColumn.UseColumnTextForButtonValue = true;
            GoodsTable.Columns.Insert(0, deleteColumn);

            //DataGridViewButtonColumn upColumn = new DataGridViewButtonColumn();
            //upColumn.Name = "Up";
            //upColumn.HeaderText = "Up";
            //upColumn.Text = "▲";
            //upColumn.UseColumnTextForButtonValue = true;
            //CommoditiesGrid.Columns.Insert(8, upColumn);

            //DataGridViewButtonColumn downColumn = new DataGridViewButtonColumn();
            //downColumn.Name = "Down";
            //downColumn.HeaderText = "Down";
            //downColumn.Text = "▼";
            //downColumn.UseColumnTextForButtonValue = true;
            //CommoditiesGrid.Columns.Insert(9, downColumn);

//            CommoditiesGrid.Columns[0].Width = 50;  // Delete
//            CommoditiesGrid.Columns[1].Width = 30;  // Delete ?
            GoodsTable.Columns[0].Width = 50;  // Delete
            GoodsTable.Columns[2].Width = 130;  // Cat
            GoodsTable.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            GoodsTable.Columns[4].Width = 50;  // Sell
            GoodsTable.Columns[5].Width = 50; // Buy
            GoodsTable.Columns[6].Width = 60; // Supply
            GoodsTable.Columns[7].Width = 100; // Last Updated
//            CommoditiesGrid.Columns[8].Width = 40; // Up
//            CommoditiesGrid.Columns[9].Width = 40; // Down

//            GoodsTable.Rows[2].DefaultCellStyle.BackColor = Color.Red;

            foreach (DataGridViewRow row in GoodsTable.Rows)
            {
                Color c = Color.White;
                bool priceCheck = (bool)row.Cells[8].Value;

                if (priceCheck)
                {
                    c = Color.Red;
                }
                else
                {
                    string v = row.Cells[2].Value.ToString();
                    switch (v)
                    {
                        case "CHEMICALS":
                            c = Color.AntiqueWhite;
                            break;
                        case "CONSUMER ITEMS":
                            c = Color.LightGray;
                            break;
                        case "FOODS":
                            c = Color.DarkSalmon;
                            break;
                        case "LEGAL DRUGS":
                            c = Color.Aquamarine;
                            break;
                        case "MACHINERY":
                            c = Color.PaleTurquoise;
                            break;
                        case "MEDICINES":
                            c = Color.MistyRose;
                            break;
                        case "METALS":
                            c = Color.Pink;
                            break;
                        case "MINERALS":
                            c = Color.Khaki;
                            break;
                        case "TECHNOLOGY":
                            c = Color.PowderBlue;
                            break;
                        case "TEXTILES":
                            c = Color.RosyBrown;
                            break;
                        case "WASTE":
                            c = Color.Tan;
                            break;
                        case "WEAPONS":
                            c = Color.LightCoral;
                            break;
                    }
                }
                row.DefaultCellStyle.BackColor = c;
            }
            GoodsTable.Update();
        }

        private void AddCommodityButton_Click(object sender, EventArgs e)
        {
            if (gameData.StarSystems.Count == 0 || gameData.StarSystems[SystemComboBox.SelectedIndex].Stations.Count == 0)
                return;

            StarSystem selectedSystem = gameData.StarSystems[SystemComboBox.SelectedIndex];
            Station selectedStation = selectedSystem.Stations[StationComboBox.SelectedIndex];

            AddEditCommodityDialog dialog = new AddEditCommodityDialog(selectedSystem.Name, selectedStation.Name, gameData.StarSystems);
            dialog.ShowDialog();

            if (dialog.Result == null)
                return;

            Commodity newCommodity = dialog.Result;
            
            newCommodity.LastUpdated = DateTime.Now;

            selectedStation.Commodities.Add(newCommodity);
            BindCommodities();
        }
        private void CommoditiesGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            switch(GoodsTable.Columns[e.ColumnIndex].Name)
            {
                case "Commodity":
                case "BuyPrice":
                case "SellPrice":
                case "Supply":
                    UpdateSelectedCommodity(GoodsTable.Rows[e.RowIndex]);
                    break;
            }
        }

        private void CalculateAllTradesManifestsAndRoutesButton_Click(object sender, EventArgs e)
        {
            gameData.Capital = CapitalNumericUpDown.Value;
            gameData.CargoSlots = CargoSlotsNumericUpDown.Value;

            if (gameData.Capital <= 0 || gameData.CargoSlots <= 0)
            {
                MessageBox.Show("Please provide your available capital and cargo slots.", "Error", MessageBoxButtons.OK);
                return;
            }

            Adviser.CalculateAll(gameData);

            if (gameData.Trades.Count > 0)
            {
                TradesManifestsRoutesDialog dialog = new TradesManifestsRoutesDialog(gameData);
                dialog.Show();
            }
            else
                MessageBox.Show("No trades found. Make sure more than one system is up to date. Check your spelling on the commodities.", "Error: No trades found.", MessageBoxButtons.OK);
        }

        private void SaveData()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.AddExtension = true;
            saveFileDialog.DefaultExt = ".pitdata";
            saveFileDialog.InitialDirectory = string.Concat(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"\SaveData");
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Filter = "Pitsea (.pitdata)|*.pitdata";
            saveFileDialog.FilterIndex = 0;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                {
                    Stream fileStream = saveFileDialog.OpenFile();
                    XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
                    serializer.Serialize(fileStream, gameData.SaveGameData);
                    fileStream.Close();
                }
            }
        }

        private void LoadData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.DefaultExt = ".pitdata";
            openFileDialog.InitialDirectory = string.Concat(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"\SaveData");
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Filter = "Pitsea (.pitdata)|*.pitdata";
            openFileDialog.FilterIndex = 0;

            TryOpenSaveFile_Current(openFileDialog);
        }

        private void TryOpenSaveFile_Current(OpenFileDialog openFileDialog)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    {
                        Stream fileStream = openFileDialog.OpenFile();
                        XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
                        gameData.SaveGameData = serializer.Deserialize(fileStream) as SaveGameData;

                        if (gameData == null)
                            throw new Exception();

                        SystemComboBox.Items.Clear();
                        StationComboBox.Items.Clear();

                        foreach (StarSystem starSystem in gameData.StarSystems)
                            SystemComboBox.Items.Add(starSystem.Name);

                        SystemComboBox.SelectedIndex = 0;

                        fileStream.Close();
                    }
                }

            }
            catch
            {
 //               TryOpenSaveFile_0004(openFileDialog);
            }
        }


        private void UpdateSelectedCommodity(DataGridViewRow commodity)
        {
            if (gameData.StarSystems.Count == 0 || SystemComboBox.SelectedIndex < 0)
                return;

            StarSystem selectedSystem = gameData.StarSystems[SystemComboBox.SelectedIndex];

            if (selectedSystem.Stations.Count == 0 || StationComboBox.SelectedIndex < 0)
                return;

            Station selectedStation = selectedSystem.Stations[StationComboBox.SelectedIndex];

            int editedCommodityIndex = -1;
            int.TryParse(commodity.Cells["Index"].Value.ToString(), out editedCommodityIndex);

            if (editedCommodityIndex < 0)
                return;

            Commodity editedCommodity = selectedStation.Commodities[editedCommodityIndex];

            string editedName = (string)commodity.Cells["Commodity"].Value;
            decimal editedBuy = (decimal)commodity.Cells["BuyPrice"].Value;
            decimal editedSell = (decimal)commodity.Cells["SellPrice"].Value;
            decimal editedSupply = (decimal)commodity.Cells["Supply"].Value;

            editedCommodity.Name = editedName;
            editedCommodity.BuyPrice = editedBuy;
            editedCommodity.SellPrice = editedSell;
            editedCommodity.Supply = editedSupply;
            editedCommodity.LastUpdated = DateTime.Now;
            editedCommodity.PriceCheckRequired = false;

            commodity.Cells["LastUpdated"].Value = editedCommodity.LastUpdated;
        }

        private Station GetSeletedStation()
        {
            if (gameData.StarSystems.Count == 0 || SystemComboBox.SelectedIndex < 0)
                return null;

            StarSystem selectedSystem = gameData.StarSystems[SystemComboBox.SelectedIndex];

            if (selectedSystem.Stations.Count == 0 || StationComboBox.SelectedIndex < 0)
                return null;

            return selectedSystem.Stations[StationComboBox.SelectedIndex];
        }
        private void UpdateSelectedStation(List<Commodity> list)
        {
            Station selectedStation = GetSeletedStation();
            if (selectedStation != null)
            {
                selectedStation.Commodities = list;
                BindCommodities();
            }
        }


        private void CommoditiesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GoodsTable.Columns[e.ColumnIndex].Name != "Delete" &&
                GoodsTable.Columns[e.ColumnIndex].Name != "Up" &&
                GoodsTable.Columns[e.ColumnIndex].Name != "Down")
                return;

            int selectedSystemIndex = SystemComboBox.SelectedIndex;
            int selectedStationIndex = StationComboBox.SelectedIndex;
            int selectedCommodityIndex = int.Parse(GoodsTable.Rows[e.RowIndex].Cells["Index"].Value.ToString());

            switch (GoodsTable.Columns[e.ColumnIndex].Name.ToString())
            {
                case "Delete":
                    gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities.RemoveAt(selectedCommodityIndex);
                    break;
                case "Up":
                    if (selectedCommodityIndex > 0)
                    {
                        Commodity temp = gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities[selectedCommodityIndex - 1];
                        gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities[selectedCommodityIndex - 1] = gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities[selectedCommodityIndex];
                        gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities[selectedCommodityIndex] = temp;
                    }
                    break;
                case "Down":
                    if (selectedCommodityIndex < gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities.Count - 1)
                    {
                        Commodity temp = gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities[selectedCommodityIndex];
                        gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities[selectedCommodityIndex] = gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities[selectedCommodityIndex + 1];
                        gameData.StarSystems[selectedSystemIndex].Stations[selectedStationIndex].Commodities[selectedCommodityIndex + 1] =  temp;
                    }
                    break;
                default:
                    break;
            }

            BindCommodities();
        }

        private void saveDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }
        private void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void exitWithSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Close();
        }
        private void saveAndExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit without saving?", "Exit", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
                return;

            if (result == DialogResult.Yes)
                this.Close();
        }

        private void TimestampAllButton_Click(object sender, EventArgs e)
        {
            if (SystemComboBox.SelectedIndex < 0)
                return;

            StarSystem selectedSystem = gameData.StarSystems[SystemComboBox.SelectedIndex];

            if (StationComboBox.SelectedIndex < 0)
                return;

            Station selectedStation = selectedSystem.Stations[StationComboBox.SelectedIndex];

            DateTime timeStamp = DateTime.Now;

            foreach (Commodity commodity in selectedStation.Commodities)
                commodity.LastUpdated = timeStamp;

            BindCommodities();
        }

        private void GrabDataButton_Click(object sender, EventArgs e)
        {
            GrabData gd = new GrabData(gameData);
            gd.Left = this.Left + 20;
            gd.Top = this.Top + 20; ;
            gd.SetStation(StationComboBox.Text);
            gd.SetSystem(SystemComboBox.Text);
            gd.ShowDialog();
            var data = gd.GetData();
            if (data != null)
            {
                UpdateSelectedStation(data);
            }
            else
            {
                Station station = GetSeletedStation();
                if (station != null)
                {
                    gd.GetUpdate(station.Commodities);
                    BindCommodities();
                }
            }
        }

        private void FindCommodityButton_Click(object sender, EventArgs e)
        {
            FindCommodity fc = new FindCommodity(gameData, gameData);
            fc.Left = this.Left + 20;
            fc.Top = this.Top + 20; ;
            fc.ShowDialog();
        }
    }


    class CustomDataGridView : DataGridView
    {
        public CustomDataGridView()
        {
            DoubleBuffered = true;
        }
    }
}
