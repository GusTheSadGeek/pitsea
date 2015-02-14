using System;
using System.Text;

namespace Pitsea
{
    public class CommodityCategory
    {
        public Int32  id;
        public String name;
        public String scanName;
    }

    public class CommodityType
    {
        public Int32 id;
        public String name;
        public Int32 category_id;
        public Int64 average_price;
        private CommodityCategory category;
        public String [] scanNames;

        public String CatName()
        {
            if (category == null)
            {
                category = GameData.Instance.CommodityCategories.Find(c => c.id == category_id);
            }
            return category.name; 
        }

        public void SetCategory(CommodityCategory c)
        {
            category = c;
        }
    }



    public class Commodity
    {
        private string name;
//        private string cat;
        private decimal buyPrice;
        private decimal sellPrice;
        private decimal supply;
        private DateTime lastUpdated;
        private bool priceCheckRequired;
        public  Int64 id=-1;
        private CommodityType type;

        public Int64 GetId()
        {
            return id;
        }
        public void SetId(Int64 x)
        {
            id=x;
            type = GameData.Instance.CommodityTypes.Find(c => c.id == id);    
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string NiceName
        {
            get
            {
                string[] names = Name.Split(' ');
                StringBuilder sb = new StringBuilder();
                foreach (string n in names)
                {
                    sb.Append(char.ToUpper(n[0]) + n.Substring(1).ToLower() + ' ');
                }
                return sb.ToString().Trim();
            }
        }

        public bool PriceCheckRequired
        {
            get { return priceCheckRequired; }
            set { priceCheckRequired = value; }
        }

        public string Cat
        {
            get
            {
                if (type == null)
                {
                    type = GameData.Instance.CommodityTypes.Find(c => c.id == id);
                }
                return type.CatName();
            }
            set {  }
        }
        public decimal BuyPrice
        {
            get { return buyPrice; }
            set { buyPrice = value; }
        }
        public decimal SellPrice
        {
            get { return sellPrice; }
            set { sellPrice = value; }
        }
        public decimal Supply
        {
            get { return supply; }
            set { supply = value; }
        }
        public DateTime LastUpdated
        {
            get { return lastUpdated; }
            set { lastUpdated = value; }
        }

        public Commodity()
        {
            priceCheckRequired = false;
        }
        public Commodity(Commodity copy)
        {
            name = copy.Name;
            //cat = copy.Cat;
            buyPrice = copy.BuyPrice;
            sellPrice = copy.SellPrice;
            supply = copy.supply;
            lastUpdated = copy.LastUpdated;
            priceCheckRequired = copy.priceCheckRequired;
        }

        public bool Equals(Commodity compareTo)
        {
            if (name != compareTo.Name || buyPrice != compareTo.BuyPrice || sellPrice != compareTo.SellPrice || supply != compareTo.Supply)
                return false;

            return true;
        }
    }
}
