using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektLAB.TrainService.Class
{
    public class TrainSchedule
    {
        public int IDTrainSchedule { get; set; }
        public Train Train { get; set; } = new Train();
        public Route Route { get; set; } = new Route();
        public string DelayTime { get; set; }

        public TrainSchedule()
            {
            DelayTime = string.Empty;
            }
    }
}
