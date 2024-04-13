using ProjektLAB.UserClass;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektLAB.TrainService.Pages
{
    /// <summary>
    /// Interaction logic for UserHistoryPage.xaml
    /// </summary>
    public partial class UserHistoryPage : Page
    {
        public User loggedUser { get; set; }

        public UserHistoryPage(User user)
        {
            InitializeComponent();
            loggedUser = user;
            this.DataContext = loggedUser;
            LoadUserTravelHistories();
        }

        private void LoadUserTravelHistories()
        {
            var histories = UserServiceDataBase.GetTravelHistoriesByUserId(loggedUser.Id);
            TravelHistoryList.ItemsSource = histories;
        }
    }
}
