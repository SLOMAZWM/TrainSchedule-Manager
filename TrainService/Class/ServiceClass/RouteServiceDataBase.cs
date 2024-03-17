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


    }
}
