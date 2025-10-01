using Cafe_Kiosk.Stores;
using Cafe_Kiosk.ViewModels;
using Cafe_Kiosk.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Services
{
    public class NavigationService : INavigationService
    {
        private readonly MainNavigationStore _mainNavigationStore;
        public INotifyPropertyChanged? CurrentViewModel
        {
            set => _mainNavigationStore.CurrentViewModel = value;
        }

        public NavigationService(MainNavigationStore mainNavigationStore)
        {
            _mainNavigationStore = mainNavigationStore;
        }

        public void Navigate(NaviType naviType)
        {
            switch (naviType)
            {
                case NaviType.MenuView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(MenuViewModel));
                    break;
                case NaviType.CartView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(CartViewModel));
                    break;
                case NaviType.MenuOptionView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(MenuOptionViewModel));
                    break;
                default:
                    return;
            }
        }
    }
}