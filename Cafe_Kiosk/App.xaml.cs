using Cafe_Kiosk.Services;
using Cafe_Kiosk.Services.API;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Services.Dialog;
using Cafe_Kiosk.Services.Navi;
using Cafe_Kiosk.Services.Payment;
using Cafe_Kiosk.Stores;
using Cafe_Kiosk.ViewModels;
using Cafe_Kiosk.ViewModels.Payment;
using Cafe_Kiosk.Views;
using Cafe_Kiosk.Views.Payment;
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
            services.AddSingleton<PaymentNavigationStore>();
            services.AddSingleton<PaymentSelectionStore>();

            // Services
            services.AddSingleton<IMenuApiService, MenuApiService>();
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<ICartService, CartService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<IPaymentFlowManager, PaymentFlowManager>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MenuViewModel>();
            services.AddSingleton<CartViewModel>();
            services.AddSingleton<MenuOptionViewModel>();
            services.AddSingleton<PaymentViewModel>();
            services.AddTransient<PaymentStartViewModel>();
            services.AddTransient<PaymentMethodViewModel>();
            services.AddTransient<PaymentProcessingViewModel>();
            services.AddTransient<PaymentWaitingViewModel>();
            services.AddTransient<PaymentResultViewModel>();
            services.AddTransient<ReceiptOptionViewModel>();

            // Views
            services.AddSingleton(s => new MainView()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });
            services.AddTransient(s => new MenuOptionView
            {
                DataContext = s.GetRequiredService<MenuOptionViewModel>()
            });
            services.AddTransient(s => new PaymentView
            {
                DataContext = s.GetRequiredService<PaymentViewModel>()
            });

            return services.BuildServiceProvider();
        }
    }
}