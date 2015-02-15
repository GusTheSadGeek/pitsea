using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pitsea
{
    public enum EditorMode
    {
        View, Edit
    }


    // ONLY THIS BIT GETS SAVED
    public class SaveGameData
    {
        public List<CommodityCategory> commodityCategories;
        public List<CommodityType> commodityTypes;

        public Rectangle captureRect;
        public List<StarSystem> starSystems;
       // public List<GrabCommodityDatum> commodityList;
        public decimal capital;
        public decimal cargoSlots;
        public decimal jumpDist;

        public Int64 currentStationId;
        public Int64 currentSystemId;

        public Int64 nextNewStarSystemId;

        public SaveGameData()
        {
            commodityCategories = new List<CommodityCategory>();
            commodityTypes = new List<CommodityType>();

            starSystems = new List<StarSystem>();
   //         commodityList = new List<GrabCommodityDatum>();
        }
    }

    
    public class GameData
    {

        private static GameData instance;

        private SaveGameData saveGameData;

        private List<Trade> trades;
        private List<Manifest> optimalManifests;
        private List<Manifest> userManifests;
        private List<Route> optimalRoutes;
        private List<Route> userRoutes;

//        private decimal capital;
//        private decimal cargoSlots;

        public SaveGameData SaveGameData
        {
            get { return saveGameData; }
            set { saveGameData = value; }
        }

        //public List<GrabCommodityDatum> CommodityList
        //{
        //    get { return saveGameData.commodityList; }
        //}


        public List<CommodityCategory> CommodityCategories
        {
            get { return saveGameData.commodityCategories; }
        }

        public List<CommodityType> CommodityTypes
        {
            get { return saveGameData.commodityTypes; }
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
            get { return saveGameData.capital; }
            set { saveGameData.capital = value; }
        }

        public decimal JumpDist
        {
            get { return saveGameData.jumpDist; }
            set { saveGameData.jumpDist = value; }
        }

        public decimal CargoSlots
        {
            get { return saveGameData.cargoSlots; }
            set { saveGameData.cargoSlots = value; }
        }
        public Int64 CurrentStationId
        {
            get { return saveGameData.currentStationId; }
            set { saveGameData.currentStationId = value; }
        }
        public Int64 CurrentSystemId
        {
            get { return saveGameData.currentSystemId; }
            set { saveGameData.currentSystemId = value; }
        }



        public static GameData Instance{
            get { return instance; }
        }

        public GameData()
        {
            instance = this;
            saveGameData = new SaveGameData();

            trades = new List<Trade>();
            optimalManifests = new List<Manifest>();
            userManifests = new List<Manifest>();
            optimalRoutes = new List<Route>();
            userRoutes = new List<Route>();
        }
    }
}
