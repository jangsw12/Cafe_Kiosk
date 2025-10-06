using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
using CommunityToolkit.Mvvm.Input;
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

        public ObservableCollection<CartItem> CartItems => _cartService.GetItems();

        public int TotalCartPrice => CartItems.Sum(item => item.TotalPrice);

        // Commands
        public ICommand PayCommand { get; set; }
        public ICommand ClearCartCommand { get; set; }

        // Constructor
        public CartViewModel(ICartService cartService)
        {
            _cartService = cartService;

            PayCommand = new RelayCommand<object>(Pay, CanPay);
            ClearCartCommand = new RelayCommand<object>(ClearCart, CanClearCart);

            CartItems.CollectionChanged += (_, __) =>
            {
                OnPropertyChanged(nameof(TotalCartPrice));
                CommandManager.InvalidateRequerySuggested();
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
            // 결제 처리 로직
            MessageBox.Show("결제가 완료되었습니다");
            ClearCart(null);
        }

        private bool CanPay(object _) => CartItems.Any();
        
        private void ClearCart(object _)
        {
            CartItems.Clear();
        }

        private bool CanClearCart(object _) => CartItems.Any();
    }
}