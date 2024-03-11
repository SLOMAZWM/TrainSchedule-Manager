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
            InitializeSchedule();
        }

        private void InitializeSchedule()
        {
            var schedules = new List<TrainSchedule>
    {
        new TrainSchedule {
    Train = new Train { TrainNumber = "1234", Carrier = "PKP Intercity", TrainType = "Ekspres" },
    DepartureStation = "Łódź", ArrivalStation = "Wrocław", StartTime = "12:00", EndTime = "15:00", DepartureDate = "12.03.2024", DelayTime = "Brak" },
new TrainSchedule {
    Train = new Train { TrainNumber = "7402", Carrier = "PKP Intercity", TrainType = "Ekspres" },
    DepartureStation = "Opole", ArrivalStation = "Wrocław", StartTime = "08:09", EndTime = "10:34", DepartureDate = "12.03.2024", DelayTime = "Brak" },
new TrainSchedule {
    Train = new Train { TrainNumber = "1504", Carrier = "PKP Intercity", TrainType = "Osobowy" },
    DepartureStation = "Wrocław", ArrivalStation = "Opole", StartTime = "10:30", EndTime = "12:53", DepartureDate = "12.03.2024", DelayTime = "Brak" },
new TrainSchedule {
    Train = new Train { TrainNumber = "5742", Carrier = "PKP Intercity", TrainType = "Ekspres" },
    DepartureStation = "Opole", ArrivalStation = "Wrocław", StartTime = "10:00", EndTime = "12:38", DepartureDate = "12.03.2024", DelayTime = "15 min" },
new TrainSchedule {
    Train = new Train { TrainNumber = "9101", Carrier = "PKP Intercity", TrainType = "Osobowy" },
    DepartureStation = "Łódź", ArrivalStation = "Opole", StartTime = "10:05", EndTime = "15:52", DepartureDate = "12.03.2024", DelayTime = "Brak" },
new TrainSchedule {
    Train = new Train { TrainNumber = "6068", Carrier = "PKP Intercity", TrainType = "Pospieszny" },
    DepartureStation = "Wrocław", ArrivalStation = "Opole", StartTime = "10:09", EndTime = "13:24", DepartureDate = "12.03.2024", DelayTime = "Brak" },
new TrainSchedule {
    Train = new Train { TrainNumber = "9911", Carrier = "PKP Intercity", TrainType = "Pospieszny" },
    DepartureStation = "Opole", ArrivalStation = "Wrocław", StartTime = "09:22", EndTime = "11:00", DepartureDate = "12.03.2024", DelayTime = "Brak" },
new TrainSchedule {
    Train = new Train { TrainNumber = "5653", Carrier = "PKP Intercity", TrainType = "Osobowy" },
    DepartureStation = "Łódź", ArrivalStation = "Opole", StartTime = "10:14", EndTime = "14:59", DepartureDate = "12.03.2024", DelayTime = "Brak" },
new TrainSchedule {
    Train = new Train { TrainNumber = "5919", Carrier = "PKP Intercity", TrainType = "Pospieszny" },
    DepartureStation = "Opole", ArrivalStation = "Wrocław", StartTime = "14:00", EndTime = "15:20", DepartureDate = "12.03.2024", DelayTime = "Brak" },
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
