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

namespace ProjektLAB.TrainService
{
    /// <summary>
    /// Interaction logic for TrainServiceWindow.xaml
    /// </summary>
    public partial class TrainServiceWindow : Window
    {
        public User ? User { get; set; }
        public TrainServiceWindow(User loggedUser)
        {
            InitializeComponent();
            User = loggedUser;
        }

        public TrainServiceWindow() //Guest Login
        {
            InitializeComponent();
            User = null;
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
