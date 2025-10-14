using Cafe_Kiosk.Commands;
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

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class ReceiptOptionViewModel : ViewModelBase
    {
        // Properties
        private readonly IPaymentFlowManager _paymentFlowManager;
        private readonly IDialogService _dialogService;

        // Commands
        public ICommand GoBackCommand { get; set; }
        public ICommand ProceedPaymentCommand { get; set; }

        // Constructor
        public ReceiptOptionViewModel(IPaymentFlowManager paymentFlowManager, IDialogService dialogService)
        {
            _paymentFlowManager = paymentFlowManager;
            _dialogService = dialogService;

            GoBackCommand = new RelayCommand<object>(GoBack);
            ProceedPaymentCommand = new RelayCommand<object>(ProceedPayment);
        }

        // Methods
        private void GoBack(object _)
        {
            _paymentFlowManager.GoToPrevious();
        }

        private void ProceedPayment(object _)
        {
            _paymentFlowManager.CompletePayment();
            _dialogService.ClosePaymentDialog();
        }
    }
}