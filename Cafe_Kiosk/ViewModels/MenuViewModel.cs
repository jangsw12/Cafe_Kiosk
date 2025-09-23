using Cafe_Kiosk.Services;
using Cafe_Kiosk.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Cafe_Kiosk.Stores;
using System.IO;
using Cafe_Kiosk.ViewModels;
using System.Windows;
using Cafe_Kiosk.Views;
using Cafe_Kiosk.Services.MenuData;
using Cafe_Kiosk.Services.Dialog;

namespace Cafe_Kiosk.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        // Properties
        private readonly IMenuDataService _menuDataService;
        private readonly IDialogService _dialogService;

        private List<CafeMenuItem> _menuItems;

        public List<CafeMenuItem> MenuItems
        {
            get { return _menuItems; }
            set {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand MenuItemClickCommand { get; set; }

        // Methods
        public MenuViewModel(IMenuDataService menuDataService, IDialogService dialogService)
        {
            _menuDataService = menuDataService;
            _dialogService = dialogService;
            
            MenuItemClickCommand = new RelayCommand<CafeMenuItem>(MenuItemClick);

            LoadMenu();
        }

        private void MenuItemClick(CafeMenuItem selectedItem)
        {
            _dialogService.ShowMenuOptionDialog(selectedItem);
        }

        private void LoadMenu()
        {
            MenuItems = _menuDataService.LoadCafeMenu();
        }
    }
}