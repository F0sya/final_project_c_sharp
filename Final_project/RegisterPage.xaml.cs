using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace Final_project
{
    public partial class RegisterPage : Window
    {
        private readonly string MinPasswordLength = "6"; // Matches SupabaseClientService

        public RegisterPage()
        {
            InitializeComponent();
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
