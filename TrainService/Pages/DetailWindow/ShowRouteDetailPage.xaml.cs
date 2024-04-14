﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using ProjektLAB.TrainService.Class.ServiceClass;
using ProjektLAB.UserClass;

namespace ProjektLAB.TrainService.Pages.DetailWindow
{
    /// <summary>
    /// Interaction logic for ShowRouteDetailPage.xaml
    /// </summary>
    public partial class ShowRouteDetailPage : Page
    {
        private TrainSchedule currentSchedule;
        private User loggedUser;

        public ShowRouteDetailPage(TrainSchedule selectedSchedule, User user)
        {
            InitializeComponent();
            currentSchedule = selectedSchedule;
            LoadRouteDetails();
            loggedUser = user;
        }

        private void ManageTripBtn_Click(object sender, RoutedEventArgs e)
        {
            if (loggedUser != null)
            {
                bool isAlreadySaved = UserServiceDataBase.IsRouteAlreadySaved(loggedUser.Id, currentSchedule.IDTrainSchedule, currentSchedule.Route.StartStationName, currentSchedule.Route.EndStationName);

                if (isAlreadySaved)
                {
                    MessageBox.Show("Ta trasa została już zapisana na Twoim koncie.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    bool success = UserServiceDataBase.SaveUserRoute(loggedUser.Id, currentSchedule.IDTrainSchedule, currentSchedule.Route.StartStationName, currentSchedule.Route.EndStationName);
                    if (success)
                    {
                        MessageBox.Show("Trasa została pomyślnie zapisana.", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Nie udało się zapisać trasy.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Zarejestruj się lub zaloguj w celu zapisania trasy na swoim koncie!", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void LoadRouteDetails()
        {
            var startStationIndex = currentSchedule.Route!.Stations.FindIndex(s => s.Name == currentSchedule.Route.StartStationName);
            var endStationIndex = currentSchedule.Route!.Stations.FindIndex(s => s.Name == currentSchedule.Route.EndStationName);

            var displayInfoList = new List<StationDisplayInfo>();

            for (int i = 0; i < currentSchedule.Route!.Stations.Count; i++)
            {
                var station = currentSchedule.Route!.Stations[i];
                var displayInfo = new StationDisplayInfo
                {
                    Name = station.Name,
                    ArrivalTime = station.ArrivalTime,
                    DepartureTime = station.DepartureTime,
                    DelayTime = currentSchedule.DelayTime ?? "Brak",
                    PlatformNumber = station.PlatformNumber ?? 0,
                    TrackNumber = station.TrackNumber ?? 0,
                    TrainNumber = currentSchedule.Train?.TrainNumber ?? "Brak danych",

                    IsStartStation = i == startStationIndex,
                    IsEndStation = i == endStationIndex,
                    IsIntermediateStation = i > startStationIndex && i < endStationIndex,
                };
                displayInfoList.Add(displayInfo);
            }

            StationsListView.ItemsSource = displayInfoList;
        }


    }
}
