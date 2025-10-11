using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Services.Dialog;
using Cafe_Kiosk.ViewModels.Payment;
using Cafe_Kiosk.Views.Payment;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cafe_Kiosk.ViewModels
{
    public class CartViewModel : ViewModelBase
    {
		// Properties
        private readonly ICartService _cartService;
        private readonly IDialogService _dialogService;

        public ObservableCollection<CartItem> CartItems => _cartService.GetItems();

        public int TotalCartPrice => CartItems.Sum(item => item.TotalPrice);

        // Commands
        public ICommand PayCommand { get; set; }
        public ICommand ClearCartCommand { get; set; }
        public ICommand DecreaseQuantityCommand { get; set; }
        public ICommand IncreaseQuantityCommand { get; set; }
        public ICommand RemoveItemCommand { get; set; }

        // Constructor
        public CartViewModel(ICartService cartService, IDialogService dialogService)
        {
            _cartService = cartService;
            _dialogService = dialogService;

            PayCommand = new RelayCommand<object>(Pay, CanPay);
            ClearCartCommand = new RelayCommand<object>(ClearCart, CanClearCart);
            DecreaseQuantityCommand = new RelayCommand<object>(DecreaseQuantity);
            IncreaseQuantityCommand = new RelayCommand<object>(IncreaseQuantity);
            RemoveItemCommand = new RelayCommand<object>(RemoveItem);

            CartItems.CollectionChanged += (_, __) =>
            {
                OnPropertyChanged(nameof(TotalCartPrice));
                (PayCommand as RelayCommand<object>)?.RaiseCanExecuteChanged();
                (ClearCartCommand as RelayCommand<object>)?.RaiseCanExecuteChanged();
            };

            foreach (var item in CartItems)
            {
                HookItem(item);
            }

            CartItems.CollectionChanged += (_, e) =>
            {
                if (e.NewItems != null)
                    foreach (CartItem item in e.NewItems)
                        HookItem(item);
            };
        }

        // Methods
        private void HookItem(CartItem item)
        {
            item.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(CartItem.Quantity))
                {
                    OnPropertyChanged(nameof(TotalCartPrice));
                }
            };
        }

        private void Pay(object _)
        {
            _dialogService.ShowPaymentDialog();
        }

        private bool CanPay(object _)
        {
            return CartItems.Any();
        }

        private void ClearCart(object _)
        {
            if (CartItems.Count > 0)
            {
                if (_dialogService.ShowConfirmation("장바구니를 비우시겠습니까?", "비우기 확인"))
                {
                    CartItems.Clear();
                }
            }
        }

        private bool CanClearCart(object _)
        {
            return CartItems.Any();
        }

        private void DecreaseQuantity(object parameter)
        {
            if (parameter is CartItem item && item.Quantity > 1)
            {
                item.Quantity--;
            }
        }

        private void IncreaseQuantity(object parameter)
        {
            if (parameter is CartItem item)
            {
                item.Quantity++;
            }
        }

        private void RemoveItem(object parameter)
        {
            if (parameter is CartItem item)
            {
                CartItems.Remove(item);
            }
        }
    }
}