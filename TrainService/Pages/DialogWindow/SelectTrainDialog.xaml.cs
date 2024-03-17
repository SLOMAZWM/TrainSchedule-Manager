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

        private void AddTrainBtn_Click(object sender, RoutedEventArgs e)
        {
            Train selectedItem = (Train)TrainDataGrid.SelectedItem;
            if(selectedItem != null) 
            {
                string SelectedTrainNumber = selectedItem.TrainNumber!;
                selectedItem = TrainServiceDataBase.GetTrainInfo(SelectedTrainNumber);

                routePage.TrainNumberLbl.Content = selectedItem.TrainNumber;
                routePage.CarrierLbl.Content = selectedItem.Carrier;
                routePage.TrainTypeLbl.Content = selectedItem.TrainType;
                routePage.NumberOfSeatsLbl.Content = selectedItem.NumberOfSeats;
                routePage.MaxSpeedLbl.Content = selectedItem.MaxSpeed;
                routePage.CompartmentCarLbl.Content = selectedItem.CompartmentCar;
                routePage.OpenCarLbl.Content = selectedItem.OpenCar;
                routePage.SleepingCarLbl.Content = selectedItem.SleepingCar;
                routePage.selectedTrain = selectedItem;
                this.Close();
            }
            else
            {
                MessageBox.Show("Wybierz pociąg z podanej listy!", "Błąd wyboru", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
