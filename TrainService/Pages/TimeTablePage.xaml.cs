using ProjektLAB.TrainService.Class;
using ProjektLAB.TrainService.Class.ServiceClass;
using ProjektLAB.TrainService.Pages.DetailWindow;
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
        public User loggedUser {  get; set; }

        public TimeTablePage(User User)
        {
            InitializeComponent();
            InitializeTime();
            trainSchedules = new ObservableCollection<TrainSchedule>();
            LoadTrainSchedules();
            TrainScheduleDataGrid.Visibility = Visibility.Collapsed;
            SeeDetailsBtn.Visibility = Visibility.Collapsed;
            loggedUser = User;
        }

        private bool IsStationOnRoute(Route route, string stationPartialName, out int stationIndex)
        {
            stationIndex = route.Stations.FindIndex(station => station.Name!.IndexOf(stationPartialName, StringComparison.OrdinalIgnoreCase) >= 0);
            return stationIndex >= 0;
        }

        private string GetFullStationName(Route route, string stationPartialName)
        {
            var fullStationName = route.Stations.FirstOrDefault(station => station.Name!.IndexOf(stationPartialName, StringComparison.OrdinalIgnoreCase) >= 0)?.Name;
            return fullStationName ?? string.Empty;
        }


        private void SearchCriteriaBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!this.IsLoaded) return;

            var departureFrom = DepartureFromTextBox.Text.Trim();
            var arrivalTo = ArivalToTextBox.Text.Trim();
            var selectedDate = DepartureDatePicker.SelectedDate;
            var selectedHourString = DepartureHourCB.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(departureFrom) || string.IsNullOrWhiteSpace(arrivalTo))
            {
                MessageBox.Show("Należy wprowadzić miejsce wyjazdu i miejsce przyjazdu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TimeSpan? selectedTime = null;
            if (!string.IsNullOrEmpty(selectedHourString))
            {
                TimeSpan parsedTime;
                if (TimeSpan.TryParse(selectedHourString, out parsedTime))
                {
                    selectedTime = parsedTime;
                }
            }

            TimeSpan timeWindowStart = selectedTime.HasValue ? selectedTime.Value.Add(TimeSpan.FromHours(-1)) : TimeSpan.MinValue;
            TimeSpan timeWindowEnd = selectedTime.HasValue ? selectedTime.Value.Add(TimeSpan.FromHours(3)) : TimeSpan.MaxValue;

            var filteredSchedules = new ObservableCollection<TrainSchedule>(trainSchedules.Where(schedule =>
            {
                var route = schedule.Route!;
                int departureIndex, arrivalIndex;

                bool matchesDeparture = IsStationOnRoute(route, departureFrom, out departureIndex);
                bool matchesArrival = IsStationOnRoute(route, arrivalTo, out arrivalIndex);
                bool matchesDate = !selectedDate.HasValue || route.StartDate.Date == selectedDate.Value.Date;
                bool withinTimeWindow = !selectedTime.HasValue || (route.StartTime >= timeWindowStart && route.StartTime <= timeWindowEnd);

                if (!matchesDeparture || !matchesArrival || !matchesDate || !withinTimeWindow || departureIndex > arrivalIndex) return false;

                route.StartStationName = GetFullStationName(route, departureFrom);
                route.EndStationName = GetFullStationName(route, arrivalTo);

                if (departureIndex != -1)
                {
                    route.StartTime = TimeSpan.Parse(route.Stations[departureIndex].DepartureTime!);
                }
                if (arrivalIndex != -1)
                {
                    route.EndTime = TimeSpan.Parse(route.Stations[arrivalIndex].ArrivalTime!);
                }

                return true;
            }));

            SeeDetailsBtn.Visibility = Visibility.Visible;
            TrainScheduleDataGrid.ItemsSource = filteredSchedules;
            TrainScheduleDataGrid.Visibility = Visibility.Visible;
        }


        private void ClearSearchCriteriaBtn_Click(object sender, RoutedEventArgs e)
        {
            DepartureFromTextBox.Text = string.Empty;
            ArivalToTextBox.Text = string.Empty;
            DepartureDatePicker.Text = string.Empty;
            DepartureHourCB.Text = string.Empty;

            TrainScheduleDataGrid.Visibility = Visibility.Collapsed;
            SeeDetailsBtn.Visibility = Visibility.Collapsed;
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

            DepartureDatePicker.SelectedDate = DateTime.Now;
        }

        private void TrainScheduleDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (TrainScheduleDataGrid.SelectedItem is TrainSchedule selectedSchedule)
            {
                ShowDetailWindow detailWindow = new ShowDetailWindow(selectedSchedule, loggedUser);
                detailWindow.ShowDialog();
            }
        }

        private void SeeDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TrainScheduleDataGrid.SelectedItem is TrainSchedule selectedSchedule)
            {
                ShowDetailWindow detailWindow = new ShowDetailWindow(selectedSchedule, loggedUser);
                detailWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wybierz trasę do wyświetlenia jej szczegółów", "Błąd wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
