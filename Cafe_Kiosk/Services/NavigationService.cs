using Cafe_Kiosk.Stores;
using Cafe_Kiosk.ViewModels;
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
                case NaviType.FirstView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(FirstViewModel));
                    break;
                case NaviType.SecondView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(SecondViewModel));
                    break;
                case NaviType.ThirdView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(ThirdViewModel));
                    break;
                default:
                    return;
            }
        }
    }
}