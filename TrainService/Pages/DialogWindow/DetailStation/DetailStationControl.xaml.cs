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

namespace ProjektLAB.TrainService.Pages.DialogWindow.DetailStation
{
    /// <summary>
    /// Interaction logic for DetailStationControl.xaml
    /// </summary>
    public partial class DetailStationControl : UserControl
    {
        public DetailStationControl(Station station)
        {
            InitializeComponent();
            this.DataContext = station;
        }
    }
}
