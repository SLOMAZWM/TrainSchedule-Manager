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
using ProjektLAB.TrainService.Class;

namespace ProjektLAB.TrainService.Pages.DialogWindow.DetailStation
{
    /// <summary>
    /// Interaction logic for DetailStationWindow.xaml
    /// </summary>
    public partial class DetailStationWindow : Window
    {
        private ListBox listOfStations;
        private TimeSpan StartTime;
        private TimeSpan EndTime;
        private Station selectedStation;

        public DetailStationWindow(ListBox listOfSt, TimeSpan StartT, TimeSpan EndT, Station selectS)
        {
            InitializeComponent();
            listOfStations = listOfSt;
            StartTime = StartT;
            EndTime = EndT;
            selectedStation = selectS;
            InitializeData(selectedStation);
            InitializePagination();
        }

        private void InitializeData(Station selectS)
        {
            NameOfStationsLbl.Content = selectS.Name;
            ArrivalTimeTextBox.Text = selectS.ArrivalTime;
            DepartureTimeTextBox.Text = selectS.DepartureTime;

            PlatformCmB.Items.Clear();
            TrackCmB.Items.Clear();

            for (int i = 1; i <= selectS.PlatformNumber; i++) 
            {
                PlatformCmB.Items.Add(i.ToString());
            }

            if (selectS.SelectedPlatform != null)
            {
                PlatformCmB.SelectedItem = selectS.SelectedPlatform.ToString();
            }

            if(selectS.SelectedTrack != null) 
            { 
                TrackCmB.SelectedItem = selectS.SelectedTrack.ToString();
            }

            TrackCmB.IsEnabled = false;
        }

        private void InitializePagination()
        {
            PaginationItemsControl.Items.Clear();

            for (int i = 0; i < listOfStations.Items.Count; i++)
            {
                var button = new Button
                {
                    Content = (i + 1).ToString(), 
                    Style = Application.Current.Resources["pagingButton"] as Style,
                    Tag = i 
                };
                button.Click += PageButton_Click; 

                PaginationItemsControl.Items.Add(button); 
            }
        }

        private void PlatformCmB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TrackCmB.IsEnabled = true;
            if (PlatformCmB.SelectedItem != null)
            {
                int selectedPlatformNumber = int.Parse(PlatformCmB.SelectedItem.ToString()!);

                TrackCmB.Items.Clear();

                int firstTrackNumber = (selectedPlatformNumber - 1) * 2 + 1;
                TrackCmB.Items.Add(firstTrackNumber.ToString());
                TrackCmB.Items.Add((firstTrackNumber + 1).ToString());

                if (selectedStation.SelectedTrack != null &&
                    (selectedStation.SelectedTrack == firstTrackNumber || selectedStation.SelectedTrack == firstTrackNumber + 1))
                {
                    TrackCmB.SelectedItem = selectedStation.SelectedTrack.ToString();
                }
                else
                {
                    TrackCmB.SelectedIndex = 0;
                }
            }
        }


        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int index = (int)button.Tag;
                if (index >= 0 && index < listOfStations.Items.Count)
                {
                    listOfStations.SelectedIndex = index;
                    Station selectedStation = (Station)listOfStations.Items[index];

                    UpdateSelectedStation();
                }
            }
        }

        private void FirstPageButton_Click(object sender, RoutedEventArgs e)
        {
            listOfStations.SelectedIndex = 0;
            UpdateSelectedStation();
        }

        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (listOfStations.SelectedIndex > 0)
            {
                listOfStations.SelectedIndex--;
                UpdateSelectedStation();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (listOfStations.SelectedIndex < listOfStations.Items.Count - 1)
            {
                listOfStations.SelectedIndex++;
                UpdateSelectedStation();
            }
        }

        private void LastPageButton_Click(object sender, RoutedEventArgs e)
        {
            listOfStations.SelectedIndex = listOfStations.Items.Count - 1;
            UpdateSelectedStation();
        }

        private void UpdateSelectedStation()
        {
            if (listOfStations.SelectedIndex != -1)
            {
                selectedStation = (Station)listOfStations.SelectedItem;
                InitializeData(selectedStation);
            }
        }


        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (TimeSpan.TryParseExact(ArrivalTimeTextBox.Text, "hh\\:mm", null, out TimeSpan arrivalTime) &&
                TimeSpan.TryParseExact(DepartureTimeTextBox.Text, "hh\\:mm", null, out TimeSpan departureTime))
            {
                if (arrivalTime < StartTime)
                {
                    MessageBox.Show($"Czas przyjazdu nie może być wcześniejszy niż czas rozpoczęcia kursu ({StartTime.ToString("hh\\:mm")}).", "Błąd czasu przyjazdu", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (departureTime <= arrivalTime)
                {
                    MessageBox.Show("Czas odjazdu musi być późniejszy niż czas przyjazdu.", "Błąd czasu odjazdu", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (departureTime > EndTime)
                {
                    MessageBox.Show($"Czas odjazdu nie może być późniejszy niż czas zakończenia kursu ({EndTime.ToString("hh\\:mm")}).", "Błąd czasu odjazdu", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (PlatformCmB.SelectedItem == null)
                {
                    MessageBox.Show("Proszę wybrać peron.", "Błąd wyboru peronu", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else if (TrackCmB.SelectedItem == null)
                {
                    MessageBox.Show("Proszę wybrać tor.", "Błąd wyboru toru", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    selectedStation.ArrivalTime = ArrivalTimeTextBox.Text;
                    selectedStation.DepartureTime = DepartureTimeTextBox.Text;
                    selectedStation.SelectedPlatform = int.Parse(PlatformCmB.SelectedItem.ToString()!);
                    selectedStation.SelectedTrack = int.Parse(TrackCmB.SelectedItem.ToString()!);
                    MessageBox.Show($"Zmiany zostały poprawnie zapisane dla: {selectedStation.Name}!", "Poprawny zapis", MessageBoxButton.OK, MessageBoxImage.Information);
                    NextPageButton_Click(sender, e);
                    UpdateSelectedStation();
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowy format czasu. Użyj formatu HH:mm.", "Błąd formatu czasu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
