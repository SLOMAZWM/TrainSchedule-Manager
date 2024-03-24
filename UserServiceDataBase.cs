using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektLAB
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
                    string query = @"INSERT INTO [dbo].[User] (Login, Password, First_Name, Last_Name, BirthDay) VALUES (@Login, @Password, @First_Name, @Last_Name, @BirthDay)";

                    using(SqlCommand cmd = new SqlCommand(query, conn)) 
                    {
                        cmd.Parameters.AddWithValue("@Login", user.Login);
                        cmd.Parameters.AddWithValue("@Password", HashingHelper.HashPassword(user.Password)); 
                        cmd.Parameters.AddWithValue("@First_Name", user.First_Name);
                        cmd.Parameters.AddWithValue("@Last_Name", user.Last_Name);
                        cmd.Parameters.AddWithValue("@BirthDay", user.BirthDay);

                        try
                        {
                            conn.Open();
                            int result = cmd.ExecuteNonQuery();

                            if(result > 0) 
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
                        catch(Exception ex) 
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
                string query = @"SELECT Id, Login, Password, First_Name, Last_Name, BirthDay FROM [dbo].[User] WHERE Login = @Login";

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
                                string storedHash = reader["Password"].ToString();
                                if (hashedPassword.Equals(storedHash))
                                {
                                    User user = new User()
                                    {
                                        Id = Convert.ToInt32(reader["Id"]),
                                        Login = reader["Login"].ToString(),
                                        Password = reader["Password"].ToString(),
                                        First_Name = reader["First_Name"].ToString(),
                                        Last_Name = reader["Last_Name"].ToString(),
                                        BirthDay = reader["BirthDay"].ToString()
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
                }
            }
            // Authentication failed.
            return null;
        }

    }
}
