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

        // Methods
        public MainViewModel(MainNavigationStore mainNavigationStore, INavigationService navigationService)
        {
            _mainNavigationStore = mainNavigationStore;
            _navigationService = navigationService;
            _mainNavigationStore.CurrentViewModelChanged += CurrentViewModelChanged;

            _navigationService.Navigate(NaviType.MenuView);
        }

        private void CurrentViewModelChanged()
        {
            CurrentViewModel = _mainNavigationStore.CurrentViewModel;
        }
    }
}