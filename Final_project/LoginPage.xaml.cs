
using System.Windows;
using System.Windows.Input;
using Final_project_wpf.Core.Auth;

namespace Final_project
{
    public partial class LoginPage : Window
    {
        private readonly AuthService _authService;

        public LoginPage(AuthService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = await _authService.Login(
                    EmailTextBox.Text,
                    PasswordBox.Password
                );

                if (user != null)
                {
                    var mainWindow = App.GetService<MainWindow>();
                    mainWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid email or password. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void StartRegisterCommand(object sender, MouseButtonEventArgs e)
        {
            var registerWindow = App.GetService<RegisterPage>();
            registerWindow.Show();
            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
