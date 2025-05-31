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

namespace Final_project
{
    /// <summary>  
    /// Interaction logic for LoginPage.xaml  
    /// </summary>  
    public partial class RegisterPage : Window
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            
            var mainWindow = new MainWindow();
            var service = App.SupabaseService;
            if (PasswordBox.Password == PasswordConfirmBox.Password)
            {
                await service.RegisterUserAsync(UsernameTextBox.Text, PasswordBox.Password);
                MessageBox.Show("Registration successful! You can now log in.");
                var LoginWindow = new LoginPage();
                LoginWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Passwords do not match. Please try again.");
                return;
            }
        }
        private void StartLoginCommand(object sender, MouseButtonEventArgs e)
        {
            var loginWindow = new LoginPage();
            loginWindow.Show();

            this.Close();
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
