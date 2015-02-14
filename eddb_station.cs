using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

using System.IO;
using System.Net;
//Xml.Serialization;


namespace Pitsea
{
    class eddb_commodity_category
    {
        public Int32  id;
        public String name;
    }

    class eddb_commodity_type
    {
        public Int32 id;
        public String name;
        public Int32 category_id;
        private Int64 _average_price;
        public eddb_commodity_category category = new eddb_commodity_category();

        public String average_price
        {
            get {return _average_price.ToString();}
            set {
                if (value == null)
                {
                    _average_price = 0;
                }
                else
                {
                    _average_price = Int64.Parse(value);
                }
            }
        }

        public Int64 GetAveragePrice()
        {
            return _average_price;
        }

    }


    class eddb_commodity
    {
        public Int64 id;
        public Int64 station_id;
        public Int32 commodity_id;
        public Int64 supply;
        public Int64 buy_price;
        public Int64 sell_price;
        public Int64 demand;
        public Int64 collected_at;
    }

    class eddb_station
    {
        public Int64 id;
        public String name;
        public Int64 system_id;
        private Int32 _hasblackmarket;
        public String max_landing_pad_size;
        private Int64 _distance_to_star;
        public List<eddb_commodity> listings;

        public String has_blackmarket
        {
            get { return _hasblackmarket.ToString(); }
            set
            {
                if (value != null)
                {
                    _hasblackmarket = Int32.Parse(value);
                }
                else
                {
                    _hasblackmarket = 0;
                }
            }
        }
        public String distance_to_star
        {
            get { return _distance_to_star.ToString(); }
            set
            {
                if (value != null)
                {
                    _distance_to_star = Int64.Parse(value);
                }
                else
                {
                    _distance_to_star = 0;
                }
            }
        }

        public bool hasBlackMarket()
        {
            return (_hasblackmarket > 0);
        }

        public Int64 distanceToStar()
        {
            return _distance_to_star;
        }

    }

    class eddb_stations
    {
        public List<eddb_station> stations;

        public eddb_stations()
        {
        }
    }


    class eddb_system
    {
        public Int64 id;
        public String name;
        public double x;
        public double y;
        public double z;
        public List<eddb_station> stations;// = new List<eddb_station>();
    }

    class eddb_systems
    {
        public List<eddb_system> systems;
        public List<eddb_station> stations;
        public List<eddb_commodity_type> commoditie_types;

        public eddb_systems()
        {
        }

        public void save_json(string filepath)
        {
            filepath = @"d:\systems2.json";
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = 2147483647;
            var json = serializer.Serialize(systems);

            StreamWriter fileStream = new StreamWriter(filepath);
            fileStream.Write(json);
            fileStream.Close();
        }

        public void load_json()
        {
            using (var client = new WebClient())
            {
                InfoBoxA.Instance.Message("Downloading 'commodities.json'");
                string data = client.DownloadString("http://eddb.io/archive/commodities.json");
                InfoBoxA.Instance.Message("Deserializing 'commodities.json'");
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = 2147483647;
                commoditie_types = serializer.Deserialize(data, typeof(List<eddb_commodity_type>)) as List<eddb_commodity_type>;
            }


            using (var client = new WebClient())
            {
                InfoBoxA.Instance.Message("Downloading 'systems.json'");
                string data = client.DownloadString("http://eddb.io/archive/systems.json");
                InfoBoxA.Instance.Message("Deserializing 'systems.json'");
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = 2147483647;
                systems = serializer.Deserialize(data, typeof(List<eddb_system>)) as List<eddb_system>;
            }

            using (var client = new WebClient())
            {
                InfoBoxA.Instance.Message("Downloading 'stations.json'");
                string data = client.DownloadString("http://eddb.io/archive/stations.json");
                InfoBoxA.Instance.Message("Deserializing 'stations.json'");
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = 2147483647;
                stations = serializer.Deserialize(data, typeof(List<eddb_station>)) as List<eddb_station>;
            }
        }

