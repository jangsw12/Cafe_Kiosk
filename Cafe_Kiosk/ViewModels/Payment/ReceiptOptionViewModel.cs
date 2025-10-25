using System;
﻿using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Services.Dialog;
using Cafe_Kiosk.Services.Payment;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class ReceiptOptionViewModel : ViewModelBase
    {
        // Properties
        private readonly IPaymentFlowManager _paymentFlowManager;
        private readonly IDialogService _dialogService;
        private readonly ICartService _cartService;
        private CancellationTokenSource _cts;

        public ObservableCollection<CartItem> CartItems => _cartService.GetItems();
        public int TotalCartPrice => CartItems.Sum(item => item.TotalPrice);

        private int _orderNumber = 1;

        public int OrderNumber
        {
            get { return _orderNumber; }
            set { 
                _orderNumber = value;
                OnPropertyChanged();
            }
        }

        private DateTime _paymentDate = DateTime.Now;

        public DateTime PaymentDate
        {
            get { return _paymentDate; }
            set {
                _paymentDate = value;
                OnPropertyChanged();
            }
        }

        private int _remainingSeconds = 10;

        public int RemainingSeconds
        {
            get { return _remainingSeconds; }
            set { 
                if (_remainingSeconds != value)
                {
                    _remainingSeconds = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RemainingTimeText));
                }
            }
        }

        public string RemainingTimeText => $"{RemainingSeconds}초 후 메인 화면으로 돌아갑니다.";

        // Commands
        public ICommand GoHomeCommand { get; set; }

        // Constructor
        public ReceiptOptionViewModel(IPaymentFlowManager paymentFlowManager, IDialogService dialogService,
                                      ICartService cartService)
        {
            _paymentFlowManager = paymentFlowManager;
            _dialogService = dialogService;
            _cartService = cartService;

            GoHomeCommand = new RelayCommand<object>(GoHome);

            OrderNumber = GetNextOrderNumber();

            StartAutoReturnCountdown();
        }

        // Methods
        private int GetNextOrderNumber()
        {
            if (!Application.Current.Properties.Contains("LastOrderNumber"))
                Application.Current.Properties["LastOrderNumber"] = 1;
            else
                Application.Current.Properties["LastOrderNumber"] = (int)Application.Current.Properties["LastOrderNumber"] + 1;

            return (int)Application.Current.Properties["LastOrderNumber"];
        }

        private void GoHome(object _)
        {
            _cts?.Cancel();
            _paymentFlowManager.CompletePayment();
            _dialogService.ClosePaymentDialog();
        }

        private async void StartAutoReturnCountdown()
        {
            _cts = new CancellationTokenSource();

            try
            {
                while (RemainingSeconds > 0)
                {
                    await Task.Delay(1000, _cts.Token);
                    RemainingSeconds--;
                }

                if (!_cts.Token.IsCancellationRequested)
                {
                    GoHome(null);
                }
            }
            catch (TaskCanceledException)
            {
                // 타이머 취소
            }
        }
    }
}