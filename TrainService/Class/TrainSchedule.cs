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
        public int IDTrainSchedule {  get; set; }
        public Train ? Train { get; set; }
        public Route ? Route { get; set; }
        public string? DelayTime { get; set; }
    }
}
