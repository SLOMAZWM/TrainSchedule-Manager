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
using ProjektLAB.TrainService.Class;

namespace ProjektLAB.TrainService.Pages.DialogWindow
{
    /// <summary>
    /// Interaction logic for RouteAddPage.xaml
    /// </summary>
    public partial class RouteAddPage : Page
    {
        public Route newRoute { get; set; }
        private string startStation;
        private string endStation;
        public RouteAddPage(Route newR, string start, string end)
        {
            InitializeComponent();
            newRoute = newR;
            startStation = start;
            endStation = end;
            InitializeData();
        }

        private void InitializeData()
        {
            StartStationLbl.Content = startStation;
            EndStationLbl.Content = endStation;
            RouteListBox.ItemsSource = newRoute.Stations;
            AmountOfStationsLbl.Content = RouteListBox.Items.Count;
        }

        private void LoadTrainBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectTrainDialog selectTrain = new SelectTrainDialog(this);
            selectTrain.ShowDialog();
        }

        private void RouteListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void SeeDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            if(StartTimeTextBox.Text == "Liczba" || StartTimeTextBox.Text == string.Empty && EndTimeTextBox.Text == "Liczba" || EndTimeTextBox.Text == string.Empty)
            {
                MessageBox.Show("Wypełnij datę rozpoczęcia, lub zakończenia kursu!", "Błąd wypełnienia", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                TimeSpan StartTime = TimeSpan.Parse(StartTimeTextBox.Text);
                TimeSpan EndTime = TimeSpan.Parse(EndTimeTextBox.Text);
                DetailStationDialog detailStationDialog = new DetailStationDialog(newRoute, StartTime, EndTime);
                detailStationDialog.OnStationsUpdated = (updatedStations) =>
                {
                    newRoute.Stations = updatedStations;
                    RouteListBox.ItemsSource = newRoute.Stations;
                };
                detailStationDialog.ShowDialog();
            }
        }
    }
}
