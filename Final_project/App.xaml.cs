using System.Configuration;
using System.Data;
using System.Windows;
using Final_project_wpf.SupabaseService;

namespace Final_project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static SupabaseClientService SupabaseService { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SupabaseService = new SupabaseClientService();
            await SupabaseService.InitializeAsync();
        }
    }

}
