using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.ObjectModel;

namespace ProjektLAB.TrainService.Class.ServiceClass
{
    public static class TimeTableServiceDataBase
    {
        public static ObservableCollection<TrainSchedule> GetTrainSchedules()
        {
            ObservableCollection<TrainSchedule> schedules = new ObservableCollection<TrainSchedule>();
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                SELECT
                    ts.IDTrainSchedule,
                    ts.DelayTime,
                    t.IDTrain,
                    t.TrainNumber,
t.TrainType,
                    t.Carrier,
                    r.RouteID,
                    r.StartTime,
                    r.EndTime,
                    r.DepartureDate
                FROM
                    TrainSchedule ts
                JOIN
                    Train t ON ts.TrainID = t.IDTrain
                JOIN
                    Route r ON ts.RouteID = r.RouteID";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var schedule = new TrainSchedule
                        {
                            IDTrainSchedule = int.Parse(reader["IDTrainSchedule"].ToString()),
                            DelayTime = reader["DelayTime"] as string,
                            Train = new Train
                            {
                                IDTrain = int.Parse(reader["IDTrain"].ToString()),
                                TrainNumber = reader["TrainNumber"] as string,
                                Carrier = reader["Carrier"] as string,
                                TrainType = reader["TrainType"] as string,
                            },
                            Route = new Route
                            {
                                RouteID = int.Parse(reader["RouteID"].ToString()),
                                StartTime = TimeSpan.Parse(reader["StartTime"].ToString()),
                                EndTime = TimeSpan.Parse(reader["EndTime"].ToString()),
                                StartDate = DateTime.Parse(reader["DepartureDate"].ToString())
                            }
                        };

                        schedules.Add(schedule);
                    }
                }
            }

            return schedules;
        }
    }
}
