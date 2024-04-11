using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ProjektLAB.TrainService.Class.ServiceClass
{
    public static class TrainServiceDataBase
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static ObservableCollection<Train> GetAllTrainFromDataBase()
        {
            ObservableCollection<Train> trains = new ObservableCollection<Train>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT IDTrain, TrainNumber, Carrier, TrainType, NumberOfSeats, CompartmentCar, OpenCar, SleepingCar FROM [dbo].[Train]";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Train train = new Train()
                            {
                                IDTrain = (int)reader["IDTrain"],
                                TrainNumber = reader["TrainNumber"].ToString() ?? "Brak informacji z bazy danych",
                                Carrier = reader["Carrier"]?.ToString() ?? "Brak informacji z bazy danych",
                                TrainType = reader["TrainType"].ToString() ?? "Brak informacji z bazy danych",
                                NumberOfSeats = (int)reader["NumberOfSeats"],
                                CompartmentCar = (bool)reader["CompartmentCar"],
                                OpenCar = (bool)reader["OpenCar"],
                                SleepingCar = (bool)reader["SleepingCar"]
                            };

                            trains.Add(train);
                        }
                    }
                }
            }

            return trains;
        }

        public static Train GetTrainInfo(string selectedTrain)
        {
            Train train = new Train();

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"SELECT [IDTrain], [TrainNumber], [TrainType], [Carrier], [NumberOfSeats], [MaxSpeed], [CompartmentCar], [OpenCar], [SleepingCar]
                 FROM [dbo].[Train]
                 WHERE [TrainNumber] = @TrainNumber";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TrainNumber", selectedTrain);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            train.IDTrain = reader.IsDBNull(reader.GetOrdinal("IDTrain")) ? 0 : reader.GetInt32(reader.GetOrdinal("IDTrain"));
                            train.TrainNumber = reader["TrainNumber"].ToString() ?? "Brak informacji z bazy danych";
                            train.Carrier = reader["Carrier"].ToString() ?? "Brak informacji z bazy danych";
                            train.TrainType = reader["TrainType"].ToString() ?? "Brak informacji z bazy danych";
                            train.CompartmentCar = reader.IsDBNull(reader.GetOrdinal("CompartmentCar")) ? false : reader.GetBoolean(reader.GetOrdinal("CompartmentCar"));
                            train.OpenCar = reader.IsDBNull(reader.GetOrdinal("OpenCar")) ? false : reader.GetBoolean(reader.GetOrdinal("OpenCar"));
                            train.SleepingCar = reader.IsDBNull(reader.GetOrdinal("SleepingCar")) ? false : reader.GetBoolean(reader.GetOrdinal("SleepingCar"));
                            train.NumberOfSeats = reader.IsDBNull(reader.GetOrdinal("NumberOfSeats")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumberOfSeats"));
                            train.MaxSpeed = reader.IsDBNull(reader.GetOrdinal("MaxSpeed")) ? 0 : reader.GetInt32(reader.GetOrdinal("MaxSpeed"));
                        }
                        reader.Close();
                    }
                }
                conn.Close();
            }

            return train;
        }
    }
}
