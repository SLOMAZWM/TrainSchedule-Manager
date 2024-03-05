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
using ProjektLAB.TrainService.Pages;

namespace ProjektLAB.TrainService
{
    /// <summary>
    /// Interaction logic for TrainServiceWindow.xaml
    /// </summary>
    public partial class TrainServiceWindow : Window
    {
        public User? User { get; set; }
        private Dictionary<string, Page> PageDictionary = new Dictionary<string, Page>()
{
            { "Home", new HomePage()},
            //Add new Page
    };

        public TrainServiceWindow(User loggedUser)
        {
            InitializeComponent();
            User = loggedUser;
        }

        public TrainServiceWindow() //Guest Login
        {
            InitializeComponent();
            User = null;
            //InitializeGuestLogin();
        }

        private void InitializeGuestLogin()
        {
            Schedule.IsEnabled = false;
            Stations.IsEnabled = false;
            Schedule.Background = new SolidColorBrush(Colors.Gray);
            Stations.Background = new SolidColorBrush(Colors.Gray);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string pageKey)
            {
                Page pageToNavigate;
                if (PageDictionary.TryGetValue(pageKey, out pageToNavigate))
                 { 
                    ContentFrame.Navigate(pageToNavigate);
                }
            }
        }
    }
}
