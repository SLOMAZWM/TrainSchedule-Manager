using ProjektLAB.TrainService.Class;
using System;

namespace ProjektLAB.UserClass
{
    public class UserTravelHistory
    {
        public int UserTravelHistoryID { get; set; }
        public User TravelUser { get; set; }
        public TrainSchedule Schedule { get; set; }
        public string Status { get; set; }
        public string StartStation { get; set; } 
        public string EndStation { get; set; } 

        public UserTravelHistory()
        {
            UserTravelHistoryID = 0;
            TravelUser = new User();
            Schedule = new TrainSchedule();
            Status = "Nieznany";
            StartStation = string.Empty;
            EndStation = string.Empty;
        }

        public void InitializeStatus()
        {
            DateTime scheduleDate = Schedule.Route.StartDate;
            DateTime today = DateTime.Now;

            if (scheduleDate > today)
            {
                Status = "Zaplanowana podróż";
            }
            else if (scheduleDate < today)
            {
                Status = "Odbyta podróż";
            }
            else if (scheduleDate == today)
            {
                Status = "Dzisiejsza podróż";
            }
            else
            {
                Status = "Błąd odczytu";
            }
        }
    }
}
