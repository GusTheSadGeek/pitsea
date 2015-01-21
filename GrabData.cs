using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Tesseract;


namespace Pitsea
{
    public partial class GrabData : Form
    {
        private List<GrabCommodity> commodities = new List<GrabCommodity>();
        string scanned_text;
        private Market market = new Market();
        bool dataSubmitted = false;
        bool dataUpdate = false;
        private GameData gcd;
        private TesseractEngine engine;
        public GrabData(GameData grabComData)
        {
            gcd = grabComData;
            InitializeComponent();
            init_commodities(grabComData);
            Environment.SetEnvironmentVariable("TESSDATA_PREFIX", null);
            engine = new TesseractEngine(@"./tessdata", "gus", EngineMode.TesseractOnly);
        }

        public List<Commodity> GetData()
        {
            if (dataSubmitted)
                return market.GetCommodities();
            else
                return null;
        }
        public void GetUpdate(List<Commodity> list)
        {
            if (dataUpdate)
                market.updateCommodities(list);
        }


        public void SetStation(string text)
        {
            StationName.Text = text;
        }
        public void SetSystem(string text)
        {
            SystemName.Text = text;
        }

        private long timestamp_ms()
        {
            return (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 10000;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            InfoBox.Text = "-3";
            InfoBox.Update();
            Thread.Sleep(1000);
            InfoBox.Text = "-2";
            InfoBox.Update();
            Thread.Sleep(1000);
            InfoBox.Text = "-1";
            InfoBox.Update();
            Thread.Sleep(1000);
            InfoBox.Text = "0";
            InfoBox.Update();
            button1.Text = "GO !!";
            button1.Update();
            long ticks = timestamp_ms() + 20000;
            while (ticks > timestamp_ms())
            {
                scan_and_process();
                textBox1.Text = market.get_market_string();
                textBox1.Update();
                InfoBox.Text = ((ticks - timestamp_ms()) / 1000).ToString();
                InfoBox.Update();
//                int pauseTime = (int)(ticks - timestamp_ms());
//                if (pauseTime > 0)
//                    Thread.Sleep(pauseTime);
            }
            textBox1.Text += "\r\nX";
            button1.Text = "START";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            scan_and_process();
            textBox1.Text = market.get_market_string();
        }

        private void scan_and_process()
        {
            Bitmap mdata = grab_market_data();
            scanned_text = scan_bitmap(mdata);
            process_scanned_text(scanned_text);
        }

        //private void scan_and_process_station_name()
        //{
        //    Bitmap mdata = grab_station_name();
        //    scanned_text = scan_bitmap(mdata);
        //    StationName.Text = scanned_text;
        //}

        //private Bitmap grab_station_name()
        //{
        //                Rectangle cropRect = new Rectangle(50, 100, 1250, 1000);
        //    //Rectangle cropRect = new Rectangle(50, 110, 800, 52);
        //    Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

        //    using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
        //                                                Screen.PrimaryScreen.Bounds.Height))
        //    {
        //        using (Graphics g = Graphics.FromImage(bmpScreenCapture))
        //        {
        //            g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
        //                             Screen.PrimaryScreen.Bounds.Y,
        //                             0, 0,
        //                             bmpScreenCapture.Size,
        //                             CopyPixelOperation.SourceCopy);

        //            using (Graphics g2 = Graphics.FromImage(target))
        //            {
        //                g2.DrawImage(bmpScreenCapture, new Rectangle(0, 0, target.Width, target.Height),
        //                                 cropRect,
        //                                 GraphicsUnit.Pixel);
        //            }

        //            target.Save("d:\\gus.elite.exp_station.bmp");
        //        }
        //    }
        //    return target;
        //}

        private Bitmap grab_market_data()
        {
//            Rectangle cropRect = new Rectangle(50, 100, 1250, 1000);
//            Rectangle cropRect = new Rectangle(100, 230, 1250, 650);
            Rectangle cropRect = new Rectangle(50, 295, 1250, 750); 
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Bitmap bmpScreenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                                        Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     bmpScreenCapture.Size,
                                     CopyPixelOperation.SourceCopy);

//                    bmpScreenCapture.Save("d:\\q2.bmp");
                    
                    using (Graphics g2 = Graphics.FromImage(target))
                    {
                        g2.DrawImage(bmpScreenCapture, new Rectangle(0, 0, target.Width, target.Height),
                                         cropRect,
                                         GraphicsUnit.Pixel);
                    }

                    //long ticks = DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
                    //ticks /= 10000000; //Convert windows ticks to seconds
                    //string timestamp = ticks.ToString();
                    //timestamp = "X";
                    //target.Save("d:\\gus.elite.exp"+timestamp+".bmp");
                }
            }
            return target;
        }




        private string scan_bitmap(Bitmap bmp)
        {
            string text="";
            try
            {
               // using (var engine = new TesseractEngine(@"./tessdata", "gus", EngineMode.TesseractOnly))
                {
                    using (var img = bmp)
                    {
                        using (var page = engine.Process(img, PageSegMode.SingleBlock))
                        {
                            text = page.GetText();
                            page.Image.Dispose();
                            page.Dispose();
                        }
                    }
                }
            }
            catch (Exception e)
            {
//                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
            }
            return text;
        }

        private void process_scanned_text(string text)
        {
            int q = text.IndexOf('\t');

            var lines = text.Split('\n');
            List<string> rawlines = new List<string>();

            foreach (string line in lines)
            {
                if (line.Trim().Length > 0)
                {
                    rawlines.Add(line.Replace(",", "").Replace(".", ""));
                }
            }



            StringBuilder sb = new StringBuilder();
            foreach (string line in rawlines)
            {
                sb.Append(find_goods(line) + "\r\n");
            }
//            textBox1.Text = sb.ToString();
        }

        //private void commoditiesAdd(GrabCommodity com)
        //{
        //    GrabCommodityDatum datum = new GrabCommodityDatum();

        //    if (com.Category)
        //    {
        //        datum.Category = com.Name;
        //        datum.Names = null;
        //    }
        //    else
        //    {
        //        datum.Category = com.CategoryName;
        //        datum.Names = com.Names;
        //    }
        //    gcd.CommodityList.Add(datum);

        //    commodities.Add(com);
        //    market.add_commoditity(com);
        //}

        //private void save_commodities(string path)
        //{
        //    using (StreamWriter sw = new StreamWriter(path))
        //    {
        //        foreach (GrabCommodity com in commodities)
        //        {
        //            sw.WriteLine(com.SaveString);
        //        }
        //    }
        //}

        //private void load_commodities(string path)
        //{
        //    using (StreamReader sr = new StreamReader(path))
        //    {
        //        while (!sr.EndOfStream)
        //        {
        //            string line = sr.ReadLine();
        //            GrabCommodity com = new GrabCommodity(line, 2);
        //            commoditiesAdd(com);
        //        }
        //    }
        //}

        private void init_commodities(GameData gcd)
        {
            foreach (GrabCommodityDatum datum in gcd.CommodityList)
            {
                GrabCommodity com = new GrabCommodity(datum);
                commodities.Add(com);
                market.add_commoditity(com);
            }

            //commoditiesAdd(new GrabCommodity("CHEMICALS",1));   // Categpry
            //commoditiesAdd(new GrabCommodity("EXPLOSIVES", "CHEMICALS"));
            //commoditiesAdd(new GrabCommodity("HYDROGEN FUEL", "CHEMICALS"));
            //commoditiesAdd(new GrabCommodity("MINERAL OIL", "CHEMICALS"));
            //commoditiesAdd(new GrabCommodity("CONSUMER ITEMS",1));   // Categpry
            //commoditiesAdd(new GrabCommodity("CLOTHING", "CONSUMER ITEMS"));
            //commoditiesAdd(new GrabCommodity("CONSUMER TECHNOLOGY", "CONSUMER ITEMS"));
            //commoditiesAdd(new GrabCommodity("DOMESTIC APPLIANCES", "CONSUMER ITEMS"));

            //commoditiesAdd(new GrabCommodity("FOODS",1));   // Categpry
            //commoditiesAdd(new GrabCommodity("ANIMAL MEAT", "FOODS"));
            //commoditiesAdd(new GrabCommodity("ALGAE", "FOODS"));
            //commoditiesAdd(new GrabCommodity("ANIMAL MEAT", "FOODS"));
            //commoditiesAdd(new GrabCommodity("COFFEE", "FOODS"));
            //commoditiesAdd(new GrabCommodity("FISH", "FOODS"));
            //commoditiesAdd(new GrabCommodity("FOOD CARTRIDGES", "FOODS"));
            //commoditiesAdd(new GrabCommodity("FRUIT AND VEGETABLES", "FOODS"));
            //commoditiesAdd(new GrabCommodity("GRAIN", "FOODS"));
            //commoditiesAdd(new GrabCommodity("SYNTHETIC MEAT", "FOODS"));
            //commoditiesAdd(new GrabCommodity("TEA,IEA", "FOODS"));

            //commoditiesAdd(new GrabCommodity("INDUSTRIAL MATERIALS",1));   // Categpry
            //commoditiesAdd(new GrabCommodity("POLYMERS","INDUSTRIAL MATERIALS"));
            //commoditiesAdd(new GrabCommodity("SEMICONDUCTORS","INDUSTRIAL MATERIALS"));
            //commoditiesAdd(new GrabCommodity("SUPERCONDUCTORS","INDUSTRIAL MATERIALS"));

            //commoditiesAdd(new GrabCommodity("LEGAL DRUGS",1));   // Categpry
            //commoditiesAdd(new GrabCommodity("BEER,8EER","LEGAL DRUGS"));
            //commoditiesAdd(new GrabCommodity("LIQUOR","LEGAL DRUGS"));
            //commoditiesAdd(new GrabCommodity("TOBACCO","LEGAL DRUGS"));
            //commoditiesAdd(new GrabCommodity("WINE","LEGAL DRUGS"));

            //commoditiesAdd(new GrabCommodity("MACHINERY", 1));   // Categpry
            //commoditiesAdd(new GrabCommodity("ATMOSPHERIC PROCESSORS", "MACHINERY"));
            //commoditiesAdd(new GrabCommodity("CROP HARVESTER", "MACHINERY"));
            //commoditiesAdd(new GrabCommodity("MARINE EQUIPMENT", "MACHINERY"));
            //commoditiesAdd(new GrabCommodity("MICROBIAL FURNACES", "MACHINERY"));
            //commoditiesAdd(new GrabCommodity("MINERAL EXTRACTORS", "MACHINERY"));
            //commoditiesAdd(new GrabCommodity("POWER GENERATORS", "MACHINERY"));
            //commoditiesAdd(new GrabCommodity("WATER PURIFIERS", "MACHINERY"));

            //commoditiesAdd(new GrabCommodity("MEDICINES", 1));   // Categpry
            //commoditiesAdd(new GrabCommodity("AGRI MEDICINDES", "MEDICINES"));
            //commoditiesAdd(new GrabCommodity("BASIC MEDICINES", "MEDICINES"));
            //commoditiesAdd(new GrabCommodity("PERFORMANCE ENHANCERS", "MEDICINES"));
            //commoditiesAdd(new GrabCommodity("PROGENITOR CELLS", "MEDICINES"));

            //commoditiesAdd(new GrabCommodity("METALS", 1));   // Categpry
            //commoditiesAdd(new GrabCommodity("ALUMINIUM", "METALS"));
            //commoditiesAdd(new GrabCommodity("BERYLLIUM", "METALS"));
            //commoditiesAdd(new GrabCommodity("COBALT", "METALS"));
            //commoditiesAdd(new GrabCommodity("COPPER", "METALS"));
            //commoditiesAdd(new GrabCommodity("GALLIUM", "METALS"));
            //commoditiesAdd(new GrabCommodity("GOLD", "METALS"));
            //commoditiesAdd(new GrabCommodity("INDIUM", "METALS"));
            //commoditiesAdd(new GrabCommodity("LITHIUM", "METALS"));
            //commoditiesAdd(new GrabCommodity("PALLADIUM", "METALS"));
            //commoditiesAdd(new GrabCommodity("PLATINUM", "METALS"));
            //commoditiesAdd(new GrabCommodity("SILVER", "METALS"));
            //commoditiesAdd(new GrabCommodity("TANTALUM", "METALS"));
            //commoditiesAdd(new GrabCommodity("TITANIUM", "METALS"));
            //commoditiesAdd(new GrabCommodity("URANIUM", "METALS"));

            //commoditiesAdd(new GrabCommodity("MINERALS", 1));   // Categpry
            //commoditiesAdd(new GrabCommodity("BAUXITE", "MINERALS"));
            //commoditiesAdd(new GrabCommodity("BERTRANDITE", "MINERALS"));
            //commoditiesAdd(new GrabCommodity("COLTAN", "MINERALS"));
            //commoditiesAdd(new GrabCommodity("GALLITE", "MINERALS"));
            //commoditiesAdd(new GrabCommodity("INDITE", "MINERALS"));
            //commoditiesAdd(new GrabCommodity("LEPIDOLITE", "MINERALS"));
            //commoditiesAdd(new GrabCommodity("RUTILE", "MINERALS"));
            //commoditiesAdd(new GrabCommodity("URANINITE", "MINERALS"));


            //commoditiesAdd(new GrabCommodity("TECHNOLOGY", 1));   // Categpry
            //commoditiesAdd(new GrabCommodity("ADVANCED CATALYSERS", "TECHNOLOGY"));
            //commoditiesAdd(new GrabCommodity("AUTOFABRICATORS", "TECHNOLOGY"));
            //commoditiesAdd(new GrabCommodity("BIOREDUCING LICHEN", "TECHNOLOGY"));
            //commoditiesAdd(new GrabCommodity("COMPUTER COMPONENTS", "TECHNOLOGY"));
            //commoditiesAdd(new GrabCommodity("HE SUITS", "TECHNOLOGY"));
            //commoditiesAdd(new GrabCommodity("RESONATING SEPARATROS", "TECHNOLOGY"));
            //commoditiesAdd(new GrabCommodity("ROBOTICS", "TECHNOLOGY"));

            //commoditiesAdd(new GrabCommodity("TEXTILES", 1));   // Categpry
            //commoditiesAdd(new GrabCommodity("LEATHER", "TEXTILES"));
            //commoditiesAdd(new GrabCommodity("NATURAL FABRICS", "TEXTILES"));
            //commoditiesAdd(new GrabCommodity("SYNTHETIC FABRICS", "TEXTILES"));

            //commoditiesAdd(new GrabCommodity("WASTE", 1));   // Categpry
            //commoditiesAdd(new GrabCommodity("BIOWASTE", "WASTE"));
            //commoditiesAdd(new GrabCommodity("CHEMICAL WASTE", "WASTE"));
            //commoditiesAdd(new GrabCommodity("SCRAP", "WASTE"));


            //commoditiesAdd(new GrabCommodity("WEAPONS", 1));   // Categpry
            //commoditiesAdd(new GrabCommodity("NONLETHAL WEAPONS", "WEAPONS"));
            //commoditiesAdd(new GrabCommodity("REACTIVE ARMOUR","WEAPONS"));




            //commoditiesAdd(new GrabCommodity("UNCAT", 1));   // Categpry
            
            //commoditiesAdd(new GrabCommodity("PESTICIDES"));
            

            //commoditiesAdd(new GrabCommodity("ANIMAL MONITORS"));
            //commoditiesAdd(new GrabCommodity("AQUAPONIC SYSTEMS"));
            //commoditiesAdd(new GrabCommodity("LAND ENRICHMENT SYSYTEMS"));

            //save_commodities(@"d:\git\commodities.txt");


        }

        private string find_goods(string line)
        {

            float bestscore = 0f;
            string CurrentCategory = "-----------------------";
            GrabCommodity bestCommodity = null;
            foreach (var commodity in commodities)
            {

                float myscore = commodity.test(line, CurrentCategory);
                if (myscore > bestscore)
                {
                    bestscore = myscore;
                    bestCommodity = commodity;
                }
            }

            string info = "??  ";

            var bs = bestscore.ToString().PadRight(3,' ').Substring(0, 3);
            if (bestscore > 0.5)
            {
                info = "[" + bs + "] " + bestCommodity.Name;
                info = info.PadRight(40, ' ');
              //  line = line.Substring(bestCommodity.Name.Length);
            }
            else
            {
                if (bestCommodity != null)
                    info = "[" + bs + "] " + bestCommodity.Name + "###";
            }
            string values = "";
            if ((bestCommodity != null) && (bestscore > 0.9))
            {
                string newline = bestCommodity.remove_name(line);
                values = get_values(newline);
                bestCommodity.set_data(values);
  //              market.add_commoditity(bestCommodity);

                if (bestCommodity.Category)
                {
                    CurrentCategory = bestCommodity.Name;
                }
            }
            return info+values+line;
        }


        private string get_values(string line)
        {
            line = line.Replace("  ", " ");
            line = line.Replace("  ", " ");
            line = line.Replace("D", "0");

            string[] words = line.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                string w = words[i];
                int numcount = 0;

                foreach (char c in w)
                {
                    if ("0123456789".IndexOf(c) >= 0)
                    {
                        ++numcount;
                    }
                }
                float percent_num = ((float)numcount) / w.Length;
                if (!((percent_num > 0.5) || (w == "-")))
                {
                    words[i] = "";
                }
            }
            string q = string.Join(" ", words).Trim();
            q = q.Replace("  ", " ").Replace("  ", " ").Replace("B", "8").Replace("S", "5").Replace("I", "1");
            words = q.Split(' ');
            int j = 0;
            if (words.Length == 5)
            {
                string sell = words[j++];
                string buy = words[j++];
                string cargo = words[j++];
                string demandsupply = words[j++];
                string galactic = words[j++];
                return sell + " " + buy + " " + cargo + " " + demandsupply + " " + galactic;
            }
            return "$$";
        }

        private void GrabData_FormClosed(object sender, FormClosedEventArgs e)
        {
           // MainForm.Instance.IncommingData(56);
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            dataSubmitted = true;
            Close();
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            dataUpdate = true;
            Close();
        }
    }

    public class Market
    {
        List<GrabCommodity> goods = new List<GrabCommodity>();

        public Market()
        {

        }

        public List<Commodity> GetCommodities()
        {
            DateTime ts = DateTime.Now; 
            List<Commodity> comms = new List<Commodity>();
            foreach (GrabCommodity good in goods)
            {
                if ((good.DataValid) && (!good.Category))
                {
                    Commodity com = new Commodity();
                    com.BuyPrice = good.Buy;
                    com.SellPrice = good.Sell;
                    com.Name = good.Name;
                    com.Cat = good.CategoryName;
                    com.Supply = good.Demandsupply;
                    com.LastUpdated = ts;
                    comms.Add(com);
                }
            }
            return comms;
        }

        private double getPercentDiff(int a , int b)
        {
            if (a == b) return 0.0;
            if ((a == 0) && (b != 0)) return 1.0;

            int diff = Math.Abs(a - b);
            double percent = (double)diff / a;
            return percent;
        }

        public void updateCommodities(List<Commodity> comms)
        {
            DateTime ts = DateTime.Now;
            foreach (GrabCommodity good in goods)
            {
                if ((good.DataValid) && (!good.Category))
                {
                    Commodity com = comms.Find(a => a.Name == good.Name);
                    if (com == null)
                    {
                        com = new Commodity();
                        comms.Add(com);
                    }
                    else
                    {
                        double buyPercent = getPercentDiff((int)com.BuyPrice, good.Buy);
                        double sellPercent = getPercentDiff((int)com.SellPrice, good.Sell);

                        if ((buyPercent > 0.1) || (sellPercent > 0.1))
                        {
                            com.PriceCheckRequired = true;
                        }
                        else
                        {
                            com.PriceCheckRequired = false;
                        }
                    }
                    com.BuyPrice = good.Buy;
                    com.SellPrice = good.Sell;
                    com.Name = good.Name;
                    com.Cat = good.CategoryName;
                    com.Supply = good.Demandsupply;
                    com.LastUpdated = ts;
                }
            }
        }

        public bool add_commoditity(GrabCommodity com)
        {
            GrabCommodity x = goods.Find(a => a.Name == com.Name);
            if (x == null)
            {
                goods.Add(com);
            }
            else
            {
                x.update_data(com);
            }
            return true;
        }

        public string get_market_string()
        {
            StringBuilder sb = new StringBuilder();
            foreach (GrabCommodity c in goods)
            {
                string info = c.Info;
                if (info != null) sb.Append(c.Info+"\r\n");
            }
            return sb.ToString();
        }
    }


    public class GrabCommodityDatum
    {
        private string[] names;
        private string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public string Name
        {
            get { 
                if (names!=null) 
                    return names[0];
                else
                    return null;
            }
        }

        public string[] Names
        {
            get { return names; }
            set { names = value; }
        }

        public string NiceName
        {
            get
            {
                if (Name == null) return null;
                string[] names = Name.Split(' ');
                StringBuilder sb = new StringBuilder();
                foreach (string n in names)
                {
                    sb.Append(char.ToUpper(n[0]) + n.Substring(1).ToLower() + ' ');
                }
                return sb.ToString().Trim();
            }
        }
    }


    public class GrabCommodity
    {
        private string[] names;
        private string category;
        private bool cat=false;
        private List<int> buy = new List<int>();
        private List<int> sell = new List<int>();
        private List<int> cargo = new List<int>();
        private List<int> demandsupply = new List<int>();
        private List<int> galactic = new List<int>();
        private bool valid_data = false;
        private int hcount;
        private int buyCount;

        public GrabCommodity(GrabCommodityDatum datum)
        {
            category = datum.Category;
            if (datum.Names == null)
            {
                cat = true;
                names = datum.Category.Split(',');
            }
            else
            {
                cat = false;
                names = datum.Names;
            }
        }


        //public GrabCommodity(string name)
        //{
        //    names = name.Split(',');
        //}

        //// TODO del
        //public GrabCommodity(string name, int type)
        //{
        //    if (type == 2)
        //    {
        //        InitFromSaveString(name);
        //    }
        //    else
        //    {
        //        if (type == 1)
        //        {
        //            cat = true;
        //        }
        //        names = name.Split(',');
        //    }
        //}

        //private void initCatName(string name)
        //{
        //    cat = true;
        //    category = "";
        //    names = name.Split(',');
        //}

        //private void init(string name, string type)
        //{
        //    cat = false;
        //    category = type;
        //    names = name.Split(',');
        //}

        //public GrabCommodity(string name, string type)
        //{
        //    init(name, type);
        //}
        public bool DataValid
        {
            get { return valid_data; }
        }

        public bool Category
        {
            get { return cat; }
        }

        public string CategoryName
        {
            get { return category; }
        }

        public string Name
        {
            get { return names[0]; }
        }
        public string[] Names
        {
            get { return names; }
        }

        //public string SaveString
        //{
        //    get {
        //        if (cat)
        //        {
        //            return  names[0]+":-";
        //        }
        //        else
        //        {
        //            return CategoryName + ":" + string.Join(",", names);
        //        }
        //    }
        //}

        //public void InitFromSaveString(string s)
        //{
        //    string [] q = s.Split(':');
        //    if (s[0] == '-')
        //    {
        //        names = q[0].Split(',');
        //        cat = true;
        //    }
        //    else
        //    {
        //        init(q[0], q[1]);
        //    }
        //}

        public string Info
        {
            get {
                if (cat)
                    return names[0];
                else
                {
                    if (valid_data)
                        return names[0].PadRight(25) + "\t" + Sell + "\t" + Buy + "\t" + Cargo + "\t" + Demandsupply + "\t" + Galactic + "\t" + buy.Count.ToString()+"/"+buyCount.ToString();
                    else
                        return null;
                }
            }
        }

        public int Buy
        {
            get { int q = getBestValue(buy);
            buyCount = hcount; return q;
            }
        }

        public int Sell
        {
            get { return getBestValue(sell); }
        }

        public int Cargo
        {
            get { return getBestValue(cargo); }
        }

        public int Demandsupply
        {
            get { return getBestValue(demandsupply); }
        }

        public int Galactic
        {
            get { return getBestValue(galactic); }
        }


        private int getBestValue(List<int> vl)
        {
            if (vl == null)
            {
                return -1;
            }
            Hashtable hashtable = new Hashtable();

            foreach (int v in vl)
            {
                if (hashtable.Contains(v))
                {
                    hashtable[v] = (int)hashtable[v] + 1;
                }
                else
                {
                    hashtable.Add(v, 1);
                }
            }
            
            int mostCommonValue = -1;
            int secondMostCommonValue = -1;
            int highestCount = 0;
            int secondHighestCount = 0;
            foreach (int key in hashtable.Keys)
	        {
                int count = (int)hashtable[key];
                if (count > highestCount)
                {
                    secondHighestCount = highestCount;
                    secondMostCommonValue = mostCommonValue;
                    highestCount = count;
                    mostCommonValue = key;
                }
	        }
            if (mostCommonValue == 0)
            {
                if ((secondHighestCount * 1.5) > highestCount)
                {
                    highestCount = secondHighestCount;
                    mostCommonValue = secondMostCommonValue;
                }
            }
            hcount = highestCount;
            return mostCommonValue;
        }

        private void addValue(List<int> list, int value)
        {
            if (value >= 0)
            {
                list.Add(value);
            }
        }

        private int getValueFromString(string s)
        {
            int value = 0;
            if (s.IndexOf('-') < 0)
            {
                try
                {
                    value = Int32.Parse(s);
                }
                catch
                {
                    value = -1;
                }
            }
            return value;
        }

        public string remove_name(string line)
        {
            int wordcount = names[0].Split(' ').Length;
            var words = line.Split(' ').ToList();

            while (wordcount > 0)
            {
                words.RemoveAt(0);
                wordcount--;
            }
            string [] wordsarray = words.ToArray();

            return string.Join(" ", wordsarray);
        }

        public void set_data(string data)
        {
            var values = data.Trim().Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Split(' ');
            if (values.Length == 5)
            {
                int tempsell = getValueFromString(values[0]);
//                int tempsd = getValueFromString(values[3]);
                int tempgal = getValueFromString(values[4]);

                if ((tempsell > 0)  && (tempgal > 0))
                {
                    valid_data = true;
                    addValue(sell, tempsell);
                    addValue(buy, getValueFromString(values[1]));
                    addValue(cargo, getValueFromString(values[2]));
                    addValue(demandsupply, getValueFromString(values[3]));
                    addValue(galactic, tempgal);
                }
            }
        }

        public void update_data(GrabCommodity c)
        {
            if ((c.Sell > 0)  && (c.Galactic > 0))
            {
                valid_data = true; 
                addValue(buy, c.Buy);
                addValue(sell, c.Sell);
                addValue(cargo, c.Cargo);
                addValue(demandsupply, c.Demandsupply);
                addValue(galactic, c.Galactic);
            }
        }

        private string get_candidate(string line)
        {
            var words = line.Split(' ');

            StringBuilder candidate = new StringBuilder();
            int count = 0;
            foreach (string w in words)
            {
                bool bad = false;
                int numcount = 0;
                foreach (char c in w)
                {
                    if (count > 0)
                    {
                        if ("123456789".IndexOf(c) >= 0)
                        {
                            if (++numcount > 1)
                            {
                                bad = true;
                                break;
                            }
                        }
                    }
                }
                if (!bad)
                {
                    candidate.Append(w);
                    candidate.Append(' ');
                }
                else
                {
                    break;
                }
                count++;
            }
            return candidate.ToString().Replace("-","");
        }


        public float test(string line, string currentCat="")
        {
            var words = line.Split(' ');

            string candidate = get_candidate(line);

            float highscore = compare(candidate);

            if (candidate.Length > 1)
            {
                float score = compare(candidate.Substring(1));
                if (score > highscore) highscore = score;
                if (highscore < .8)
                {
                    if (candidate.Length > 6)
                    {
                        score = compare(candidate.Substring(2));
                        if (score > highscore) highscore = score;

                        score = compare(candidate.Substring(3));
                        if (score > highscore) highscore = score;
                    }
                }
            }
            if (currentCat == CategoryName)
            {
                highscore *= 2;
            }
            return highscore;
        }

        private float compare(string line)
        {
            float highscore = 0f;
            foreach (string alt in names)
            {
                int l = alt.Length;
                if (l <= line.Length)
                {
                    int myscore = 0;
                    for (int n = 0; n < l; n++)
                    {
                        if ((line[n] != '-') && (line.Length > n))
                        {
                            if ((line[n] == alt[n]) || ((line[n] == 'D') && (alt[n] == 'O')) || ((line[n] == 'O') && (alt[n] == 'D')) || ((line[n] == '8') && (alt[n] == 'B')))
                            {
                                myscore++;
                            }
                        }
                    }
                    float thisScore = ((float)myscore) / l;
                    if (thisScore > highscore)
                    {
                        highscore = thisScore;
                    }
                }
            }
            return highscore;
        }

    }
}
