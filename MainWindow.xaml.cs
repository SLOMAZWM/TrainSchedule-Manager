using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjektLAB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool IsValid()
        {
            if(RegisterLoginTxt.Text.Length < 4) 
            {
                MessageBox.Show("Wypełnij Pole Loginu w rejestracji - użyj minimum 4 znaków!", "Błąd rejestracji", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(RegisterPasswordTxt.Password.Length < 5) 
            {
                MessageBox.Show("Wypełnij Pole Hasła w rejestracji - użyj minimum 5 znaków!", "Błąd rejestracji", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(First_NameTxt.Text.Length == 0) 
            {
                MessageBox.Show("Wypełnij Pole Imienia w rejestracji", "Błąd rejestracji", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(Last_NameTxt.Text.Length == 0)
            {
                MessageBox.Show("Wypełnij Pole Nazwiska w rejestracji", "Błąd rejestracji", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(BirthDayDP.Text.Length == 0) 
            {
                MessageBox.Show("Wypełnij Pole Daty Urodzin w rejestracji", "Błąd rejestracji", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if(IsValid()) 
            {
                User registerUser = new User()
                {
                    Login = RegisterLoginTxt.Text,
                    Password = RegisterPasswordTxt.Password,
                    First_Name = First_NameTxt.Text,
                    Last_Name = Last_NameTxt.Text,
                    BirthDay = BirthDayDP.Text
                };
                UserServiceDataBase.AddNewUserToDataBase(registerUser);
            }
           else
            {
                return;
            }
        }


        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}