using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLAB.TrainService.Class
{
    public class Train
    {
        public int? IDTrain { get; set; }
        public string? TrainNumber { get; set; }
        public string? Carrier { get; set; }
        public string? TrainType { get; set; }
        public int? NumberOfSeats { get; set; }
        public bool? CompartmentCar { get; set; }
        public bool? OpenCar { get; set; } 
        public bool? SleepingCar { get; set; } 
    }
}
