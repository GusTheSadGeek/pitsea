using System;

namespace Pitsea
{

    public class Commodity
    {
        private string name;
        private string cat;
        private decimal buyPrice;
        private decimal sellPrice;
        private decimal supply;
        private DateTime lastUpdated;
        private bool priceCheckRequired;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public bool PriceCheckRequired
        {
            get { return priceCheckRequired; }
            set { priceCheckRequired = value; }
        }

        public string Cat
        {
            get { return cat; }
            set { cat = value; }
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
            cat = copy.Cat;
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
