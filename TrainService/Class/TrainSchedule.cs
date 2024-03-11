using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLAB.TrainService.Class
{
    public class TrainSchedule
    {
        public Train ? Train { get; set; }
        public string ? DepartureStation { get; set; }
        public string ? ArrivalStation { get; set; }
        public string ? DepartureTime { get; set; }
        public string ? ArrivalTime { get; set; }
        public string ? DepartureDate { get; set; }
        public string? DelayTime { get; set; }
    }
}
