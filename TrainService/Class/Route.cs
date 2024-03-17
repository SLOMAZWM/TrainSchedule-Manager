using ProjektLAB.TrainService.Class.ServiceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLAB.TrainService.Class
{
    public class Route
    {
        public int RouteID { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime StartDate { get; set; }
        public List<Station> Stations { get; set; } = new List<Station>();

        public Route()
        {
            Stations = StationServiceDataBase.InitializeAllStationsFromDataBase();
        }

        public Route FromWroclawToOpole()
        {
            Route fromWroclawToOpole = new Route();
            List<Station> stationFromWroclawToOpole = new List<Station>();

            Dictionary<string, int> stationOrder = new Dictionary<string, int>
            {
        { "WROCŁAW GŁÓWNY", 1 },
        { "OŁAWA", 2 },
        { "BRZEG", 3 },
        { "OPOLE GŁÓWNE", 4 }
            };

            foreach (Station station in Stations)
            {
                if (station.Name != null && stationOrder.ContainsKey(station.Name))
                {
                    stationFromWroclawToOpole.Add(station);
                }
            }

            stationFromWroclawToOpole = stationFromWroclawToOpole
                .Where(s => s.Name != null)
                .OrderBy(s => stationOrder[s.Name!])
                .ToList();
            fromWroclawToOpole.Stations = stationFromWroclawToOpole;

            return fromWroclawToOpole;
        }


        public Route FromOpoleToWroclaw()
        {
            Route fromOpoleToWroclaw = new Route();
            List<Station> stationFromOpoleToWroclaw = new List<Station>();

            Dictionary<string, int> stationOrder = new Dictionary<string, int>
             {
        { "OPOLE GŁÓWNE", 1 },
        { "BRZEG", 2 },
        { "OŁAWA", 3 },
        { "WROCŁAW GŁÓWNY", 4 }
            };

            foreach (Station station in Stations)
            {
                if (station.Name != null && stationOrder.ContainsKey(station.Name))
                {
                    stationFromOpoleToWroclaw.Add(station);
                }
            }

            stationFromOpoleToWroclaw = stationFromOpoleToWroclaw
                .Where(s => s.Name != null)
                .OrderBy(s => stationOrder[s.Name!])
                .ToList();
            fromOpoleToWroclaw.Stations = stationFromOpoleToWroclaw;

            return fromOpoleToWroclaw;
        }


        public Route FromOpoleToLodz()
        {
            Route fromOpoleToLodz = new Route();
            List<Station> stationFromOpoleToLodz = new List<Station>();

            Dictionary<string, int> stationOrder = new Dictionary<string, int>
    {
        { "OPOLE GŁÓWNE", 1 },
        { "OZIMEK", 2 },
        { "LUBLINIEC", 3 },
        { "CZĘSTOCHOWA STRADOM", 4 },
        { "CZĘSTOCHOWA", 5 },
        { "KOLUSZKI", 6 },
        { "ŁÓDŹ WIDZEW", 7 }
    };

            foreach (Station station in Stations)
            {
                if (station.Name != null && stationOrder.ContainsKey(station.Name))
                {
                    stationFromOpoleToLodz.Add(station);
                }
            }

            stationFromOpoleToLodz = stationFromOpoleToLodz
                .Where(s => s.Name != null)
                .OrderBy(s => stationOrder[s.Name!])
                .ToList();
            fromOpoleToLodz.Stations = stationFromOpoleToLodz;

            return fromOpoleToLodz;
        }

        public Route FromLodzToOpole()
        {
            Route fromLodzToOpole = new Route();
            List<Station> stationFromLodzToOpole = new List<Station>();

            Dictionary<string, int> stationOrder = new Dictionary<string, int>
    {
        { "ŁÓDŹ WIDZEW", 1 },
        { "KOLUSZKI", 2 },
        { "CZĘSTOCHOWA", 3 },
        { "CZĘSTOCHOWA STRADOM", 4 },
        { "LUBLINIEC", 5 },
        { "OZIMEK", 6 },
        { "OPOLE GŁÓWNE", 7 }
    };

            foreach (Station station in Stations)
            {
                if (station.Name != null && stationOrder.ContainsKey(station.Name))
                {
                    stationFromLodzToOpole.Add(station);
                }
            }

            stationFromLodzToOpole = stationFromLodzToOpole
                .Where(s => s.Name != null)
                .OrderBy(s => stationOrder[s.Name!])
                .ToList();
            fromLodzToOpole.Stations = stationFromLodzToOpole;

            return fromLodzToOpole;
        }

        public Route FromWroclawToLodzWidzew()
        {
            Route fromWroclawToLodzWidzew = new Route();
            List<Station> stationsFromWroclawToLodzWidzew = new List<Station>();

            Dictionary<string, int> stationOrder = new Dictionary<string, int>
{
    { "WROCŁAW GŁÓWNY", 1 },
    { "WROCŁAW MIKOŁAJÓW", 2 },
    { "WROCŁAW NADODRZE", 3 },
    { "OLEŚNICA RATAJE", 4 },
    { "MILICZ", 5 },
    { "KROTOSZYN", 6 },
    { "OSTRÓW WIELKOPOLSKI", 7 },
    { "KALISZ", 8 },
    { "SIERADZ", 9 },
    { "ZDUŃSKA WOLA", 10 },
    { "ŁASK", 11 },
    { "PABIANICE", 12 },
    { "ŁÓDŹ RETKINIA", 13 },
    { "ŁÓDŹ CHOJNY", 14 },
    { "ŁÓDŹ WIDZEW", 15 }
};


            foreach (Station station in Stations)
            {
                if (station.Name != null && stationOrder.ContainsKey(station.Name))
                {
                    stationsFromWroclawToLodzWidzew.Add(station);
                }
            }

            stationsFromWroclawToLodzWidzew = stationsFromWroclawToLodzWidzew
                .Where(s => s.Name != null)
                .OrderBy(s => stationOrder[s.Name!])
                .ToList();
            fromWroclawToLodzWidzew.Stations = stationsFromWroclawToLodzWidzew;

            return fromWroclawToLodzWidzew;
        }


        public Route FromLodzWidzewToWroclawGlowny()
        {
            Route fromLodzWidzewToWroclawGlowny = new Route();
            List<Station> stationsFromLodzWidzewToWroclawGlowny = new List<Station>();

            Dictionary<string, int> stationOrder = new Dictionary<string, int>
    {
        { "ŁÓDŹ WIDZEW", 1 },
        { "ŁÓDŹ CHOJNY", 2 },
        { "ŁÓDŹ RETKINIA", 3 },
        { "PABIANICE", 4 },
        { "ŁASK", 5 },
        { "ZDUŃSKA WOLA", 6 },
        { "SIERADZ", 7 },
        { "KALISZ", 8 },
        { "OSTRÓW WIELKOPOLSKI", 9 },
        { "KROTOSZYN", 10 },
        { "MILICZ", 11 },
        { "OLEŚNICA RATAJE", 12 },
        { "WROCŁAW NADODRZE", 13 },
        { "WROCŁAW MIKOŁAJÓW", 14 },
        { "WROCŁAW GŁÓWNY", 15 }
    };

            foreach (Station station in Stations)
            {
                if (station.Name != null && stationOrder.ContainsKey(station.Name))
                {
                    stationsFromLodzWidzewToWroclawGlowny.Add(station);
                }
            }

            stationsFromLodzWidzewToWroclawGlowny = stationsFromLodzWidzewToWroclawGlowny
                .Where(s => s.Name != null)
                .OrderBy(s => stationOrder[s.Name!])
                .ToList();
            fromLodzWidzewToWroclawGlowny.Stations = stationsFromLodzWidzewToWroclawGlowny;

            return fromLodzWidzewToWroclawGlowny;
        }

    }
}
