using ProjektLAB.TrainService.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektLAB.UserClass
{
    public class UserTravelHistory
    {
        public int UserTravelHistoryID { get; set; }
        public User TravelUser { get; set; }
        public TrainSchedule Schedule { get; set; }
        public string Status { get; set; }

        public UserTravelHistory()
        {
            UserTravelHistoryID = 0;
            TravelUser = new User();
            Schedule = new TrainSchedule();
            Status = "Nieznany";
        }

        public void InitializeStatus()
        {
            DateTime ScheduleDate = Schedule.Route.StartDate;
            DateTime Today = DateTime.Now;

            if(ScheduleDate > Today)
            {
                Status = "Zaplanowana podróż";
            }
            else if(ScheduleDate < Today)
            {
                Status = "Odbyta podróż";
            }
            else if(ScheduleDate == Today)
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
