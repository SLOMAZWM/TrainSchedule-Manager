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

namespace ProjektLAB.TrainService.Pages
{
    /// <summary>
    /// Interaction logic for StationsPage.xaml
    /// </summary>
    public partial class StationsPage : Page
    {
        private List<Station> stations = new List<Station>();

        public StationsPage()
        {
            InitializeComponent();
            stations = StationServiceDataBase.InitializeMainStationsFromDataBase();
            InitializeStationsToSelect();
        }

        private void InitializeStationsToSelect()
        {
            foreach(Station station in stations) 
            { 
                SelectStationCB.Items.Add(station.Name);
            }
        }
    }
}