        public DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        public void import_from_json()
        {
            InfoBoxA.Instance.Message("Importing data from json");

            List<CommodityType> commodity_types = GameData.Instance.SaveGameData.commodityTypes;
            List<CommodityCategory> commodity_cats = GameData.Instance.SaveGameData.commodityCategories;

            InfoBoxA.Instance.Message("Importing commodity info from json");
            foreach (eddb_commodity_type commodity_type in commoditie_types)
            {
                CommodityType ct = commodity_types.Find(x => x.id == commodity_type.id);
                if (ct == null)
                {
                    ct = new CommodityType();
                    ct.id = commodity_type.id;
                    commodity_types.Add(ct);
                    ct.name = commodity_type.name;
                    ct.average_price = commodity_type.GetAveragePrice();
                    ct.category_id = commodity_type.category_id;

                    CommodityCategory cc = commodity_cats.Find(x => x.id == commodity_type.category.id);
                    if (cc == null)
                    {
                        cc = new CommodityCategory();
                        cc.id = commodity_type.category.id;
                        cc.name = commodity_type.category.name;
                        commodity_cats.Add(cc);
                    }
                    ct.SetCategory(cc);
                }
            }

            InfoBoxA.Instance.Message("Importing system and station info from json");
            var starSystems = GameData.Instance.SaveGameData.starSystems;
            foreach (eddb_system ed_system in systems)
            {
                StarSystem ss = starSystems.Find(s => s.Id == ed_system.id);
                if (ss == null)
                {
                    ss = starSystems.Find(s => s.Name == ed_system.name);
                    if (ss != null)
                    {
                        InfoBoxA.Instance.Message(ed_system.name + " Updating id");
                    }
                }
                if (ss == null)
                {
                    ss = new StarSystem();
                    ss.Name = ed_system.name;
                    starSystems.Add(ss);
                    InfoBoxA.Instance.Message(ed_system.name + " New system");
                }
                ss.Id = ed_system.id;
                ss.Coordinate = new Coordinate(ed_system.x, ed_system.y, ed_system.z); 
                ss.isAnarchy = false;

                foreach (eddb_station ed_station in ed_system.stations)
                {
                    Station stat = ss.Stations.Find(X => X.Id == ed_station.id);
                    if (stat == null)
                    {
                        stat = new Station();
                        stat.Name = ed_station.name;
                        stat.Id = ed_station.id;
                        ss.Stations.Add(stat);
                    }
                    stat.HasBlackMarket = ed_station.hasBlackMarket();
                    stat.MaxLandingPadSize = ed_station.max_landing_pad_size;
                    if (stat.DistanceToStar == 0)
                    {
                        stat.DistanceToStar = ed_station.distanceToStar();
                    }
                    stat.SystemId = ed_station.system_id;
    
                    eddb_station ed_station2 = stations.Find(x => x.id == ed_station.id);
                    if ((ed_station2!=null) && (ed_station2.listings != null) )
                    {
                        long newest = 0;
                        foreach (eddb_commodity ed_comm in ed_station2.listings)
                        {
                            if (ed_comm.collected_at > newest)
                                newest = ed_comm.collected_at;
                        }
                        foreach (eddb_commodity ed_comm in ed_station2.listings)
                        {
                            if (ed_comm.collected_at == newest)
                            {
                                eddb_commodity_type comm_type = commoditie_types.Find(x => x.id == ed_comm.commodity_id);
                                //String n = comm_type.name;
                                //switch (comm_type.name)
                                //{
                                //    case "H.E. Suits": n = "HE SUITS"; break;
                                //    case "Non-lethal Weapons": n = "NONLETHAL WEAPONS"; break;
                                //   // case "Liquor": n = "LIQUOR"; break;
                                //    case "Auto-Fabricators": n = "AUTOFABRICATORS"; break;
                                //    case "Agri-Medicines": n = "AGRI MEDICINES"; break;
                                //}


                                Commodity commodity = stat.Commodities.Find(x => x.id == ed_comm.commodity_id);
                                if (commodity == null)
                                {
                                    commodity = new Commodity();
                                    commodity.Name = comm_type.name;
                                    stat.Commodities.Add(commodity);
                                    commodity.LastUpdated = FromUnixTime(ed_comm.collected_at);
                                    //                                Console.WriteLine(comm_type.name + "   " + ed_system.name+"    "+stat.Name);
                                }
                                if (commodity.LastUpdated <= FromUnixTime(ed_comm.collected_at))
                                {
                                    commodity.SetId(ed_comm.commodity_id);
                                    commodity.BuyPrice = ed_comm.buy_price;
                                    commodity.SellPrice = ed_comm.sell_price;
                                    if (ed_comm.demand > 0)
                                        commodity.Supply = ed_comm.demand;
                                    if (ed_comm.supply > 0)
                                        commodity.Supply = ed_comm.supply;
                                    commodity.LastUpdated = FromUnixTime(ed_comm.collected_at);
                                }
                            }
                            else
                            {

                            }
                        }
                    }
                }
            }
        }
    }
}
