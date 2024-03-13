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

namespace ProjektLAB.TrainService.Pages
{
    /// <summary>
    /// Interaction logic for StationsPage.xaml
    /// </summary>
    public partial class StationsPage : Page
    {
        private TrainServiceWindow trainService;
        private List<Station> stations = new List<Station>();

        public StationsPage(TrainServiceWindow trainS)
        {
            InitializeComponent();
            stations = StationServiceDataBase.InitializeMainStationsFromDataBase();
            InitializeStationsToSelect();
            trainService = trainS;
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
    }
}
