using System;
using System.Collections.Generic;
using System.Linq;
using ProjektLAB.TrainService.Class;
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
    /// Interaction logic for WhichStationsDialog.xaml
    /// </summary>
    public partial class WhichStationsDialog : Window
    {
        private TrainServiceWindow trainWindow;
        public WhichStationsDialog(TrainServiceWindow trainW)
        {
            InitializeComponent();
            InitializeComboBox();
            trainWindow = trainW;
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MouseDown_Click(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void InitializeComboBox()
        {
            StartStationCB.Items.Add("OPOLE GŁÓWNE");
            StartStationCB.Items.Add("WROCŁAW GŁÓWNY");
            StartStationCB.Items.Add("ŁÓDŹ WIDZEW");

            EndStationCB.Items.Add("OPOLE GŁÓWNE");
            EndStationCB.Items.Add("WROCŁAW GŁÓWNY");
            EndStationCB.Items.Add("ŁÓDŹ WIDZEW");
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if(StartStationCB != null && EndStationCB != null) 
            {
                byte stationNumberIndexStart = Convert.ToByte(StartStationCB.SelectedIndex);
                byte stationNumberIndexEnd = Convert.ToByte(EndStationCB.SelectedIndex);
                switch(stationNumberIndexStart)
                {
                    case 0: // OPOLE GŁÓWNE
                        {
                            switch(stationNumberIndexEnd)
                            {
                                case 0:
                                    {
                                        MessageBox.Show($"Nie można dodać trasy z {StartStationCB.SelectedItem} do {EndStationCB.SelectedItem}", "Błąd wyboru", MessageBoxButton.OK, MessageBoxImage.Error);
                                        break;
                                    }
                                case 1:
                                    {
                                        string StartStation = StartStationCB.SelectedItem.ToString()!;
                                        string EndStation = EndStationCB.SelectedItem.ToString()!;
                                        Route fromOpoleToWroclaw = new Route();
                                        fromOpoleToWroclaw = fromOpoleToWroclaw.FromOpoleToWroclaw();
                                        trainWindow.ContentFrame.NavigationService.Navigate(new RouteAddPage(fromOpoleToWroclaw, StartStation, EndStation));
                                        this.Close();
                                        break;
                                    }
                                case 2:
                                    {
                                        string StartStation = StartStationCB.SelectedItem.ToString()!;
                                        string EndStation = EndStationCB.SelectedItem.ToString()!;
                                        Route fromOpoleToLodz = new Route();
                                        fromOpoleToLodz = fromOpoleToLodz.FromOpoleToLodz();
                                        trainWindow.ContentFrame.NavigationService.Navigate(new RouteAddPage(fromOpoleToLodz, StartStation, EndStation));
                                        this.Close();
                                        break;
                                    }
                            }
                            break;
                        }
                    case 1: //WROCŁAW GŁÓWNY
                        {
                            switch(stationNumberIndexEnd)
                            {
                                case 0:
                                    {
                                        string StartStation = StartStationCB.SelectedItem.ToString()!;
                                        string EndStation = EndStationCB.SelectedItem.ToString()!;
                                        Route fromWroclawToOpole = new Route();
                                        fromWroclawToOpole = fromWroclawToOpole.FromWroclawToOpole();
                                        trainWindow.ContentFrame.NavigationService.Navigate(new RouteAddPage(fromWroclawToOpole, StartStation, EndStation));
                                        this.Close();
                                        break;
                                    }
                                case 1:
                                    {
                                        MessageBox.Show($"Nie można dodać trasy z {StartStationCB.SelectedItem} do {EndStationCB.SelectedItem}", "Błąd wyboru", MessageBoxButton.OK, MessageBoxImage.Error);
                                        break;
                                    }
                                case 2:
                                    {
                                        string StartStation = StartStationCB.SelectedItem.ToString()!;
                                        string EndStation = EndStationCB.SelectedItem.ToString()!;
                                        Route fromWroclawToLodz = new Route();
                                        fromWroclawToLodz = fromWroclawToLodz.FromWroclawToLodzWidzew();
                                        trainWindow.ContentFrame.NavigationService.Navigate(new RouteAddPage(fromWroclawToLodz, StartStation, EndStation));
                                        this.Close();
                                        break;
                                    }
                            }
                            break;
                        }
                    case 2: //ŁÓDŹ
                        {
                            switch(stationNumberIndexEnd)
                            {
                                case 0:
                                    {
                                        string StartStation = StartStationCB.SelectedItem.ToString()!;
                                        string EndStation = EndStationCB.SelectedItem.ToString()!;
                                        Route fromLodzToOpole = new Route();
                                        fromLodzToOpole = fromLodzToOpole.FromLodzToOpole();
                                        trainWindow.ContentFrame.NavigationService.Navigate(new RouteAddPage(fromLodzToOpole, StartStation, EndStation));
                                        this.Close();
                                        break;
                                    }
                                case 1:
                                    {
                                        string StartStation = StartStationCB.SelectedItem.ToString()!;
                                        string EndStation = EndStationCB.SelectedItem.ToString()!;
                                        Route fromLodzToWroclaw = new Route();
                                        fromLodzToWroclaw = fromLodzToWroclaw.FromLodzWidzewToWroclawGlowny();
                                        trainWindow.ContentFrame.NavigationService.Navigate(new RouteAddPage(fromLodzToWroclaw, StartStation, EndStation));
                                        this.Close();
                                        break;
                                    }
                                case 2:
                                    {
                                        MessageBox.Show($"Nie można dodać trasy z {StartStationCB.SelectedItem} do {EndStationCB.SelectedItem}", "Błąd wyboru", MessageBoxButton.OK, MessageBoxImage.Error);
                                        break;
                                    }
                            }
                            break;
                        }
                }
            }
        }
    }
}
