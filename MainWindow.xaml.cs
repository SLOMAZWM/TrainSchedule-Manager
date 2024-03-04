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
using ProjektLAB.TrainService;

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

        private bool IsValidRegister()
        {
            if(RegisterLoginTxt.Text.Length < 4) 
            {
                MessageBox.Show("Wypełnij Pole Loginu w rejestracji - użyj minimum 4 znaków!", "Błąd rejestracji", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(RegisterPasswordTxt.Password.Length < 8) 
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

        private bool IsValidLogin()
        {
            if(LoginLoginTxt.Text.Length <= 0)
            {
                MessageBox.Show("Wypełnij Pole Loginu, w Logowaniu", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(LoginPasswordTxt.Password == null) 
            {
                MessageBox.Show("Wypełnij Pole Hasła, w Logowaniu", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            if(IsValidRegister()) 
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
                RegisterClearInputs();
                TrainServiceWindow Window = new TrainServiceWindow(registerUser);
                Window.Show();
                this.Close();
            }
           else
            {
                return;
            }
        }

        private void RegisterClearInputs()
        {
            RegisterLoginTxt.Text = "";
            RegisterPasswordTxt.Password = "";
            First_NameTxt.Text = "";
            Last_NameTxt.Text = "";
            BirthDayDP.Text = "";
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e ) 
        {
            if(IsValidLogin()) 
            {
                User loggedUser = UserServiceDataBase.TryLoginUser(LoginLoginTxt.Text, LoginPasswordTxt.Password);

                if(loggedUser != null) 
                {
                    MessageBox.Show("Zalogowano pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                    TrainServiceWindow newWindow = new TrainServiceWindow(loggedUser);
                    newWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login lub hasło jest niepoprawne!", "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}