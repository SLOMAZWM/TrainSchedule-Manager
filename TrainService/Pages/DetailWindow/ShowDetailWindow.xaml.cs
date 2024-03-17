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
        private TrainSchedule selectedSchedule = new TrainSchedule();
        public ShowDetailWindow(TrainSchedule selectedSch)
        {
            InitializeComponent();
            selectedSchedule = selectedSch;
        }

        private void InitializeData()
        {

        }

        private void Border_MouseDown(object sender, MouseEventArgs e)
        {
            this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
