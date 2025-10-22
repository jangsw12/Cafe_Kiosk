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

        private string _waitingMessage;

        public string WaitingMessage
        {
            get { return _waitingMessage; }
            set { 
                _waitingMessage = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand GoBackCommand { get; set; }

        // Constructor
        public PaymentWaitingViewModel(IPaymentFlowManager paymentFlowManager)
        {
            _paymentFlowManager = paymentFlowManager;

            GoBackCommand = new RelayCommand<object>(GoBack);

            //WaitingMessage = GetMessageForPaymentType();

            StartWaiting();
        }

        // Methods
        private void GoBack(object _)
        {
            _paymentFlowManager.GoToPrevious();
        }

        private void GetMessageForPaymentType()
        {

        }

        private async void StartWaiting()
        {
            // 추후 실제 상태 감지 로직으로 대체
            await Task.Delay(3000);

            _paymentFlowManager.GoToNext();
        }
    }
}