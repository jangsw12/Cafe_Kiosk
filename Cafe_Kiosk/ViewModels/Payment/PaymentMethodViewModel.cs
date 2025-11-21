using System;
﻿using Cafe_Kiosk.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Cafe_Kiosk.Services.Payment;
using Cafe_Kiosk.Stores;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentMethodViewModel : ViewModelBase
    {
        // Properties
        private readonly IPaymentFlowManager _paymentFlowManager;
        private readonly PaymentSelectionStore _paymentSelectionStore;

        private bool _isCardSelected;

        public bool IsCardSelected
        {
            get { return _isCardSelected; }
            set
            {
                if (_isCardSelected != value)
                {
                    _isCardSelected = value;
                    if (value)
                    {
                        IsCashSelected = false;
                        IsMobilePaySelected = false;
                    }
                    OnPropertyChanged();
                    RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isCashSelected;

        public bool IsCashSelected
        {
            get { return _isCashSelected; }
            set
            {
                if (_isCashSelected != value)
                {
                    _isCashSelected = value;
                    if (value)
                    {
                        IsCardSelected = false;
                        IsMobilePaySelected = false;
                    }
                    OnPropertyChanged();
                    RaiseCanExecuteChanged();
                }
            }
        }

        private bool _isMobilePaySelected;

        public bool IsMobilePaySelected
        {
            get { return _isMobilePaySelected; }
            set
            {
                if (_isMobilePaySelected != value)
                {
                    _isMobilePaySelected = value;
                    if (value)
                    {
                        IsCardSelected = false;
                        IsCashSelected = false;
                    }
                    OnPropertyChanged();
                    RaiseCanExecuteChanged();
                }
            }
        }

        private string _cardNumber = string.Empty;

        public string CardNumber
        {
            get { return _cardNumber; }
            set
            {
                if (_cardNumber != value)
                {
                    _cardNumber = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsCardNumberInValid));
                    RaiseCanExecuteChanged();
                }
            }
        }

        private string _cardExpiry = string.Empty;

        public string CardExpiry
        {
            get { return _cardExpiry; }
            set
            {
                if (_cardExpiry != value)
                {
                    _cardExpiry = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsCardExpiryInValid));
                    RaiseCanExecuteChanged();
                }
            }
        }

        private string _cardCVC = string.Empty;

        public string CardCVC
        {
            get { return _cardCVC; }
            set
            {
                if (_cardCVC != value)
                {
                    _cardCVC = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsCardCVCInValid));
                    RaiseCanExecuteChanged();
                }
            }
        }

        public bool IsCardNumberInValid => string.IsNullOrWhiteSpace(CardNumber);
        public bool IsCardExpiryInValid => string.IsNullOrWhiteSpace(CardExpiry);
        public bool IsCardCVCInValid => string.IsNullOrWhiteSpace(CardCVC);

        // Commands
        public ICommand GoBackCommand { get; }
        public ICommand ProceedPaymentCommand { get; }

        // Constructor
        public PaymentMethodViewModel(IPaymentFlowManager paymentFlowManager, PaymentSelectionStore paymentSelectionStore)
        {
            _paymentFlowManager = paymentFlowManager;
            _paymentSelectionStore = paymentSelectionStore;

            GoBackCommand = new RelayCommand<object>(GoBack);
            ProceedPaymentCommand = new RelayCommand<object>(ProceedPayment, CanProceedPayment);
        }

        // Methods
        private void GoBack(object _)
        {
            _paymentFlowManager.GoToPrevious();
        }

        private void ProceedPayment(object _)
        {
            if (IsCardSelected)
            {
                _paymentSelectionStore.SetCardPayment(CardNumber, CardExpiry, CardCVC);
            }
            else if (IsCashSelected)
            {
                _paymentSelectionStore.SetCashPayment();
            }
            else if (IsMobilePaySelected)
            {
                _paymentSelectionStore.SetMobilePayPayment();
            }

            _paymentFlowManager.GoToNext();
        }

        private bool CanProceedPayment(object _)
        {
            if (IsCardSelected)
            {
                return !string.IsNullOrWhiteSpace(CardNumber)
                    && !string.IsNullOrWhiteSpace(CardExpiry)
                    && !string.IsNullOrWhiteSpace(CardCVC);
            }

            return IsCashSelected || IsMobilePaySelected;
        }

        private void RaiseCanExecuteChanged()
        {
            if (ProceedPaymentCommand is RelayCommand<object> cmd)
                cmd.RaiseCanExecuteChanged();
        }
    }
}