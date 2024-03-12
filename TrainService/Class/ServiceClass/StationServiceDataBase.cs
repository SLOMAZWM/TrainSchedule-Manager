using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ProjektLAB.TrainService.Class.ServiceClass
{
    public static class StationServiceDataBase
    {

        public static List<Station> InitializeMainStationsFromDataBase()
        {
            List<Station> mainStations = new List<Station>();
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Name FROM Stations WHERE StationID IN (2, 11, 5);";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            mainStations.Add(new Station
                            {
                                Name = reader["Name"].ToString(),
                                //PlatformToTracks = 
                            });
                        }
                    }
                }
            }

            return mainStations;
        }
        public static List<Station> InitializeAllStationsFromDataBase()
        {
            List<Station> stations = new List<Station>();

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Name, Latitude, Longitude FROM Stations;";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stations.Add(new Station
                            {
                                Name = reader["Name"].ToString(),
                            });
                        }
                    }
                }
            }

            return stations;
        }

    }
}
