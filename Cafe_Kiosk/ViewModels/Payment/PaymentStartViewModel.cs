using System;
﻿using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Models;
using Cafe_Kiosk.Services.Cart;
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
using System.Windows.Navigation;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentStartViewModel : ViewModelBase
    {
        // Properties
        private readonly IPaymentFlowManager _paymentFlowManager;
        private readonly IDialogService _dialogService;
        private readonly ICartService _cartService;

        public ObservableCollection<CartItem> CartItems => _cartService.GetItems();

        public int TotalCartPrice => CartItems.Sum(item => item.TotalPrice);

        // Commands
        public ICommand GoBackCommand { get; set; }
        public ICommand ProceedPaymentCommand { get; set; }

        // Constructor
        public PaymentStartViewModel(IPaymentFlowManager paymentFlowManager, IDialogService dialogService, ICartService cartService)
        {
            _paymentFlowManager = paymentFlowManager;
            _dialogService = dialogService;
            _cartService = cartService;

            GoBackCommand = new RelayCommand<object>(GoBack);
            ProceedPaymentCommand = new RelayCommand<object>(ProceedPayment);
        }

        // Methods
        private void GoBack(object _)
        {
            _dialogService.ClosePaymentDialog();
        }

        private void ProceedPayment(object _)
        {
            _paymentFlowManager.GoToNext();
        }
    }
}