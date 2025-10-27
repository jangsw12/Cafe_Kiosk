using System;
﻿using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Services.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cafe_Kiosk.Models.Enums;
using Cafe_Kiosk.Models;
using System.Collections.ObjectModel;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Stores;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentWaitingViewModel : ViewModelBase
    {
        // Properties
        private readonly IPaymentFlowManager _paymentFlowManager;
        private readonly ICartService _cartService;
        private readonly PaymentSelectionStore _paymentSelectionStore;
        private CancellationTokenSource _cts;

        public PaymentMethod SelectedMethod => _paymentSelectionStore.SelectedMethod;

        public bool IsCardSelected => SelectedMethod == PaymentMethod.Card;
        public bool IsCashSelected => SelectedMethod == PaymentMethod.Cash;
        public bool IsMobilePaySelected => SelectedMethod == PaymentMethod.MobilePay;

        public ObservableCollection<CartItem> CartItems => _cartService.GetItems();
        public int TotalCartPrice => CartItems.Sum(item => item.TotalPrice);

        private int _remainingSeconds;

        public int RemainingSeconds
        {
            get { return _remainingSeconds; }
            set
            {
                if (_remainingSeconds != value)
                {
                    _remainingSeconds = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(RemainingTimeText));
                }
            }
        }

        public string RemainingTimeText => $"남은 시간 : {RemainingSeconds}초";

        public string CardNumber => _paymentSelectionStore.CardNumber;
        public string CardExpiry => _paymentSelectionStore.CardExpiry;
        public string CardCVC => _paymentSelectionStore.CardCVC;

        public string CashInfoText => $"총 {TotalCartPrice}원을 투입해주세요.";

        // Commands
        public ICommand CancelCommand { get; set; }
        public ICommand ProceedPaymentCommand { get; set; }

        // Constructor
        public PaymentWaitingViewModel(IPaymentFlowManager paymentFlowManager, ICartService cartService, 
                                       PaymentSelectionStore paymentSelectionStore)
        {
            _paymentFlowManager = paymentFlowManager;
            _cartService = cartService;
            _paymentSelectionStore = paymentSelectionStore;
            
            CancelCommand = new RelayCommand<object>(Cancel);
            ProceedPaymentCommand = new RelayCommand<object>(ProceedPayment);

             StartWaiting();
        }

        // Methods
        private void Cancel(object _)
        {
            _cts?.Cancel();
            _paymentFlowManager.GoToPrevious();
        }

        private void ProceedPayment(object _)
        {
            _cts?.Cancel();
            _paymentFlowManager.GoToNext();
        }

        private async void StartWaiting()
        {
            _cts = new CancellationTokenSource();
            RemainingSeconds = 30;

            try
            {
                while (RemainingSeconds > 0)
                {
                    await Task.Delay(1000, _cts.Token);
                    RemainingSeconds--;
                }

                if (!_cts.Token.IsCancellationRequested)
                    AutoCancel();
            }
            catch (TaskCanceledException)
            {
                // 대기 타이머 취소
            }
        }

        private void AutoCancel()
        {
            // 결제 시간 초과
            System.Windows.MessageBox.Show("결제 시간이 초과되었습니다. 결제를 다시 시도해주세요.");
            _paymentFlowManager.GoToPrevious();
        }
    }
}