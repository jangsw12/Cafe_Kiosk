using System;
﻿using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Services.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentWaitingViewModel : ViewModelBase
    {
        // Properties
        private readonly IPaymentFlowManager _paymentFlowManager;
        private CancellationTokenSource _cts;

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

        // Commands
        public ICommand CancelCommand { get; set; }

        // Constructor
        public PaymentWaitingViewModel(IPaymentFlowManager paymentFlowManager)
        {
            _paymentFlowManager = paymentFlowManager;
            CancelCommand = new RelayCommand<object>(Cancel);

            StartWaiting();
        }

        // Methods
        private void Cancel(object _)
        {
            _cts?.Cancel();
            _paymentFlowManager.GoToPrevious();
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

                // 시간 지나면 자동 취소 처리
                if (!_cts.Token.IsCancellationRequested)
                {
                    AutoCancel();
                }
            }
            catch (TaskCanceledException)
            {
                // 취소 시 무시
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