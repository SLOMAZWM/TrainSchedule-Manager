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
        private TimeSpan StartTime;
        private TimeSpan EndTime;
        private int currentPageIndex = 0;
        
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
            for (int i = 0; i < StationsList.Count; i++)
            {
                Button pageButton = new Button()
                {
                    Content = (i + 1).ToString(),
                    Tag = i
                };
                pageButton.Click += PageButton_Click;
                PaginationItemsControl.ItemsSource = Enumerable.Range(1, StationsList.Count);
                Style = (Style)this.Resources["pagingButton"];
            }

            UpdateFrameContent(0);
        }

        private void PageButton_Click(object sender, RoutedEventArgs e) 
        { 
            if (sender is Button button)
            {
                currentPageIndex = Convert.ToInt32(button.Content) - 1;
                UpdateFrameContent(currentPageIndex);
            }
        }

        private void UpdateFrameContent(int PageIndex) 
        {
            var station = StationsList[PageIndex];
            StationDetailFrame.Content = new DetailStationControl(station);
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MouseDown_Click(object sender, RoutedEventArgs e)
        {
            this.DragMove();
        }
    }
}
