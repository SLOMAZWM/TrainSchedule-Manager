using ProjektLAB.TrainService.Class;
using ProjektLAB.TrainService.Class.ServiceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjektLAB.TrainService.Pages.DialogWindow;
using System.Collections.ObjectModel;

namespace ProjektLAB.TrainService.Pages
{
    /// <summary>
    /// Interaction logic for StationsPage.xaml
    /// </summary>
    public partial class StationsPage : Page
    {
        private TrainServiceWindow trainService;
        private List<Station> stations = new List<Station>();
        private ObservableCollection<TrainSchedule> trainSchedules;

        public StationsPage(TrainServiceWindow trainS)
        {
            InitializeComponent();
            stations = StationServiceDataBase.InitializeMainStationsFromDataBase();
            InitializeStationsToSelect();
            trainService = trainS;
            trainSchedules = new ObservableCollection<TrainSchedule>();
            trainSchedules = TimeTableServiceDataBase.GetTrainSchedules();
            TrainDataGrid.ItemsSource = trainSchedules;
        }

        private void InitializeStationsToSelect()
        {
            foreach(Station station in stations) 
            { 
                SelectStationCB.Items.Add(station.Name);
            }
        }

        private void AddRideBtn_Click(object sender, RoutedEventArgs e)
        {
            WhichStationsDialog user_Choice = new WhichStationsDialog(trainService);
            user_Choice.ShowDialog();
        }

        private void SelectStationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(SelectStationCB.SelectedIndex)
            {
                case 0:
                    {
                        FilteredText("OPOLE GŁÓWNE");
                        break;
                    }
                case 1:
                    {
                        FilteredText("WROCŁAW GŁÓWNY");
                        break;
                    }
                case 2:
                    {
                        FilteredText("ŁÓDŹ WIDZEW");
                        break;
                    }
            }
        }

        private void FilteredText(string nameStation)
        {
            var filteredSchedules = trainSchedules.Where(schedule =>
                (schedule.Route!.StartStationName!.Contains(nameStation, StringComparison.OrdinalIgnoreCase)) ||
                (schedule.Route!.EndStationName!.Contains(nameStation, StringComparison.OrdinalIgnoreCase))).ToList();

            TrainDataGrid.ItemsSource = new ObservableCollection<TrainSchedule>(filteredSchedules);
        }

        private void EditRideBtn_Click(object sender, RoutedEventArgs e)
        {
            TrainSchedule selectedItem = (TrainSchedule)TrainDataGrid.SelectedItem;

            if (selectedItem != null) 
            {
                string startStationSelected = selectedItem.Route?.StartStationName!;
                string endStationSelected = selectedItem.Route?.EndStationName!;

                bool isEdit = true;
                Train editedTrain = new Train();
                editedTrain = selectedItem.Train!;

                RouteAddPage editedRoute = new RouteAddPage(selectedItem.Route!, startStationSelected, endStationSelected, isEdit, editedTrain);
                trainService.ContentFrame.Navigate(editedRoute);
            }
            else
            {
                MessageBox.Show("Zaznacz trasę do edycji!", "Błąd wyboru", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
