using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektLAB.TrainService.Class
{
    public class Station
    {
        public int StationID { get; set; }
        public string? Name { get; set; }
        public string? ArrivalTime { get; set; }
        public string? DepartureTime { get; set; }
        public int? PlatformNumber { get; set; }
        public int? TrackNumber { get; set; }
        public int? SelectedPlatform { get; set; }
        public int? SelectedTrack {  get; set; }

        public void AssignData()
        {
            PlatformNumber = SelectedPlatform;
            TrackNumber = SelectedTrack;
        }

        
    }
}
