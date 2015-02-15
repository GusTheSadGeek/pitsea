using Microsoft.Win32;
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

        private static Main _instance;
        public static Main Instance
        {
            get { return _instance; }
        }

        public Main()
        {
            _instance = this;

            object left = ReadReg("MainLeft");
            object top = ReadReg("MainTop");

            if ( (left != null) && (top != null) )
            {
                this.Left = (int)left;
                this.Top = (int)top;
            }
            else
            {
                var Z = Screen.AllScreens;
                if (Z.Length == 1)
                {
                    // 1 screen only - not really ideal !!
                    this.Left = 20;
                    this.Top = 20;
                }
                else
                {
                    foreach(var screen in Z)
                    {
                        if (!screen.Primary)
                        {
                            //default to a secondary screen
                            this.Left = screen.Bounds.X + 20;
                            this.Top = screen.Bounds.Y + 20;
                            break;
                        }
                    }
                }
            }


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

            StarSystem system = SystemComboBox.SelectedItem as StarSystem;
            gameData.StarSystems.Remove(system);

            RefreshSystemComboBox();
////        SystemComboBox.Items.Remove(system);

//            if (gameData.StarSystems.Count > 0)
//                SystemComboBox.SelectedIndex = 0;
//            else
//            {
//                SystemComboBox.Text = string.Empty;
//                StationComboBox.Items.Clear();
//                StationComboBox.Text = string.Empty;
//                BindCommodities();
//            }
        }
        private bool updatingSystemComboBox = false;
        private void SystemComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SystemComboBox.SelectedIndex < 0)
                return;
            if (updatingSystemComboBox)
                return;

            try
            {
//                updatingSystemComboBox = true;

                StarSystem selectedSystem = SystemComboBox.SelectedItem as StarSystem;
                if (gameData.CurrentSystemId != selectedSystem.Id)
                {
                    gameData.CurrentSystemId = selectedSystem.Id;
                    RefreshSystemComboBox();
                }

                StationComboBox.DataSource = selectedSystem.Stations;
                StationComboBox.DisplayMember = "Name";
                StationComboBox.ValueMember = "Name";

                if (StationComboBox.Items.Count > 0)
                {
                    StationComboBox.SelectedIndex = 0;
                    int i = 0;
                    foreach (Station s in selectedSystem.Stations)
                    {
                        if (s.Id == gameData.CurrentStationId)
                        {
                            StationComboBox.SelectedIndex = i;
                            break;
                        }
                        ++i;
                    }
                }
                else
                {
                    StationComboBox.Text = string.Empty;
                    BindCommodities();
                }
                GrabDataButton.Enabled = (StationComboBox.SelectedIndex >= 0);
            }
            finally
            {
//                updatingSystemComboBox = false;
            }
        }

        private void AddStationButton_Click(object sender, EventArgs e)
        {
            if (SystemComboBox.Items.Count == 0)
                return;

            AddStationDialog dialog = new AddStationDialog((SystemComboBox.SelectedItem as StarSystem).Name);
            dialog.ShowDialog();

            if (dialog.Result == null)
                return;

            gameData.StarSystems[SystemComboBox.SelectedIndex].Stations.Add(dialog.Result);
            StationComboBox.Items.Add(dialog.Result.Name);

            StationComboBox.SelectedIndex = StationComboBox.Items.Count - 1;
        }
        private void RemoveStationButton_Click(object sender, EventArgs e)
        {

            Station selectedStation = StationComboBox.SelectedItem as Station;
            StarSystem selectedSystem = SystemComboBox.SelectedItem as StarSystem;

            if ((selectedStation == null) || (selectedSystem == null))
            {
                return;
            }

            selectedSystem.Stations.Remove(selectedStation);

            // Force redraw of station
            int i = SystemComboBox.SelectedIndex;
            SystemComboBox.SelectedIndex = i + 1;
            SystemComboBox.SelectedIndex = i;

            if (selectedSystem.Stations.Count > 0)
                StationComboBox.SelectedIndex = 0;
            else
            {
                StationComboBox.Text = string.Empty;
                BindCommodities();
            }
        }
        private void StationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCommodities();
            Station selectedStation = StationComboBox.SelectedItem as Station;
            distanceBox.Text = selectedStation.DistanceToStar.ToString();
            GrabDataButton.Enabled = (SystemComboBox.SelectedIndex >= 0);
        }

        private void BindCommodities()
        {
            bindingTable = new DataTable();


//            bindingTable.coRowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing

            bindingTable.Columns.Add("Id");
            bindingTable.Columns.Add("Category");
            bindingTable.Columns.Add("Commodity");
            bindingTable.Columns.Add("SellPrice");
            bindingTable.Columns.Add("BuyPrice");
            bindingTable.Columns.Add("Supply");
            bindingTable.Columns.Add("LastUpdated");
            bindingTable.Columns.Add("PC");

            bindingTable.Columns["Id"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["BuyPrice"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["SellPrice"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["Supply"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["Supply"].DataType = System.Type.GetType("System.Decimal");
            bindingTable.Columns["LastUpdated"].DataType = System.Type.GetType("System.DateTime");
            bindingTable.Columns["PC"].DataType = System.Type.GetType("System.Boolean");

            GoodsTable.Columns.Clear();

            Station selectedStation = StationComboBox.SelectedItem as Station;

            if (selectedStation == null)
                return;

            int index = 0;




            foreach (Commodity commodity in selectedStation.CommoditiesSorted())
            {
                DataRow newRow = bindingTable.NewRow();
                newRow["Id"] = commodity.id;
                newRow["Category"] = commodity.Cat;
                newRow["Commodity"] = commodity.NiceName;
                newRow["SellPrice"] = commodity.SellPrice;
                newRow["BuyPrice"] = commodity.BuyPrice;
                newRow["Supply"] = commodity.Supply;
                newRow["LastUpdated"] = commodity.LastUpdated;
                newRow["PC"] = commodity.PriceCheckRequired;
                bindingTable.Rows.Add(newRow);
                index++;
            }

            GoodsTable.DataSource = bindingTable;

            GoodsTable.Columns["Id"].Visible = false;
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
                    string v = row.Cells[2].Value.ToString().ToUpper();
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
                        case "INDUSTRIAL MATERIALS":
                            c = Color.Cornsilk;
                            break;
                        case "LEGAL DRUGS":
                            c = Color.Aquamarine;
                            break;
                        case "DRUGS":
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
            if (SystemComboBox.SelectedItem == null) return;

            gameData.Capital = CapitalNumericUpDown.Value;
            gameData.CargoSlots = CargoSlotsNumericUpDown.Value;
            gameData.JumpDist = JumpUpDown.Value;

            if (gameData.Capital <= 0 || gameData.CargoSlots <= 0)
            {
                MessageBox.Show("Please provide your available capital and cargo slots.", "Error", MessageBoxButtons.OK);
                return;
            }

            string homeSystemName = SystemComboBox.SelectedItem.ToString();
            StarSystem homeSystem = SystemComboBox.SelectedItem as StarSystem;

            Adviser.CalculateAll(gameData, homeSystem);

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
                SaveData(saveFileDialog.FileName);
            }
            WriteReg("lastsavepath", saveFileDialog.FileName);
        }

        private void AutoSave()
        {
            string lastSavePath = ReadReg("lastsavepath") as string;
            if (lastSavePath != null)
            {
                InfoBoxA ib = new InfoBoxA();
                ib.Show();
                ib.Message("Autosaving.......");
                string path = Path.GetDirectoryName(lastSavePath);
                SaveData(path + @"\autosave.pitdata");
                ib.Close();
            }

            WriteReg("MainLeft", this.Left);
            WriteReg("MainTop", this.Top);
        }

        private void AutoLoad()
        {
            string lastSavePath = ReadReg("lastsavepath") as string;
            bool loaded = false;
            if (lastSavePath != null)
            {
                try
                {
                    string path = Path.GetDirectoryName(lastSavePath);
                    InfoBoxA ib = new InfoBoxA();
                    ib.Show();
                    ib.Message("Autoloading.......");
                    loaded = TryLoadFile(path + @"\autosave.pitdata");
                    ib.Close();
                }
                catch
                {
                    // Fail silently...
                }
            }
            if (!loaded)
            {
                TryLoadFile("DEFAULT");
            }
        }
        
        private void SaveData(string filepath)
        {
            try
            {
                gameData.Capital = CapitalNumericUpDown.Value;
                gameData.CargoSlots = CargoSlotsNumericUpDown.Value;
                gameData.JumpDist = JumpUpDown.Value;
                try
                {
                    gameData.CurrentStationId = (StationComboBox.SelectedItem as Station).Id;
                    gameData.CurrentSystemId = (SystemComboBox.SelectedItem as StarSystem).Id;
                }
                catch
                {
                    gameData.CurrentStationId = 0;
                    gameData.CurrentSystemId = 0;
                }

                StreamWriter fileStream = new StreamWriter(filepath);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
                serializer.Serialize(fileStream, gameData.SaveGameData);
                fileStream.Close();
            }
            catch (Exception e)
            {
                var a = e;
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
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                TryLoadFile(openFileDialog.FileName);
            }
        }

        private bool TryLoadFile(String filepath)
        {
            bool ret = false;
            try
            {
                {
                    if (filepath == "DEFAULT")
                    {
                        gameData = new GameData();
                        String q = (new InitData()).InitString;
                        var reader = new StringReader(q);
                        XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
                        gameData.SaveGameData = serializer.Deserialize(reader) as SaveGameData;
                    }
                    else
                    {
                        StreamReader fileStream = new StreamReader(filepath);
                        XmlSerializer serializer = new XmlSerializer(typeof(SaveGameData));
                        gameData = new GameData();
                        gameData.SaveGameData = serializer.Deserialize(fileStream) as SaveGameData;
                        fileStream.Close();
                    }

                    if (gameData == null)
                        throw new Exception();

                    RefreshSystemComboBox();

                    SystemComboBox_SelectedIndexChanged(this, EventArgs.Empty);

                    CapitalNumericUpDown.Value = gameData.Capital;
                    CargoSlotsNumericUpDown.Value = gameData.CargoSlots;
                    JumpUpDown.Value = gameData.JumpDist;
                    ret = true;
                }
            }
            catch(Exception e)
            {
                var a = e;
                ret = false;
                //InfoBoxA ib = new InfoBoxA();
                //ib.Message(e.ToString());
                //ib.ShowDialog();
                //ib.Close();
                //               TryOpenSaveFile_0004(openFileDialog);
            }
            return ret;
        }

        private void RefreshSystemComboBox()
        {
            if (updatingSystemComboBox) return;
            try
            {
                SystemComboBox.SuspendLayout();
                StarSystem selectedSystem = SystemComboBox.SelectedItem as StarSystem; 
                updatingSystemComboBox = true;
                SystemComboBox.DataSource = null;
                SystemComboBox.Items.Clear();
                SystemComboBox.DisplayMember = "Name";
                SystemComboBox.ValueMember = "Name";
                int i = 0;

                bool allSystems = (allSystemsCheckBox.CheckState == CheckState.Checked);
                bool localSystems = (localSystemCheckBox.CheckState == CheckState.Checked);

                List<StarSystem> newDataSource;
                if (allSystems)
                {
                    newDataSource = gameData.StarSystems;
                }
                else
                {
                    if ((selectedSystem != null) && (localSystems))
                    {
                        newDataSource = gameData.StarSystems.FindAll(x => x.isWithinRange(selectedSystem, 30) && (x.Stations.Count > 0));
                    }
                    else
                    {
                        newDataSource = gameData.StarSystems.FindAll(x => x.Stations.Count > 0);
                    }
                }

                newDataSource.Sort(delegate(StarSystem c1, StarSystem c2) { return c1.Name.CompareTo(c2.Name); });


                SystemComboBox.DataSource = newDataSource;
                if (newDataSource.Count > 0)
                {
                    SystemComboBox.SelectedIndex = 0;
                }
                foreach (StarSystem s in (SystemComboBox.DataSource as List<StarSystem>))
                {
                    if (s.Id == gameData.CurrentSystemId)
                    {
                        SystemComboBox.SelectedIndex = i;
                        break;
                    }
                    ++i;
                }
                SystemComboBox_SelectedIndexChanged(this, EventArgs.Empty);
            }
            finally
            {
                updatingSystemComboBox = false;
                SystemComboBox.ResumeLayout();

            }
        }

        private void UpdateSelectedCommodity(DataGridViewRow commodity)
        {
            Station selectedStation = StationComboBox.SelectedItem as Station;

            int editedCommodityId = -1;
            int.TryParse(commodity.Cells["Id"].Value.ToString(), out editedCommodityId);

            if (editedCommodityId < 0)
                return;

            Commodity editedCommodity = selectedStation.Commodities.Find(x => x.id == editedCommodityId);
            if (editedCommodity != null)
            {
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
        }

        public Station GetSeletedStation()
        {
            return StationComboBox.SelectedItem as Station;
        }

        public StarSystem GetSeletedSystem()
        {
            return SystemComboBox.SelectedItem as StarSystem;
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
            if (GoodsTable.Columns[e.ColumnIndex].Name != "Delete" )
                return;

            int selectedCommodityId = int.Parse(GoodsTable.Rows[e.RowIndex].Cells["Id"].Value.ToString());

            switch (GoodsTable.Columns[e.ColumnIndex].Name.ToString())
            {
                case "Delete":
                    GetSeletedStation().Commodities.RemoveAll(x => x.id == selectedCommodityId); 
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
            Station selectedStation = StationComboBox.SelectedItem as Station;
            if (selectedStation != null)
            {
                DateTime timeStamp = DateTime.Now;

                foreach (Commodity commodity in selectedStation.Commodities)
                    commodity.LastUpdated = timeStamp;

                BindCommodities();
            }
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
                Station station = StationComboBox.SelectedItem as Station;
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

        private void Main_Deactivate(object sender, EventArgs e)
        {
        }

        private void configureCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigScreenCapture gd = new ConfigScreenCapture(gameData);
            gd.Left = this.Left + 20;
            gd.Top = this.Top + 20; ;
            gd.ShowDialog();
        }

        private void loadEddbStationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoBoxA ib = new InfoBoxA();
            ib.Show();

            eddb_systems s = new eddb_systems();
            s.load_json();
//            s.save_json(@"d:\systems2.json");
            s.import_from_json();
            InfoBoxA.Instance.Message("DONE");
            InfoBoxA.Instance.Message("DONE");
            ib.Hide();
            ib.ShowDialog();
            RefreshSystemComboBox();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                AutoSave();
                Application.Exit();
            }
        }

        public bool WriteReg(string KeyName, object Value)
        {
            try
            {
                // Setting
                RegistryKey rk = Registry.CurrentUser;
                String subKey = @"SOFTWARE\" + Application.ProductName;

                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey sk1 = rk.CreateSubKey(subKey);
                // Save the value
                sk1.SetValue(KeyName.ToUpper(), Value);

                return true;
            }
            catch (Exception e)
            {
                // AAAAAAAAAAARGH, an error!
                //ShowErrorMessage(e, "Writing registry " + KeyName.ToUpper());
                return false;
            }
        }

        public object ReadReg(string KeyName)
        {
            // Opening the registry key
            RegistryKey rk = Registry.CurrentUser;
            String subKey = @"SOFTWARE\" + Application.ProductName;

            // Open a subKey as read-only
            RegistryKey sk1 = rk.OpenSubKey(subKey);
            // If the RegistrySubKey doesn't exist -> (null)
            if (sk1 == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return sk1.GetValue(KeyName.ToUpper());
                }
                catch (Exception e)
                {
                    // AAAAAAAAAAARGH, an error!
//                    ShowErrorMessage(e, "Reading registry " + KeyName.ToUpper());
                    return null;
                }
            }
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            AutoLoad();
        }

        private void distanceBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void distanceBox_Leave(object sender, EventArgs e)
        {
            double val = 0;
            double.TryParse( distanceBox.Text , out val );
            Int64 v = (Int64)Math.Round(val);

            Station s = GetSeletedStation();
            s.DistanceToStar = v;
        }

        private void allSystemsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SystemComboBox.SelectedItem != null)
            {
                gameData.CurrentSystemId = (SystemComboBox.SelectedItem as StarSystem).Id;
                RefreshSystemComboBox();
            }
        }

        private void localSystemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SystemComboBox.SelectedItem != null)
            {
                gameData.CurrentSystemId = (SystemComboBox.SelectedItem as StarSystem).Id;
                RefreshSystemComboBox();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
            about.Dispose();
        }

        private void PurgeDataButton_Click(object sender, EventArgs e)
        {
            int purgeVal = (int)PurgeDaysUpDown.Value;

            DateTime purgeTime = DateTime.Now.AddDays(-purgeVal);

            foreach (StarSystem system in gameData.StarSystems)
            {
                foreach (Station station in system.Stations)
                {
                    station.Commodities.RemoveAll(X => X.LastUpdated < purgeTime);    
                }
            }
            BindCommodities();
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
