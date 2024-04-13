using ProjektLAB.UserClass;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjektLAB
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string BirthDay { get; set; }
        public int Age
        {
            get { return GetAgeFromBirthDay(); }
        }
        public List<UserTravelHistory> TravelHistory { get; set; }

        public User()
        {
            Login = string.Empty;
            Password = string.Empty;
            First_Name = string.Empty;
            Last_Name = string.Empty;
            BirthDay = string.Empty;
            TravelHistory = new List<UserTravelHistory>();
        }

        public int GetAgeFromBirthDay()
        {
            if (DateTime.TryParseExact(BirthDay, "dd.MM.yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
            {
                DateTime today = DateTime.Today;
                var age = today.Year - birthDate.Year;
                if (birthDate > today.AddYears(-age))
                {
                    age--;
                }   
                return age;
            }
            else
            {
                MessageBox.Show("Błąd konwertowania wieku!", "Błąd formatu!", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }
    }
}
