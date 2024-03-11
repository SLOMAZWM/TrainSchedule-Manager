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
        public List<Station> Stations { get; set; }

        public Route() 
        {
            Stations = new List<Station>();
        }
    }
}
