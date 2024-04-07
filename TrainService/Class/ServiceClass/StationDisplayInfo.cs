using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLAB.TrainService.Class.ServiceClass
{
    public class StationDisplayInfo
    {
        public string ? Name { get; set; }
        public string ? ArrivalTime { get; set; }
        public string ? DepartureTime { get; set; }
        public string ? DelayTime { get; set; } 
        public int ? PlatformNumber { get; set; }
        public int ? TrackNumber { get; set; }
        public string ? TrainNumber { get; set; }
        public bool IsStartStation { get; set; }
        public bool IsEndStation { get; set; }
        public bool IsIntermediateStation { get; set; }
    }
}
