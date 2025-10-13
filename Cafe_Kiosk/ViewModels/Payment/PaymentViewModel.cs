using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Services.Navi;
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

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentViewModel : ViewModelBase
    {
        // Properties
        private readonly PaymentNavigationStore _paymentNavigationStore;
        private readonly ICartService _cartService;

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

        public ObservableCollection<CartItem> CartItems => _cartService.GetItems();

        public int TotalCartPrice => CartItems.Sum(item => item.TotalPrice);

        // Constructor
        public PaymentViewModel(PaymentNavigationStore paymentNavigationStore, INavigationService navigationService, 
                                ICartService cartService)
        {
            _paymentNavigationStore = paymentNavigationStore;
            _cartService = cartService;

            _paymentNavigationStore.CurrentViewModelChanged += CurrentViewModelChanged;

            // Payment Start View
            navigationService.Navigate(NaviType.PaymentStartView);
        }

        // Methods
        private void CurrentViewModelChanged()
        {
            CurrentViewModel = _paymentNavigationStore.CurrentViewModel;
        }
    }
}