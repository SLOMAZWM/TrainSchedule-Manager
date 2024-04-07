using ProjektLAB.TrainService.Class;
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
using System.Windows.Shapes;

namespace ProjektLAB.TrainService.Pages.DetailWindow
{
    /// <summary>
    /// Interaction logic for ShowDetailWindow.xaml
    /// </summary>
    public partial class ShowDetailWindow : Window
    {
        private TrainSchedule selectedSchedule;
        public ShowDetailWindow(TrainSchedule selectedSch)
        {
            InitializeComponent();
            selectedSchedule = selectedSch;
            InitializeData();
        }

        private void InitializeData()
        {
            FirstStationNameTextBlock.Text = selectedSchedule.Route!.Stations[0].Name ?? "N/A";
            LastStationNameTextBlock.Text = selectedSchedule.Route.Stations[selectedSchedule.Route.Stations.Count - 1].Name ?? "N/A";
            StartTimeTextBlock.Text = selectedSchedule.Route.Stations[0].ArrivalTime ?? "N/A";
            EndTimeTextBlock.Text = selectedSchedule.Route.Stations[selectedSchedule.Route.Stations.Count - 1].DepartureTime!.ToString() ?? "N/A";

            ShowRouteDetailPage RouteDetail = new ShowRouteDetailPage(selectedSchedule);
            ContentDetailFrame.Navigate(RouteDetail);
        }

        private void Border_MouseDown(object sender, MouseEventArgs e)
        {
            this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ShowTrainDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDetailFrame.Navigate(new ShowTrainDetailsPage(selectedSchedule, this));
        }
    }
}
