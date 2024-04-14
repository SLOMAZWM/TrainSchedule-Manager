using Microsoft.Data.SqlClient;
using ProjektLAB.TrainService.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
#nullable disable

namespace ProjektLAB.UserClass
{
    public static class UserServiceDataBase
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static void AddNewUserToDataBase(User user)
        {
            if (user == null)
            {
                MessageBox.Show("Błąd przekazania użytkownika do bazy danych!", "Błąd krytyczny bazy danych", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO [dbo].[User] (Login, Password, First_Name, Last_Name, BirthDay, Admin_Permission) VALUES (@Login, @Password, @First_Name, @Last_Name, @BirthDay, @Admin_Permission)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Login", user.Login);
                        cmd.Parameters.AddWithValue("@Password", HashingHelper.HashPassword(user.Password));
                        cmd.Parameters.AddWithValue("@First_Name", user.First_Name);
                        cmd.Parameters.AddWithValue("@Last_Name", user.Last_Name);
                        cmd.Parameters.AddWithValue("@BirthDay", user.BirthDay);
                        cmd.Parameters.AddWithValue("@Admin_Permission", user.IsAdmin);

                        try
                        {
                            conn.Open();
                            int result = cmd.ExecuteNonQuery();

                            if (result > 0)
                            {
                                MessageBox.Show("Zostałeś pomyślnie zarejestrowany - nastąpi automatyczne przekierowanie do aplikacji po kliknięciu OK", "Pomyślna Rejestracja!", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                MessageBox.Show("Nie udało się dodać użytkownika do bazy danych.", "Błąd rejestracji", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        catch (SqlException ex)
                        {
                            MessageBox.Show($"Wystąpił błąd podczas dodawania użytkownika do bazy danych: {ex.Message}", "Błąd krytyczny bazy danych", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Wystąpił niespodziewany błąd podczas dodawania użytkownika do bazy danych: {ex.Message}", "Błąd krytyczny bazy danych", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }

        public static User TryLoginUser(string login, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT Id, Login, Password, First_Name, Last_Name, BirthDay, Admin_Permission FROM [dbo].[User] WHERE Login = @Login";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Login", login);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string hashedPassword = HashingHelper.HashPassword(password);
                                string storedHash = reader["Password"].ToString() ?? "Brak odczytu z bazy danych!";
                                if (hashedPassword.Equals(storedHash))
                                {
                                    User user = new User()
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Login = reader["Login"].ToString() ?? "Brak odczytu z bazy danych!",
                                        Password = reader["Password"].ToString() ?? "Brak odczytu z bazy danych!",
                                        First_Name = reader["First_Name"].ToString() ?? "Brak odczytu z bazy danych!",
                                        Last_Name = reader["Last_Name"].ToString() ?? "Brak odczytu z bazy danych!",
                                        BirthDay = reader["BirthDay"].ToString() ?? "Brak odczytu z bazy danych!",
                                        IsAdmin = (bool)reader["Admin_Permission"]
                                    };
                                    return user;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Wystąpił błąd podczas próby logowania: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    return new User();
                }
            }
        }

        public static bool SaveUserRoute(int userId, int trainScheduleId, string startStation, string endStation)
        {
            if (!UserExists(userId) || !TrainScheduleExists(trainScheduleId))
            {
                MessageBox.Show("Nie znaleziono użytkownika lub rozkładu jazdy.", "Błąd zapisu", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"INSERT INTO [dbo].[UserTravelHistory] 
                                                 (UserID, TrainScheduleID, StartStation, EndStation) 
                                                 VALUES (@UserID, @TrainScheduleID, @StartStation, @EndStation)";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@TrainScheduleID", trainScheduleId);
                    cmd.Parameters.AddWithValue("@StartStation", startStation);
                    cmd.Parameters.AddWithValue("@EndStation", endStation);

                    try
                    {
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return true;
                        }
                        else
                        {
                            MessageBox.Show("Nie udało się zapisać trasy.", "Błąd zapisu", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Wystąpił błąd podczas zapisywania trasy: {ex.Message}", "Błąd bazy danych", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            return false;
        }


        private static bool UserExists(int userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) FROM [dbo].[User] WHERE Id = @Id";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", userId);
                    int userCount = (int)cmd.ExecuteScalar();
                    return userCount > 0;
                }
            }
        }

        private static bool TrainScheduleExists(int trainScheduleId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT COUNT(*) FROM [dbo].[TrainSchedule] WHERE IDTrainSchedule = @IDTrainSchedule";
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IDTrainSchedule", trainScheduleId);
                    int scheduleCount = (int)cmd.ExecuteScalar();
                    return scheduleCount > 0;
                }
            }
        }

        public static List<UserTravelHistory> GetTravelHistoriesByUserId(int userId)
        {
            List<UserTravelHistory> travelHistories = new List<UserTravelHistory>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
SELECT 
    th.UserTravelHistoryID, 
    th.TrainScheduleID,
    ts.RouteID, 
    r.DepartureDate, 
    t.TrainNumber, 
    t.Carrier,
    th.StartStation,
    th.EndStation
FROM 
    UserTravelHistory th
    INNER JOIN TrainSchedule ts ON th.TrainScheduleID = ts.IDTrainSchedule
    INNER JOIN Route r ON ts.RouteID = r.RouteID
    INNER JOIN Train t ON ts.TrainID = t.IDTrain
WHERE 
    th.UserID = @UserID
ORDER BY 
    r.DepartureDate DESC;";


                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var history = new UserTravelHistory
                            {
                                UserTravelHistoryID = reader.GetInt32(reader.GetOrdinal("UserTravelHistoryID")),
                                Schedule = new TrainSchedule
                                {
                                    IDTrainSchedule = reader.GetInt32(reader.GetOrdinal("TrainScheduleID")),
                                    Route = new Route
                                    {
                                        RouteID = reader.GetInt32(reader.GetOrdinal("RouteID")),
                                        StartDate = reader.GetDateTime(reader.GetOrdinal("DepartureDate")),
                                    },
                                    Train = new Train
                                    {
                                        TrainNumber = reader.GetString(reader.GetOrdinal("TrainNumber")),
                                        Carrier = reader.IsDBNull(reader.GetOrdinal("Carrier")) ? null : reader.GetString(reader.GetOrdinal("Carrier")),
                                    }
                                },
                                StartStation = reader.GetString(reader.GetOrdinal("StartStation")),
                                EndStation = reader.GetString(reader.GetOrdinal("EndStation")),
                            };
                            history.InitializeStatus();
                            travelHistories.Add(history);
                        }
                    }
                }
            }
            return travelHistories;
        }




    }
}
