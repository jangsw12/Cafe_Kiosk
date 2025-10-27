using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Input;
using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Services.Payment;
using Cafe_Kiosk.Models.Enums;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentViewModel : ViewModelBase
    {
        // Properties
        private readonly PaymentNavigationStore _paymentNavigationStore;
        private readonly IPaymentFlowManager _paymentFlowManager;

        private INotifyPropertyChanged? _currentViewModel;

        public INotifyPropertyChanged? CurrentViewModel
        {
            get { return _currentViewModel; }
            set { 
                if (_currentViewModel != value)
                {
                    _currentViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        // Constructor
        public PaymentViewModel(PaymentNavigationStore paymentNavigationStore, IPaymentFlowManager paymentFlowManager)
        {
            _paymentNavigationStore = paymentNavigationStore;
            _paymentFlowManager = paymentFlowManager;

            _paymentNavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            _paymentFlowManager.GoToStart();
        }

        // Methods
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModel = _paymentNavigationStore.CurrentViewModel;
        }
    }
}