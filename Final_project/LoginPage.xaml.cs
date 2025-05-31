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
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();

        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void StartRegisterCommand(object sender, MouseButtonEventArgs e)
        {
            var registerWindow = new RegisterPage();
            registerWindow.Show();

            this.Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
