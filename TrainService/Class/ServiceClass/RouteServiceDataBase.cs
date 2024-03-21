using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektLAB.TrainService.Class.ServiceClass
{
    public static class RouteServiceDataBase
    {
        public static bool SaveRouteAndStations(Route route, Train train, DateTime departureDate)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString; 

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    var routeId = AddRoute(connection, transaction, route, departureDate);

                    
                    AddStationsToRoute(connection, transaction, routeId, route.Stations);

                  
                    AddTrainSchedule(connection, transaction, train.IDTrain.Value, routeId);

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Wystąpił błąd: " + ex.Message, "Błąd krytyczny", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        private static int AddRoute(SqlConnection connection, SqlTransaction transaction, Route route, DateTime departureDate)
        {
            string sqlQuery = @"
INSERT INTO [dbo].[Route] (StartTime, EndTime, DepartureDate)
VALUES (@StartTime, @EndTime, @DepartureDate);
SELECT CAST(SCOPE_IDENTITY() as int);";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@StartTime", route.StartTime.ToString(@"hh\:mm\:ss"));
                command.Parameters.AddWithValue("@EndTime", route.EndTime.ToString(@"hh\:mm\:ss"));
                command.Parameters.AddWithValue("@DepartureDate", departureDate);

                int routeId = (int)command.ExecuteScalar();
                return routeId;
            }
        }

        private static void AddStationsToRoute(SqlConnection connection, SqlTransaction transaction, int routeId, List<Station> stations)
        {
            string insertStationQuery = @"
INSERT INTO Stations (Name, ArrivalTime, DepartureTime, PlatformNumber, TrackNumber)
VALUES (@Name, @ArrivalTime, @DepartureTime, @PlatformNumber, @TrackNumber);
SELECT CAST(SCOPE_IDENTITY() as int);";

            string insertRouteStationQuery = @"
INSERT INTO RouteStation (RouteID, StationID, SequenceNumber)
VALUES (@RouteID, @StationID, @SequenceNumber);";

            foreach (Station station in stations)
            {
                int stationId;

                using (SqlCommand insertCommand = new SqlCommand(insertStationQuery, connection, transaction))
                {
                    insertCommand.Parameters.AddWithValue("@Name", station.Name);
                    insertCommand.Parameters.AddWithValue("@ArrivalTime", station.ArrivalTime ?? (object)DBNull.Value);
                    insertCommand.Parameters.AddWithValue("@DepartureTime", station.DepartureTime ?? (object)DBNull.Value);
                    insertCommand.Parameters.AddWithValue("@PlatformNumber", station.PlatformNumber ?? (object)DBNull.Value);
                    insertCommand.Parameters.AddWithValue("@TrackNumber", station.TrackNumber ?? (object)DBNull.Value);

                    stationId = (int)insertCommand.ExecuteScalar();
                }

                using (SqlCommand insertRouteStationCommand = new SqlCommand(insertRouteStationQuery, connection, transaction))
                {
                    insertRouteStationCommand.Parameters.AddWithValue("@RouteID", routeId);
                    insertRouteStationCommand.Parameters.AddWithValue("@StationID", stationId);
                    insertRouteStationCommand.Parameters.AddWithValue("@SequenceNumber", stations.IndexOf(station) + 1);

                    insertRouteStationCommand.ExecuteNonQuery();
                }
            }
        }

        private static void AddTrainSchedule(SqlConnection connection, SqlTransaction transaction, int trainId, int routeId)
        {
            string sqlQuery = @"
INSERT INTO [dbo].[TrainSchedule] (TrainID, RouteID, DelayTime)
VALUES (@TrainID, @RouteID, @DelayTime);";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@TrainID", trainId);
                command.Parameters.AddWithValue("@RouteID", routeId);
                command.Parameters.AddWithValue("@DelayTime", DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public static bool UpdateTrainSchedule(Route route, Train train)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            bool updateSuccess = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        
                        string updateRouteQuery = @"
                    UPDATE [dbo].[Route]
                    SET StartTime = @StartTime, EndTime = @EndTime, DepartureDate = @DepartureDate
                    WHERE RouteID = @RouteID;
                ";
                        using (SqlCommand cmd = new SqlCommand(updateRouteQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@StartTime", route.StartTime);
                            cmd.Parameters.AddWithValue("@EndTime", route.EndTime);
                            cmd.Parameters.AddWithValue("@DepartureDate", route.StartDate);
                            cmd.Parameters.AddWithValue("@RouteID", route.RouteID);
                            cmd.ExecuteNonQuery();
                        }

                        
                        foreach (Station station in route.Stations)
                        {
                            string updateStationQuery = @"
                        UPDATE [dbo].[Stations]
                        SET ArrivalTime = @ArrivalTime, DepartureTime = @DepartureTime, 
                            PlatformNumber = @PlatformNumber, TrackNumber = @TrackNumber
                        WHERE StationID = @StationID;
                    ";
                            using (SqlCommand cmd = new SqlCommand(updateStationQuery, connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ArrivalTime", station.ArrivalTime ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@DepartureTime", station.DepartureTime ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@PlatformNumber", station.PlatformNumber ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@TrackNumber", station.TrackNumber ?? (object)DBNull.Value);
                                cmd.Parameters.AddWithValue("@StationID", station.StationID);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        
                        string updateTrainScheduleQuery = @"
                    UPDATE [dbo].[TrainSchedule]
                    SET TrainID = @NewTrainID
                    WHERE RouteID = @RouteID;
                ";
                        using (SqlCommand cmd = new SqlCommand(updateTrainScheduleQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@NewTrainID", train.IDTrain);
                            cmd.Parameters.AddWithValue("@RouteID", route.RouteID);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        updateSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Wystąpił błąd: + " + ex.Message, "Błąd bazy danych", MessageBoxButton.OK, MessageBoxImage.Error);
                        updateSuccess = false;
                    }
                }
            }

            return updateSuccess;
        }

        public static bool DeleteTrainSchedule(int trainScheduleId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            bool deleteSuccess = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        
                        int routeId;
                        string getRouteIdQuery = "SELECT RouteID FROM TrainSchedule WHERE IDTrainSchedule = @IDTrainSchedule";
                        using (SqlCommand cmd = new SqlCommand(getRouteIdQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IDTrainSchedule", trainScheduleId);
                            routeId = (int)cmd.ExecuteScalar();
                        }

                        
                        string deleteTrainScheduleQuery = "DELETE FROM TrainSchedule WHERE IDTrainSchedule = @IDTrainSchedule";
                        using (SqlCommand cmd = new SqlCommand(deleteTrainScheduleQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@IDTrainSchedule", trainScheduleId);
                            cmd.ExecuteNonQuery();
                        }

                        
                        string deleteRouteStationQuery = "DELETE FROM RouteStation WHERE RouteID = @RouteID";
                        using (SqlCommand cmd = new SqlCommand(deleteRouteStationQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@RouteID", routeId);
                            cmd.ExecuteNonQuery();
                        }

                        
                        string deleteRouteQuery = "DELETE FROM Route WHERE RouteID = @RouteID";
                        using (SqlCommand cmd = new SqlCommand(deleteRouteQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@RouteID", routeId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        deleteSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        transaction.Rollback();
                        deleteSuccess = false;
                    }
                }
            }

            return deleteSuccess;
        }

    }
}
