using Final_project_wpf.Core.Auth;
using Final_project_wpf.Core.Configuration;
using Final_project_wpf.Core.Interfaces;
using Final_project_wpf.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Final_project_wpf.Core;
using Final_project_wpf.MVVM.ViewModel;

namespace Final_project
{
    public partial class App : Application
    {
        private static IServiceProvider? _serviceProvider;

        // Change to IServiceProvider and add null-check operator
        public static IServiceProvider ServiceProvider => _serviceProvider ?? throw new InvalidOperationException("ServiceProvider is not initialized");

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Configure Supabase settings
            var supabaseSettings = new SupabaseSettings
            {
                Url = "https://jeszlpmipprqfwmlreao.supabase.co",
                Key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Implc3pscG1pcHBycWZ3bWxyZWFvIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDgxMjkwMTQsImV4cCI6MjA2MzcwNTAxNH0.mTwaMpfbOheO-YkxbIw7pCi3QdN8lD_2-39IRoYjVEQ"
            };

            // Register settings
            services.AddSingleton(supabaseSettings);

            // Register Supabase Client as singleton
            services.AddSingleton<Supabase.Client>(sp =>
                new Supabase.Client(
                    supabaseSettings.Url,
                    supabaseSettings.Key,
                    new Supabase.SupabaseOptions
                    {
                        AutoConnectRealtime = true,
                        AutoRefreshToken = true
                    }
                )
            );

            // Register repositories
            services.AddSingleton<ISupabaseAuthRepository>(sp =>
                new SupabaseAuthRepository(
                    supabaseSettings.Url,
                    supabaseSettings.Key
                )
            );



            // Register services
            services.AddSingleton<AuthService>();
            services.AddSingleton<TranslatorService>();
            services.AddSingleton<SimpleTextGenerator>(provider =>
     new SimpleTextGenerator("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"));

            // Register ViewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<TranslatorViewModel>();

            // Register Windows and Views
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginPage>();
            services.AddTransient<RegisterPage>();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                var authService = ServiceProvider.GetRequiredService<AuthService>();
                await authService.Initialize();

                var loginPage = ServiceProvider.GetRequiredService<LoginPage>();
                loginPage?.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during startup: {ex.Message}", "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(-1);
            }
        }


        // Add helper method for getting services
        public static T GetService<T>() where T : class
        {
            if (_serviceProvider == null)
            {
                throw new InvalidOperationException("ServiceProvider is not initialized");
            }

            var service = _serviceProvider.GetService<T>();
            if (service == null)
            {
                throw new InvalidOperationException($"Failed to resolve service of type {typeof(T).Name}");
            }

            return service;
        }

    }
}
