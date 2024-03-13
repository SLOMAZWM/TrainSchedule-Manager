using ProjektLAB.TrainService.Class;
using ProjektLAB.TrainService.Class.ServiceClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ProjektLAB.TrainService.Pages.DialogWindow
{
    /// <summary>
    /// Interaction logic for SelectTrainDialog.xaml
    /// </summary>
    public partial class SelectTrainDialog : Window
    {
        private RouteAddPage routePage;
        public ObservableCollection<Train> CollectionOfTrains { get; set; }
        public SelectTrainDialog(RouteAddPage routeP)
        {
            InitializeComponent();
            routePage = routeP;

            CollectionOfTrains = new ObservableCollection<Train>();
            CollectionOfTrains = TrainServiceDataBase.GetAllTrainFromDataBase();
            TrainDataGrid.ItemsSource = CollectionOfTrains;
        }

        private void MouseDown_Click(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e) 
        {
            this.Close();
        }
    }
}
