using Cafe_Kiosk.Services;
using Cafe_Kiosk.Stores;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cafe_Kiosk.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Properties
        private readonly MainNavigationStore _mainNavigationStore;
        private readonly INavigationService _navigationService;

        public CartViewModel _cartViewModel { get; }
        public PaymentViewModel _paymentViewModel { get; }

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
        public MainViewModel(MainNavigationStore mainNavigationStore, INavigationService navigationService, 
                             CartViewModel cartViewModel, PaymentViewModel paymentViewModel)
        {
            _mainNavigationStore = mainNavigationStore;
            _navigationService = navigationService;

            _cartViewModel = cartViewModel;
            _paymentViewModel = paymentViewModel;

            _mainNavigationStore.CurrentViewModelChanged += CurrentViewModelChanged;
            _navigationService.Navigate(NaviType.MenuView);
        }

        // Methods
        private void CurrentViewModelChanged()
        {
            CurrentViewModel = _mainNavigationStore.CurrentViewModel;
        }
    }
}