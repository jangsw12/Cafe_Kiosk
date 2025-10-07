using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Services.Dialog;
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
        private readonly IDialogService _dialogService;

        public ObservableCollection<CartItem> CartItems => _cartService.GetItems();

        public int TotalCartPrice => CartItems.Sum(item => item.TotalPrice);

        // Commands
        public ICommand PayCommand { get; set; }
        public ICommand ClearCartCommand { get; set; }

        // Constructor
        public CartViewModel(ICartService cartService, IDialogService dialogService)
        {
            _cartService = cartService;
            _dialogService = dialogService;

            PayCommand = new RelayCommand<object>(Pay);
            ClearCartCommand = new RelayCommand<object>(ClearCart);

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
            if (CartItems.Count > 0)
            {
                if (_dialogService.ShowConfirmation("결제를 진행하시겠습니까?","결제 확인"))
                {
                    // 결제 처리 로직
                    MessageBox.Show("결제가 완료되었습니다", "결제 완료", MessageBoxButton.OK, MessageBoxImage.Information);
                    CartItems.Clear();
                }
            }
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
    }
}