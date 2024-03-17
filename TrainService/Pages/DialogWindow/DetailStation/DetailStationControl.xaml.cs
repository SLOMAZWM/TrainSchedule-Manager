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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Collections.Specialized.BitVector32;

namespace ProjektLAB.TrainService.Pages.DialogWindow.DetailStation
{
    /// <summary>
    /// Interaction logic for DetailStationControl.xaml
    /// </summary>
    public partial class DetailStationControl : UserControl
    {
        private Station _station;
        public DetailStationControl(Station station)
        {
            InitializeComponent();
            _station = station;
            this.DataContext = station;
        }

        public void InitializeTextBoxes(Station station)
        {
            _station = station;
            ArrivalTimeTextBox.Text = station.ArrivalTime ?? "";
            DepartureTimeTextBox.Text = station.DepartureTime ?? "";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            _station.ArrivalTime = ArrivalTimeTextBox.Text;
            _station.DepartureTime = DepartureTimeTextBox.Text;
            MessageBox.Show("Zapisano zmiany.");
        }
    }
}
