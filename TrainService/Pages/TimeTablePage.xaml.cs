using System;
using System.Collections.Generic;
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

namespace ProjektLAB.TrainService.Pages
{
    /// <summary>
    /// Interaction logic for TimeTablePage.xaml
    /// </summary>
    public partial class TimeTablePage : Page
    {
        public TimeTablePage()
        {
            InitializeComponent();
            InitializeStations();
            InitializeTime();
        }

        private void InitializeStations()
        {
            DepartureFromCB.Items.Add("Opole");
            DepartureFromCB.Items.Add("Wrocław");
            DepartureFromCB.Items.Add("Łódź");

            ArrivalToCB.Items.Add("Opole");
            ArrivalToCB.Items.Add("Wrocław");
            ArrivalToCB.Items.Add("Łódź");
        }

        private void InitializePassengers()
        {

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
