using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLAB.TrainService.Class
{
    public class Station
    {
        public string ? Name { get; set; }
        public Dictionary<int, List<int>> ? PlatformToTracks {  get; set; }
    }
}
