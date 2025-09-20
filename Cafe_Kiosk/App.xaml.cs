using Cafe_Kiosk.ViewModels;
using Cafe_Kiosk.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Cafe_Kiosk
{
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();

            var mainView = Services.GetRequiredService<MainView>();
            mainView.Show();
        }

        public new static App Current => (App)Application.Current;

        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // ViewModels
            services.AddSingleton<MainViewModel>();

            // Views
            services.AddSingleton(s => new MainView()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            return services.BuildServiceProvider();
        }
    }
}