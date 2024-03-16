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
using ProjektLAB.TrainService.Pages.DialogWindow.DetailStation;

namespace ProjektLAB.TrainService.Pages.DialogWindow
{
    /// <summary>
    /// Interaction logic for DetailStationDialog.xaml
    /// </summary>
    public partial class DetailStationDialog : Window
    {
        public List<Station> StationsList {get; set;}
        private Dictionary<int, DetailStationControl> _paginationControls = new Dictionary<int, DetailStationControl>();
        private TimeSpan StartTime;
        private TimeSpan EndTime;
        private int currentPageIndex = 0;
        public Action<List<Station>> ?OnStationsUpdated;

        public DetailStationDialog(Route route, TimeSpan StartT, TimeSpan EndT)
        {
            InitializeComponent();
            StationsList = route.Stations;
            StartTime = StartT;
            EndTime = EndT;
            InitializePagination();
        }

        private void InitializePagination()
        {
            PaginationItemsControl.ItemsSource = Enumerable.Range(1, StationsList.Count);

            for (int i = 0; i < StationsList.Count; i++)
            {
               
                var stationControl = new DetailStationControl(StationsList[i]);


                _paginationControls.Add(i, stationControl);
            }

           
            UpdateFrameContent(0);
        }

        private void PageButton_Click(object sender, RoutedEventArgs e) 
        {
            if (sender is Button button && int.TryParse(button.Content.ToString(), out int pageIndex))
            {
                currentPageIndex = pageIndex - 1;
                UpdateFrameContent(currentPageIndex);
            }
        }

        private void UpdateFrameContent(int PageIndex) 
        {
            if (_paginationControls.TryGetValue(PageIndex, out var control))
            {
                StationDetailFrame.Content = control;
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MouseDown_Click(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }

        private void SaveAllChanges_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
