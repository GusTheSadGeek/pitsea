using System;
using System.Collections.Generic;

namespace Pitsea
{
    public class Station
    {
        private Int64 id;
        private string name;
        private Int64 systemId;
        private bool hasBlackMarket;
        private String maxLandingPadSize;
        private Int64 distanceToStar;
        private List<Commodity> commodities;


        public Int64 Id
        {
            get { return id; }
            set { id = value; }
        }

        public Int64 SystemId
        {
            get { return systemId; }
            set { systemId = value; }
        }
        public bool HasBlackMarket
        {
            get { return hasBlackMarket; }
            set { hasBlackMarket = value; }
        }
        public String MaxLandingPadSize
        {
            get { return maxLandingPadSize; }
            set { maxLandingPadSize = value; }
        }
        public Int64 DistanceToStar
        {
            get { return distanceToStar; }
            set { distanceToStar = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<Commodity> Commodities
        {
            get { return commodities; }
            set { commodities = value; }
        }

        public List<Commodity> CommoditiesSorted()
        {
            commodities.Sort (delegate(Commodity c1, Commodity c2) { return c1.id.CompareTo(c2.id);} );
            return commodities;
        }

        public Station()
        {
            commodities = new List<Commodity>();
        }
        public Station(Station copy)
        {
            commodities = new List<Commodity>();

            name = copy.Name;

            foreach (Commodity commodity in copy.Commodities)
                commodities.Add(new Commodity(commodity));
        }

    }
}
