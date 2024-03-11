using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLAB.TrainService.Class
{
    public class Station
    {
        public string ? Name { get; set; }
        public Dictionary<int, List<int>> ? PlatformToTracks {  get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
