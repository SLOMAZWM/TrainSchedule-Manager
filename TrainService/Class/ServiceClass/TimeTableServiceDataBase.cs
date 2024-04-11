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

            var routesDict = new Dictionary<int, Route>();
            var trainSchedulesDict = new Dictionary<int, TrainSchedule>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT
    ts.IDTrainSchedule,
    ts.DelayTime,
    t.IDTrain,
    t.TrainNumber,
    t.Carrier,
    t.TrainType,
    t.NumberOfSeats,
    t.CompartmentCar,
    t.OpenCar,
    t.SleepingCar,
    t.MaxSpeed,
    r.RouteID,
    r.StartTime,
    r.EndTime,
    r.DepartureDate,
    s.StationID,
    s.Name AS StationName,
    s.ArrivalTime,
    s.DepartureTime,
    s.PlatformNumber,
    s.TrackNumber,
    rs.SequenceNumber
FROM
    TrainSchedule ts
INNER JOIN
    Train t ON ts.TrainID = t.IDTrain
INNER JOIN
    Route r ON ts.RouteID = r.RouteID
INNER JOIN
    RouteStation rs ON r.RouteID = rs.RouteID
INNER JOIN
    Stations s ON rs.StationID = s.StationID
ORDER BY
    ts.IDTrainSchedule, rs.SequenceNumber;";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int routeID = (int)reader["RouteID"];
                        int trainScheduleID = (int)reader["IDTrainSchedule"];

                        if (!trainSchedulesDict.TryGetValue(trainScheduleID, out var trainSchedule))
                        {
                            trainSchedule = new TrainSchedule
                            {
                                IDTrainSchedule = trainScheduleID,
                                DelayTime = reader["DelayTime"]?.ToString() ?? "Brak informacji z bazy danych",
                                Train = new Train
                                {
                                    IDTrain = (int)reader["IDTrain"],
                                    TrainNumber = reader["TrainNumber"].ToString() ?? "Brak informacji z bazy danych",
                                    Carrier = reader["Carrier"].ToString() ?? "Brak informacji z bazy danych",
                                    TrainType = reader["TrainType"].ToString() ?? "Brak informacji z bazy danych",
                                    NumberOfSeats = (int)reader["NumberOfSeats"],
                                    CompartmentCar = (bool)reader["CompartmentCar"],
                                    OpenCar = (bool)reader["OpenCar"],
                                    SleepingCar = (bool)reader["SleepingCar"],
                                    MaxSpeed = (int)reader["MaxSpeed"]
                                },
                                Route = new Route
                                {
                                    RouteID = routeID,
                                    StartTime = (TimeSpan)reader["StartTime"],
                                    EndTime = (TimeSpan)reader["EndTime"],
                                    StartDate = (DateTime)reader["DepartureDate"],
                                    Stations = new List<Station>()
                                }
                            };

                            trainSchedulesDict.Add(trainScheduleID, trainSchedule);
                            schedules.Add(trainSchedule);
                        }

                        if (!routesDict.TryGetValue(routeID, out var route))
                        {
                            route = trainSchedule.Route;
                            routesDict.Add(routeID, route!);
                        }

                        var station = new Station
                        {
                            StationID = (int)reader["StationID"],
                            Name = reader["StationName"]?.ToString() ?? "Brak informacji z bazy danych",
                            ArrivalTime = reader["ArrivalTime"]?.ToString() ?? "Brak informacji z bazy danych",
                            DepartureTime = reader["DepartureTime"]?.ToString() ?? "Brak informacji z bazy danych",
                            PlatformNumber = (int)reader["PlatformNumber"],
                            TrackNumber = (int)reader["TrackNumber"]
                        };

                        route!.Stations.Add(station);
                    }
                }
            }

            foreach (var schedule in schedules)
            {
                var stations = schedule.Route!.Stations.OrderBy(s => s.StationID).ToList();
                schedule.Route.StartStationName = stations.FirstOrDefault().Name ?? "Brak informacji z bazy danych";
                schedule.Route.EndStationName = stations.LastOrDefault().Name ?? "Brak informacji z bazy danych";
            }

            return schedules;
        }

       

    }


}
