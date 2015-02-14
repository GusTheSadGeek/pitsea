using System.Collections.Generic;
using System.Linq;


using System;


namespace Pitsea
{
    public static class Adviser
    {
        private static List<StarSystem> ss;

        static Adviser()
        {
        }

        public static void CalculateAll(GameData gameData, StarSystem homeSystem)
        {
            UpdateDistance(gameData, homeSystem);

//            ss = gameData.StarSystems.FindAll(s => s.Stations.Count > 0);
            ss = gameData.StarSystems.FindAll(s => s.NearbyStars().Count > 0);
            CalculateAllTrades(gameData);
            CalculateOptimalManifests(gameData);

//            ss = gameData.StarSystems.FindAll(s => s.NearbyStars().Count > 0);
            CalculateOptimalRoutes(gameData);
        }

        private static void UpdateDistance(GameData gameData, StarSystem homeSystem)
        {
            int jump = 0;
            foreach (StarSystem ss in gameData.SaveGameData.starSystems)
            {
                ss.NearbyStars().Clear();
                ss.Jumps(99999);
            }
            homeSystem.Jumps(0);

            List<StarSystem> neighbours = new List<StarSystem>();
            findNeighbours(gameData, homeSystem, neighbours);
            while (jump < 10)
            {
                ++jump;
                foreach (StarSystem ss in neighbours)
                {
                    Console.WriteLine(ss.Name + " ## " + jump.ToString());
                    ss.Jumps(jump);
                }
                List<StarSystem> neighbours2 = new List<StarSystem>();
                foreach (StarSystem ss in neighbours)
                {
                    findNeighbours(gameData, ss, neighbours2);
                }
                Console.WriteLine(neighbours2.Count);

                neighbours = neighbours2;
            }

//            List<StarSystem> neighbours = new List<StarSystem>();
//            if (jump < 10)
//            {
//                foreach (StarSystem ss2 in gameData.SaveGameData.starSystems)
//                {
//                    if (homeSystem.isNearby(ss2, (double)(gameData.JumpDist)))
//                    {
//                        Console.WriteLine(homeSystem.Name+" : "+ss2.Name + "  " + jump.ToString());
////                        UpdateDistance(gameData, ss2, jump + 1);
//                        neighbours.Add(ss2);
//                        if ( ss2.Jumps() > (jump + 1))
//                        {
//                            ss2.Jumps(jump + 1);
//                        }
//                    }
//                }
//            }
//            foreach (StarSystem ss3 in neighbours)
//            {
//                UpdateDistance(gameData, ss3, jump + 1);
//            }
        }

        private static List<StarSystem> findNeighbours(GameData gameData, StarSystem homeSystem, List<StarSystem> neighbours)
        {
            foreach (StarSystem ss in gameData.SaveGameData.starSystems)
            {
                if (homeSystem.isNearby(ss, (double)(gameData.JumpDist)))
                {
                    if (ss.Jumps() > 1000)
                    {
                        if (neighbours.Find(x => x.Id == ss.Id) == null)
                        {
                            neighbours.Add(ss);
                        }
                        else
                        {
                            Console.WriteLine(ss.Name + " @@ ");
                        }
                    }
                }
            }
            return neighbours;
        }

        private static void CalculateAllTrades(GameData gameData)
        {
            gameData.Trades.Clear();

            for (int x = 0; x < ss.Count - 1; x++)
                for (int y = x + 1; y < ss.Count; y++)
                {
                    StarSystem system1 = ss[x];
                    StarSystem system2 = ss[y];

                    foreach (Station station1 in system1.Stations)
                        foreach (Station station2 in system2.Stations)
                            foreach (Commodity commodity1 in station1.Commodities)
                                foreach (Commodity commodity2 in station2.Commodities)
                                    if (commodity1.Name == commodity2.Name)
                                    {
                                        if (commodity1.BuyPrice > 0 && commodity1.Supply > 0 && commodity1.BuyPrice < commodity2.SellPrice)
                                        {
                                            Trade newTrade = new Trade();
                                            newTrade.Commodity.Name = commodity1.Name;
                                            newTrade.Commodity.BuyPrice = commodity1.BuyPrice;
                                            newTrade.Commodity.Supply = commodity1.Supply;
                                            newTrade.Commodity.SellPrice = commodity2.SellPrice;
                                            newTrade.Commodity.LastUpdated = commodity1.LastUpdated <= commodity2.LastUpdated ? commodity1.LastUpdated : commodity2.LastUpdated;
                                            newTrade.StartSystem = system1;
                                            newTrade.EndSystem = system2;
                                            newTrade.StartStation = station1;
                                            newTrade.EndStation = station2;
                                            gameData.Trades.Add(newTrade);
                                        }

                                        if (commodity2.BuyPrice > 0 && commodity2.Supply > 0 && commodity2.BuyPrice < commodity1.SellPrice)
                                        {
                                            Trade newTrade = new Trade();
                                            newTrade.Commodity.Name = commodity1.Name;
                                            newTrade.Commodity.BuyPrice = commodity2.BuyPrice;
                                            newTrade.Commodity.Supply = commodity2.Supply;
                                            newTrade.Commodity.SellPrice = commodity1.SellPrice;
                                            newTrade.Commodity.LastUpdated = commodity1.LastUpdated <= commodity2.LastUpdated ? commodity1.LastUpdated : commodity2.LastUpdated;
                                            newTrade.StartSystem = system2;
                                            newTrade.EndSystem = system1;
                                            newTrade.StartStation = station2;
                                            newTrade.EndStation = station1;
                                            gameData.Trades.Add(newTrade);
                                        }
                                    }
                }
        }

