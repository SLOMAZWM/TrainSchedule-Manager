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
            using(SqlConnection conn = new SqlConnection(connectionString)) 
            {
                conn.Open();
                string query = "SELECT IDTrain, TrainNumber, Carrier, TrainType, NumberOfSeats, CompartmentCar, OpenCar, SleepingCar FROM [dbo].[Train]";
                using(SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using(SqlDataReader reader = cmd.ExecuteReader()) 
                    {
                        while(reader.Read()) 
                        {
                            Train train = new Train()
                            {
                                IDTrain = reader["IDTrain"] as int?,
                                TrainNumber = reader["TrainNumber"]?.ToString(),
                                Carrier = reader["Carrier"]?.ToString(),
                                TrainType = reader["TrainType"]?.ToString(),
                                NumberOfSeats = reader["NumberOfSeats"] as int?,
                                CompartmentCar = reader["CompartmentCar"] as bool?,
                                OpenCar = reader["OpenCar"] as bool?,
                                SleepingCar = reader["SleepingCar"] as bool?
                            };

                            trains.Add(train);
                        }
                    }
                }
            }

            return trains;
        }
    }
}
