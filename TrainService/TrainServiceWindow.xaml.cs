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
using ProjektLAB.UserClass;

namespace ProjektLAB.TrainService
{
    /// <summary>
    /// Interaction logic for TrainServiceWindow.xaml
    /// </summary>
    public partial class TrainServiceWindow : Window
    {
        public User User { get; set; }
        private Dictionary<string, Page> PageDictionary;

        public TrainServiceWindow(User loggedUser)
        {
            InitializeComponent();
            User = loggedUser;
            if(User.IsAdmin == false)
            {
                InitializeUserLogin();
                PageDictionary = InitializePageForUser();
            }
            else if(User.IsAdmin == true)
            {
                PageDictionary = InitializePageForAdmin();
            }
            else
            {
                PageDictionary = new Dictionary<string, Page>();
                MessageBox.Show("Błąd odczytu uprawnień z bazy danych!", "Błąd bazy danych", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        public TrainServiceWindow() //Guest Login
        {
            InitializeComponent();
            User = null!;
            InitializeGuestLogin();
            PageDictionary = InitializePageDictionaryForGuest();
        }

        private Dictionary<string, Page> InitializePageForUser()
        {
            Dictionary<string, Page> pages = new Dictionary<string, Page>();
            pages.Add("ROZKŁAD JAZDY", new TimeTablePage(User));
            pages.Add("HISTORIA UŻYTKOWNIKA", new UserHistoryPage(User));
            return pages;
        }

        private Dictionary<string, Page> InitializePageForAdmin()
        {
            Dictionary<string, Page> pages = new Dictionary<string, Page>();
            pages.Add("ROZKŁAD JAZDY", new TimeTablePage(User));
            pages.Add("HISTORIA UŻYTKOWNIKA", new UserHistoryPage(User));
            pages.Add("ZARZĄDZANIE STACJAMI", new StationsPage(this));
            return pages;
        }

        private Dictionary<string, Page> InitializePageDictionaryForGuest()
        {
            Dictionary<string, Page> pages = new Dictionary<string, Page>();

            pages.Add("ROZKŁAD JAZDY", new TimeTablePage(User));

            return pages;
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            //Owner = mainWindow;
            mainWindow.Show();
            User = null!;
            this.Close();
        }

        private void InitializeUserLogin()
        {
            ManagementBtn.Visibility = Visibility.Hidden;
        }

        private void InitializeGuestLogin()
        {
            HistoryBtn.IsEnabled = false;
            ManagementBtn.Visibility = Visibility.Collapsed;
            HistoryBtn.Background = new SolidColorBrush(Colors.Gray);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NavigationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string pageKey)
            {
                if(User != null && User.IsAdmin == false)
                {
                    PageDictionary = InitializePageForUser();
                    Page pageToNavigate;
                    if (PageDictionary.TryGetValue(pageKey, out pageToNavigate!))
                    {
                        ContentFrame.Navigate(pageToNavigate);
                        TitleLbl.Content = pageKey;
                    }
                }
                else if(User != null && User.IsAdmin == true)
                {
                    PageDictionary = InitializePageForAdmin();
                    Page pageToNavigate;
                    if (PageDictionary.TryGetValue(pageKey, out pageToNavigate!))
                    {
                        ContentFrame.Navigate(pageToNavigate);
                        TitleLbl.Content = pageKey;
                    }
                }
                else if(User == null)
                {
                    PageDictionary = InitializePageDictionaryForGuest();
                    Page pageToNavigate;
                    if (PageDictionary.TryGetValue(pageKey, out pageToNavigate!))
                    {
                        ContentFrame.Navigate(pageToNavigate);
                        TitleLbl.Content = pageKey;
                    }
                }
                else
                {
                    MessageBox.Show("Błąd odczytu uprawnień z bazy danych przy wczytywaniu nawigacji", "Błąd bazy danych", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


    }
}
