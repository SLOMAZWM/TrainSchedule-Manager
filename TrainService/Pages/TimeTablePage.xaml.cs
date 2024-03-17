using ProjektLAB.TrainService.Class;
using ProjektLAB.TrainService.Class.ServiceClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;


namespace ProjektLAB.TrainService.Pages
{
    /// <summary>
    /// Interaction logic for TimeTablePage.xaml
    /// </summary>
    public partial class TimeTablePage : Page
    {
        private ObservableCollection<TrainSchedule> trainSchedules;
        private DispatcherTimer stationSwitchTimer;
        private int currentStationIndex = 0;

        public TimeTablePage()
        {
            InitializeComponent();
            InitializeTime();
            trainSchedules = new ObservableCollection<TrainSchedule>();
            LoadTrainSchedules();
        }

        private void OnSearchCriteriaChanged(object sender, EventArgs e)
        {
            if (!this.IsLoaded) return;

            var departureFrom = DepartureFromTextBox.Text;
            var arrivalTo = ArivalToTextBox.Text;
            var selectedDate = DepartureDatePicker.SelectedDate;
            var selectedHourString = DepartureHourCB.SelectedItem?.ToString();
            var trainNumber = TrainNumberTxt.Text;

            
            TimeSpan? selectedHour = null;
            if (!string.IsNullOrEmpty(selectedHourString))
            {
                var hourParts = selectedHourString.Split(':');
                if (hourParts.Length >= 2 && int.TryParse(hourParts[0], out int hour))
                {
                    selectedHour = TimeSpan.FromHours(hour);
                }
            }

            var filteredSchedules = trainSchedules.Where(schedule =>
                (string.IsNullOrEmpty(departureFrom) || schedule.Route.DepartureStation.Contains(departureFrom, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(arrivalTo) || schedule.Route.ArrivalStation.Contains(arrivalTo, StringComparison.OrdinalIgnoreCase)) &&
                (!selectedDate.HasValue || schedule.Route.StartDate.Date == selectedDate.Value.Date) &&
                (!selectedHour.HasValue || (schedule.Route.StartTime >= selectedHour.Value && schedule.Route.StartTime < selectedHour.Value.Add(TimeSpan.FromHours(4)))) &&
                (string.IsNullOrEmpty(trainNumber) || schedule.Train.TrainNumber.Contains(trainNumber, StringComparison.OrdinalIgnoreCase))
            ).ToList();

            TrainScheduleDataGrid.ItemsSource = new ObservableCollection<TrainSchedule>(filteredSchedules);
        }


        private void LoadTrainSchedules()
        {
            trainSchedules = TimeTableServiceDataBase.GetTrainSchedules();
            TrainScheduleDataGrid.ItemsSource = trainSchedules;
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
