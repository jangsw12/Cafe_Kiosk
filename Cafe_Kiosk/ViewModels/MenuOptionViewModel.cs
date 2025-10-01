using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Services.Dialog;
using Cafe_Kiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cafe_Kiosk.ViewModels
{
    public class MenuOptionViewModel : ViewModelBase
    {
        // Properties
        private readonly CafeMenuItem _selectedItem;
        private readonly IDialogService _dialogService;
        private readonly ICartService _cartService;
        public Uri ImageUri => _selectedItem.ImageUri;

        // Commands
        public ICommand AddMenuCommand { get; set; }
        public ICommand CancelMenuCommand { get; set; }

        // Constructor
        public MenuOptionViewModel(CafeMenuItem selectedItem, IDialogService dialogService, ICartService cartService)
        {
            _selectedItem = selectedItem;
            _dialogService = dialogService;
            _cartService = cartService;

            AddMenuCommand = new RelayCommand<object>(AddMenu);
            CancelMenuCommand = new RelayCommand<object>(CancelMenu);
        }

        // Methods
        public void OpenView()
        {
            var view = new MenuOptionView
            {
                Owner = Application.Current.MainWindow
            };

            view.ShowDialog();
        }

        private void AddMenu(object _)
        {
            _cartService.AddItem(_selectedItem);
            _dialogService.CloseMenuOptionDialog();
        }

        private void CancelMenu(object _)
        {
            _dialogService.CloseMenuOptionDialog();
        }
    }
}