        private static void CalculateOptimalManifests(GameData gameData)
        {
            gameData.OptimalManifests.Clear();

            foreach(Trade trade in gameData.Trades)
                if (TradePartOfExistingManifest(gameData, trade) == false)
                {
                    Manifest newManifest = new Manifest();
                    newManifest.Capital = gameData.Capital;
                    newManifest.CargoSlots = gameData.CargoSlots;
                    newManifest.AddTrade(trade);
                    gameData.OptimalManifests.Add(newManifest);
                }

            for (int x = 0; x < gameData.OptimalManifests.Count; x++)
            {
                Manifest manifest = gameData.OptimalManifests[x];

                manifest.OptimizeManifest();

                if (manifest.Profit == 0 || manifest.Trades.Count == 0)
                {
                    gameData.OptimalManifests.RemoveAt(x);
                    x--;
                }
            }
        }
        private static bool TradePartOfExistingManifest(GameData gameData, Trade trade)
        {
            foreach (Manifest manifest in gameData.OptimalManifests)
                if (manifest.Trades[0].StartSystem.Name == trade.StartSystem.Name &&
                    manifest.Trades[0].EndSystem.Name == trade.EndSystem.Name &&
                    manifest.Trades[0].StartStation.Name == trade.StartStation.Name &&
                    manifest.Trades[0].EndStation.Name == trade.EndStation.Name)
                {
                    manifest.AddTrade(trade);
                    return true;
                }

            return false;
        }

        private static void CalculateOptimalRoutes(GameData gameData)
        {
            gameData.OptimalRoutes.Clear();

            List<StarSystem> ss2 = ss.FindAll(x => x.Id >= 0);
            foreach (StarSystem startSystem in ss)
            {
                ss2.Remove(startSystem);
                foreach (Station startStation in startSystem.Stations)
                {
                    Route candidateRoute = null;

                    //Find best first out and return pair.
                    foreach (StarSystem endSystem in ss2)
                        foreach (Station endStation in endSystem.Stations)
                        {
                            if (startSystem.Equals(endSystem) || startStation.Equals(endStation))
                                continue;

                            Manifest outManifest = FindManifest(gameData, startSystem, startStation, endSystem, endStation);
                            Manifest returnManifest = FindManifest(gameData, endSystem, endStation, startSystem, startStation);

                            if (outManifest == null || returnManifest == null)
                                continue;
                            else
                            {
                                if (candidateRoute == null || candidateRoute.AverageProfitPerTrip < (outManifest.Profit + returnManifest.Profit) / 2.0M)
                                {
                                    candidateRoute = new Route();
                                    candidateRoute.Manifests.Add(outManifest);
                                    candidateRoute.Manifests.Add(returnManifest);
                                }
                            }
                        }

                    if (candidateRoute != null)
                    {
                        //Find a new out and return pair. Compare to the candidate to see if the route gets better.
                        bool foundNewPair;

                        do
                        {
                            foundNewPair = false;

                            Route compareTo = new Route(candidateRoute);
                            compareTo.Manifests.RemoveAt(compareTo.Manifests.Count - 1);
                            Manifest lastCompareManifest = compareTo.Manifests[compareTo.Manifests.Count - 1];

                            Manifest candidateStart = null;
                            Manifest candidateReturn = null;

                            foreach (StarSystem newSystem in ss)
                                foreach (Station newStation in newSystem.Stations)
                                {
                                    bool isUsed = false;

                                    foreach (Manifest usedManifest in compareTo.Manifests)
                                        if (usedManifest.StartSystemName == newSystem.Name && usedManifest.StartStationName == newStation.Name)
                                            isUsed = true;

                                    if (isUsed)
                                        continue;

                                    Manifest newStart = FindManifest(gameData, lastCompareManifest.EndSystem, lastCompareManifest.EndStation, newSystem, newStation);
                                    Manifest newReturn = FindManifest(gameData, newSystem, newStation, compareTo.CenterSystem, compareTo.CenterStation);

                                    if (newStart == null || newReturn == null)
                                        continue;
                                    else
                                    {
                                        if ((candidateStart == null && candidateReturn == null) ||
                                            candidateStart.Profit + candidateReturn.Profit < newStart.Profit + newReturn.Profit)
                                        {
                                            candidateStart = newStart;
                                            candidateReturn = newReturn;
                                        }
                                    }
                                }

                            if (candidateStart != null && candidateReturn != null)
                            {
                                compareTo.Manifests.Add(candidateStart);
                                compareTo.Manifests.Add(candidateReturn);

                                if (compareTo.AverageProfitPerTrip > candidateRoute.AverageProfitPerTrip)
                                {
                                    candidateRoute = compareTo;
                                    foundNewPair = true;
                                }
                            }
                        }
                        while (foundNewPair);

                        if (candidateRoute != null)
                            gameData.OptimalRoutes.Add(candidateRoute);
                    }
                }
            }
        }

        private static Manifest FindManifest(GameData gameData, StarSystem startSystem, Station startStation, StarSystem endSystem, Station endStation)
        {
            List<Manifest> allManifests = gameData.OptimalManifests.Concat(gameData.UserManifests).ToList();

            foreach (Manifest manifest in allManifests)
            {
                if (manifest.StartSystemName == startSystem.Name && manifest.StartStationName == startStation.Name &&
                    manifest.EndSystemName == endSystem.Name && manifest.EndStationName == endStation.Name)
                    return manifest;
            }

            return null;
        }
        private static void AddManifestToLists(Manifest candidate, List<Manifest> candidates, List<Manifest> used)
        {
            bool isUsed = false;

            for (int x = 0; x < used.Count; x++)
                if (used[x].Equals(candidate))
                    isUsed = true;

            if (isUsed)
                used.Add(candidate);
            else
                candidates.Add(candidate);
        }
    }
}