using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Services.Navi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class ReceiptOptionViewModel : ViewModelBase
    {
        // Properties
        private readonly INavigationService _navigationService;

        // Commands
        public ICommand GoBackCommand { get; set; }
        public ICommand ProceedPaymentCommand { get; set; }

        // Constructor
        public ReceiptOptionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBackCommand = new RelayCommand<object>(GoBack);
            ProceedPaymentCommand = new RelayCommand<object>(ProceedPayment);
        }

        // Methods
        private void GoBack(object _)
        {
            _navigationService.Navigate(NaviType.PaymentResultView);
        }

        private void ProceedPayment(object _)
        {
            // 최종 결제 로직
            MessageBox.Show("결제 완료!");
        }
    }
}