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
using Cafe_Kiosk.Models.Payment;
using Cafe_Kiosk.Services.API;
using Cafe_Kiosk.Services.Cart;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentProcessingViewModel : ViewModelBase
    {
        // Services
        private readonly IPaymentFlowManager _paymentFlowManager;
        private readonly PaymentSelectionStore _paymentSelectionStore;
        private readonly IPaymentApiService _paymentApiService;
        private readonly ICartService _cartService;

        // Properties
        public bool IsProcessing { get; private set; } = true;
        public string StatusMessage { get; private set; } = "결제 진행 중...";

        // Constructor
        public PaymentProcessingViewModel(IPaymentFlowManager paymentFlowManager, PaymentSelectionStore paymentSelectionStore,
                                          IPaymentApiService paymentApiService, ICartService cartService)
        {
            _paymentFlowManager = paymentFlowManager;
            _paymentSelectionStore = paymentSelectionStore;
            _paymentApiService = paymentApiService;
            _cartService = cartService;

            _ = ProcessPaymentAsync();
        }

        // Methods
        private async Task ProcessPaymentAsync()
        {
            try
            {
                // PaymentRequest 구성
                var request = new PaymentRequest
                {
                    OrderId = Guid.NewGuid().ToString(),
                    Amount = _cartService.GetItems().Sum(x => x.TotalPrice),
                    Method = _paymentSelectionStore.SelectedMethod.ToString(),
                    Items = _cartService.GetItems().ToList(),
                };

                // 카드 결제 정보 포함
                if (_paymentSelectionStore.SelectedMethod == Models.Enums.PaymentMethod.Card)
                {
                    request.Card = _paymentSelectionStore.Card;
                }

                // 결제 API 호출
                PaymentResponse response = _paymentSelectionStore.SelectedMethod switch
                {
                    Models.Enums.PaymentMethod.Card => await _paymentApiService.RequestCardPaymentAsync(request),
                    Models.Enums.PaymentMethod.Cash => await _paymentApiService.RequestCashPaymentAsync(request),
                    Models.Enums.PaymentMethod.MobilePay => await _paymentApiService.RequestMobilePaymentAsync(request),
                    _ => new PaymentResponse { IsSuccess = false, Message = "결제 수단이 선택되지 않았습니다." }
                };

                // 처리 후 상태 업데이트
                StatusMessage = response.IsSuccess
                    ? "결제 완료"
                    : $"결제 실패: {response.Message}";

                OnPropertyChanged(nameof(StatusMessage));
            }
            catch (Exception ex)
            {
                StatusMessage = $"결제 중 오류 발생: {ex.Message}";
                OnPropertyChanged(nameof(StatusMessage));
            }
            finally
            {
                IsProcessing = false;
                OnPropertyChanged(nameof(IsProcessing));

                // 다음 화면 이동
                _paymentFlowManager.GoToNext();
            }
        }
    }
}