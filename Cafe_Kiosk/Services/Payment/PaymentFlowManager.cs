using Cafe_Kiosk.Models;
using Cafe_Kiosk.Models.Enums;
using Cafe_Kiosk.Services.Cart;
using Cafe_Kiosk.Services.Navi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cafe_Kiosk.Services.Payment
{
    public class PaymentFlowManager : IPaymentFlowManager
    {
        // Properties
        private readonly INavigationService _navigationService;
        private readonly ICartService _cartService;
        private PaymentMethod _selectedMethod = PaymentMethod.None;
        private NaviType _current;

        // Constructor
        public PaymentFlowManager(INavigationService navigationService, ICartService cartService)
        {
            _navigationService = navigationService;
            _cartService = cartService;
            _current = NaviType.PaymentStartView;
        }

        // Methods
        public void SetSelectedMethod(PaymentMethod method) => _selectedMethod = method;
        public PaymentMethod GetSelectedMethod() => _selectedMethod;

        public void GoToNext()
        {
            _current = _current switch
            {
                NaviType.PaymentStartView => NaviType.PaymentMethodView,
                NaviType.PaymentMethodView => NaviType.PaymentProcessingView,
                NaviType.PaymentProcessingView => NaviType.PaymentWaitingView,
                NaviType.PaymentWaitingView => NaviType.PaymentResultView,
                NaviType.PaymentResultView => NaviType.ReceiptOptionView,
                _ => _current
            };

            _navigationService.Navigate(_current);
        }

        public void GoToPrevious()
        {
            _current = _current switch
            {
                NaviType.PaymentResultView => NaviType.PaymentWaitingView,
                NaviType.PaymentWaitingView => NaviType.PaymentMethodView,
                NaviType.PaymentMethodView => NaviType.PaymentStartView,
                _ => _current
            };

            _navigationService.Navigate(_current);
        }

        public void GoToStart()
        {
            _current = NaviType.PaymentStartView;
            _navigationService.Navigate(_current);
        }

        public void GoToResult(bool isSuccess)
        {
            _current = NaviType.PaymentResultView;
            _navigationService.Navigate(_current);
        }

        public void CompletePayment()
        {
            // 최종 결제 로직 (결제 성공 알림, 카트 비우기)
            _cartService.ClearCart();
            GoToStart();
        }
    }
}