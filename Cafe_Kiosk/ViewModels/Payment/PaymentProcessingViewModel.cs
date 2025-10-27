using Cafe_Kiosk.ViewModels;
﻿using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Services.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Cafe_Kiosk.Models.Enums;
using Cafe_Kiosk.Stores;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentProcessingViewModel : ViewModelBase
    {
        // Properties
        private readonly IPaymentFlowManager _paymentFlowManager;
        private readonly PaymentSelectionStore _paymentSelectionStore;

        public PaymentMethod SelectedMethod => _paymentSelectionStore.SelectedMethod;

        // Constructor
        public PaymentProcessingViewModel(IPaymentFlowManager paymentFlowManager, PaymentSelectionStore paymentSelectionStore)
        {
            _paymentFlowManager = paymentFlowManager;
            _paymentSelectionStore = paymentSelectionStore;

            _ = ProcessPaymentAsync();
        }

        // Methods
        private async Task ProcessPaymentAsync()
        {
            try
            {
                switch (SelectedMethod)
                {
                    case PaymentMethod.Card:
                        // 카드 결제 처리 API 호출
                        await Task.Delay(2000);
                        break;
                    case PaymentMethod.Cash:
                        // 현금 결제 처리 API 호출
                        await Task.Delay(2000);
                        break;
                    case PaymentMethod.MobilePay:
                        // 모바일 결제 처리 API 호출
                        await Task.Delay(2000);
                        break;
                    default:
                        return;
                }

                _paymentFlowManager.GoToNext();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Payment processing error: {ex.Message}");
            }
        }
    }
}