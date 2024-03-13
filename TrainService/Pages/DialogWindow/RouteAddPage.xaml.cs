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

        public void InitializeData()
        {
            StartStationLbl.Content = startStation;
            EndStationLbl.Content = endStation;
            newRoute = newRoute.FromOpoleToWroclaw();
            RouteListBox.ItemsSource = newRoute.Stations;
        }
    }
}
