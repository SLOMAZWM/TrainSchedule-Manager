using ProjektLAB.TrainService.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektLAB.TrainService.Pages
{
    /// <summary>
    /// Interaction logic for TimeTablePage.xaml
    /// </summary>
    public partial class TimeTablePage : Page
    {
        public TimeTablePage()
        {
            InitializeComponent();
            InitializePage();
        }

        private void InitializeStations()
        {
            DepartureFromCB.Items.Add("Opole");
            DepartureFromCB.Items.Add("Wrocław");
            DepartureFromCB.Items.Add("Łódź");

            ArrivalToCB.Items.Add("Opole");
            ArrivalToCB.Items.Add("Wrocław");
            ArrivalToCB.Items.Add("Łódź");
        }

        private void InitializePage()
        {
            InitializeStations();
            InitializeTime();
            InitializePassengers();
            InitializeSchedule();
        }

        private void InitializePassengers()
        {

        }

        private void InitializeSchedule()
        {
            var schedules = new List<TrainSchedule>
    {
        new TrainSchedule {
            Train = new Train { TrainNumber = "1234", Carrier = "PKP Intercity", TrainType = "Ekspres" },
            DepartureStation = "Łódź", ArrivalStation = "Wrocław", DepartureTime = "12:00", ArrivalTime = "10:00", DepartureDate = "12.03.2024", DelayTime = "Brak" },
    };

            TrainScheduleDataGrid.ItemsSource = schedules;
        }


        private void InitializeTime()
        {
            for (int i = 1; i <=24; i++)
            {
                string time = i.ToString() + ":00";
                DepartureHourCB.Items.Add(time);
            }
        }

    }
}
