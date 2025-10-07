using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Models;
using Cafe_Kiosk.Models.Enums;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Services.Dialog;
using Cafe_Kiosk.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        public int TotalPrice
        {
            get
            {
                int basePrice = _selectedItem.Price;

                int sizeExtra = SelectedCoffeeSize switch
                {
                    CoffeeSize.Small => 0,
                    CoffeeSize.Tall => 500,
                    CoffeeSize.Large => 1000,
                    _ => 0
                };

                return (basePrice + sizeExtra) * Quantity;
            }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { 
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPrice));

                    (DecreaseQuantityCommand as RelayCommand<object>)?.RaiseCanExecuteChanged();
                }
            }
        }

        private CoffeeSize _selectedCoffeeSize = CoffeeSize.Small;

        public CoffeeSize SelectedCoffeeSize
        {
            get { return _selectedCoffeeSize; }
            set { 
                if (_selectedCoffeeSize != value)
                {
                    _selectedCoffeeSize = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        private IceAmount _selectedIceAmount = IceAmount.Regular;

        public IceAmount SelectedIceAmount
        {
            get { return _selectedIceAmount; }
            set { 
                if (_selectedIceAmount != value)
                {
                    _selectedIceAmount = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TotalPrice));
                }
            }
        }

        // Commands
        public ICommand AddMenuCommand { get; set; }
        public ICommand CancelMenuCommand { get; set; }
        public ICommand DecreaseQuantityCommand { get; set; }
        public ICommand IncreaseQuantityCommand { get; set; }

        // Constructor
        public MenuOptionViewModel(CafeMenuItem selectedItem, IDialogService dialogService, ICartService cartService)
        {
            _selectedItem = selectedItem;
            _dialogService = dialogService;
            _cartService = cartService;

            _quantity = 1;

            AddMenuCommand = new RelayCommand<object>(AddMenu);
            CancelMenuCommand = new RelayCommand<object>(CancelMenu);
            DecreaseQuantityCommand = new RelayCommand<object>(DecreaseQuantity, CanDecreaseQuantity);
            IncreaseQuantityCommand = new RelayCommand<object>(IncreaseQuantity);
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
            var carItem = new CartItem(_selectedItem, Quantity)
            {
                SelectedCoffeeSize = this.SelectedCoffeeSize,
                SelectedIceAmount = this.SelectedIceAmount
            };

            _cartService.AddItem(carItem);
            _dialogService.CloseMenuOptionDialog();
        }

        private void CancelMenu(object _)
        {
            _dialogService.CloseMenuOptionDialog();
        }

        private void DecreaseQuantity(object _)
        {
            if (Quantity > 1)
                Quantity--;
        }

        private bool CanDecreaseQuantity(object _)
        {
            return Quantity > 1;
        }

        private void IncreaseQuantity(object _)
        {
            Quantity++;
        }
    }
}