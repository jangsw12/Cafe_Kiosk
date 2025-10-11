using Cafe_Kiosk.Stores;
using Cafe_Kiosk.ViewModels;
using Cafe_Kiosk.ViewModels.Payment;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Services.Navi
{
    public class NavigationService : INavigationService
    {
        private readonly PaymentNavigationStore _paymentNavigationStore;

        public INotifyPropertyChanged? CurrentViewModel
        {
            set => _paymentNavigationStore.CurrentViewModel = value;
        }

        public NavigationService(PaymentNavigationStore paymentNavigationStore)
        {
            _paymentNavigationStore = paymentNavigationStore;
        }

        public void Navigate(NaviType naviType)
        {
            switch (naviType)
            {
                case NaviType.PaymentStartView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(PaymentStartViewModel));
                    break;
                case NaviType.PaymentMethodView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(PaymentMethodViewModel));
                    break;
                case NaviType.PaymentProcessingView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(PaymentProcessingViewModel));
                    break;
                case NaviType.PaymentWaitingView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(PaymentWaitingViewModel));
                    break;
                case NaviType.PaymentResultView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(PaymentResultViewModel));
                    break;
                case NaviType.ReceiptOptionView:
                    CurrentViewModel = (ViewModelBase)App.Current.Services.GetRequiredService(typeof(ReceiptOptionViewModel));
                    break;
                default:
                    return;
            }
        }
    }
}