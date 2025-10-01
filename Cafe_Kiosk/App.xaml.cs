using Cafe_Kiosk.Services;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Services.Dialog;
using Cafe_Kiosk.Services.MenuData;
using Cafe_Kiosk.Stores;
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

            // Stores
            services.AddSingleton<MainNavigationStore>();

            // Services
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IMenuDataService, MenuDataService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<ICartService, CartService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MenuViewModel>();
            services.AddSingleton<CartViewModel>();
            services.AddSingleton<MenuOptionViewModel>();

            // Views
            services.AddSingleton(s => new MainView()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            return services.BuildServiceProvider();
        }
    }
}