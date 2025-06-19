using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Final_project_wpf.Core.Auth;
using System.Threading.Channels;


namespace Final_project
{
    public partial class RegisterPage : Window
    {
        
        private readonly AuthService _authService;


        public RegisterPage(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }


        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (PasswordBox.Password != PasswordConfirmBox.Password)
            {
                MessageBox.Show("Passwords do not match. Please re-enter your password.");
                return;
            }

            try
            {
                var user = await _authService.Register(
                    EmailTextBox.Text,
                    PasswordBox.Password
                );

                if (user != null)
                {
                    var mainWindow = App.GetService<MainWindow>();
                    MessageBox.Show("Registration successful! Redirecting to main page.");
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    
                    MessageBox.Show("This email is already registered. Redirecting to login page.");
                    var loginWindow = App.GetService<LoginPage>();
                    loginWindow.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void StartLoginCommand(object sender, MouseButtonEventArgs e)
        {
            var loginWindow = App.GetService<LoginPage>();
            loginWindow.Show();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



    }
}
