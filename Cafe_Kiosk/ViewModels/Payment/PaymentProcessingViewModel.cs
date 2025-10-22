using Cafe_Kiosk.ViewModels;
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
    public class PaymentProcessingViewModel : ViewModelBase
    {
        // Properties
        private readonly IPaymentFlowManager _paymentFlowManager;

        // Constructor
        public PaymentProcessingViewModel(IPaymentFlowManager paymentFlowManager)
        {
            _paymentFlowManager = paymentFlowManager;

            StartProcessing();
        }

        // Methods
        private async void StartProcessing()
        {
            // 지연 시간(2초) >> 추후 API 작업으로 변경
            await Task.Delay(2000);

            _paymentFlowManager.GoToNext();
        }
    }
}