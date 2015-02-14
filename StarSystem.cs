using System;
using System.Collections.Generic;

namespace Pitsea
{
    public class Coordinate
    {
        public double x;
        public double y;
        public double z;

        public Coordinate()
        {
            x = y = z = 0.0;
        }

        public Coordinate(double X, double Y, double Z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public Coordinate(Coordinate coord)
        {
            x = coord.x;
            y = coord.y;
            z = coord.z;
        }
    }

    public class NearbyStar
    {
        public StarSystem starSystem;
        public double distance;
    }

    public class StarSystem
    {
        private Int64 id;
        private string name;
        private List<Station> stations;
        public bool isAnarchy;

        private Coordinate coordinate;

        // Not saved
        private List<NearbyStar> nearbyStars;
        private int jumps;

        public Int64 Id
        {
            get { return id; }
            set { id = value; }
        }

        public Coordinate Coordinate
        {
            get { return coordinate; }
            set { coordinate = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public List<Station> Stations
        {
            get { return stations; }
        }
        public bool IsAnarchy
        {
            get { return isAnarchy; }
            set { isAnarchy = value; }
        }

        public StarSystem()
        {
            stations = new List<Station>();
            nearbyStars = new List<NearbyStar>();
        }

        public bool addNearbyStar(StarSystem ss, double distance)
        {
            bool ret = false;
            NearbyStar nearby = nearbyStars.Find(s => s.starSystem.Name == ss.Name);
            if (nearby == null)
            {
                ret = true;
                nearby = new NearbyStar();
                nearby.starSystem = ss;
                nearbyStars.Add(nearby);
                nearby.distance = distance;
            }
            return ret;
        }

        public bool isNearby(StarSystem ss, double max)
        {
            bool ret = false;
            if (ss == null) return false;
            if (ss == this) return false;
            double dx = Math.Abs(ss.Coordinate.x - coordinate.x);
            if (dx > max) return false;
            double dy = Math.Abs(ss.Coordinate.y - coordinate.y);
            if (dy > max) return false;
            double dz = Math.Abs(ss.Coordinate.z - coordinate.z);
            if (dz > max) return false;

            double distSquared = dx * dx + dy * dy + dz * dz;
            if (distSquared < (max * max))
            {
                double distance = Math.Sqrt(distSquared);
                ss.addNearbyStar(this, distance);
                addNearbyStar(ss, distance);
                ret = true;
            }
            return ret;
        }

        public bool isWithinRange(StarSystem ss, double max)
        {
            bool ret = false;
            if (ss == null) return false;
            if (ss == this) return true;
            double dx = Math.Abs(ss.Coordinate.x - coordinate.x);
            if (dx > max) return false;
            double dy = Math.Abs(ss.Coordinate.y - coordinate.y);
            if (dy > max) return false;
            double dz = Math.Abs(ss.Coordinate.z - coordinate.z);
            if (dz > max) return false;

            double distSquared = dx * dx + dy * dy + dz * dz;
            if (distSquared < (max * max))
            {
                ret = true;
            }
            return ret;
        }

        public int Jumps(int n=-99)
        {
            if (n != -99) jumps = n;
            return jumps;
        }


        public List<NearbyStar> NearbyStars()
        {
            return nearbyStars;
        }

        public StarSystem(StarSystem copy)
        {
            stations = new List<Station>();

            name = copy.Name;

            foreach (Station station in copy.Stations)
                stations.Add(new Station(station));
        }
    }
}
