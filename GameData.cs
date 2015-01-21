using System.Collections.Generic;

namespace Pitsea
{
    public enum EditorMode
    {
        View, Edit
    }


    // ONLY THIS BIT GETS SAVED
    public class SaveGameData
    {
        public List<StarSystem> starSystems;
        public List<GrabCommodityDatum> commodityList;

        public SaveGameData()
        {
            starSystems = new List<StarSystem>();
            commodityList = new List<GrabCommodityDatum>();
        }
    }

    
    public class GameData
    {
        private SaveGameData saveGameData;

        private List<Trade> trades;
        private List<Manifest> optimalManifests;
        private List<Manifest> userManifests;
        private List<Route> optimalRoutes;
        private List<Route> userRoutes;

        private decimal capital;
        private decimal cargoSlots;

        public SaveGameData SaveGameData
        {
            get { return saveGameData; }
            set { saveGameData = value; }
        }

        public List<GrabCommodityDatum> CommodityList
        {
            get { return saveGameData.commodityList; }
        }

        public List<StarSystem> StarSystems
        {
            get { return saveGameData.starSystems; }
        }
        public List<Trade> Trades
        {
            get { return trades; }
        }
        public List<Manifest> OptimalManifests
        {
            get { return optimalManifests; }
        }
        public List<Manifest> UserManifests
        {
            get { return userManifests; }
        }
        public List<Route> OptimalRoutes
        {
            get { return optimalRoutes; }
        }
        public List<Route> UserRoutes
        {
            get { return userRoutes; }
        }

        public decimal Capital
        {
            get { return capital; }
            set { capital = value; }
        }
        public decimal CargoSlots
        {
            get { return cargoSlots; }
            set { cargoSlots = value; }
        }

        public GameData()
        {
            saveGameData = new SaveGameData();

            trades = new List<Trade>();
            optimalManifests = new List<Manifest>();
            userManifests = new List<Manifest>();
            optimalRoutes = new List<Route>();
            userRoutes = new List<Route>();
        }
    }
}
