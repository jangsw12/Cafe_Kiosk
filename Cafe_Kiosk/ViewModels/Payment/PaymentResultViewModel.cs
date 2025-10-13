<<<<<<< HEAD
﻿using System;
=======
﻿using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Services.Navi;
using System;
>>>>>>> feature/payment
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
<<<<<<< HEAD
=======
using System.Windows.Input;
>>>>>>> feature/payment

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentResultViewModel : ViewModelBase
    {
<<<<<<< HEAD

=======
        // Properties
        private readonly INavigationService _navigationService;

        // Commands
        public ICommand GoBackCommand { get; set; }
        public ICommand ProceedPaymentCommand { get; set; }

        // Constructor
        public PaymentResultViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBackCommand = new RelayCommand<object>(GoBack);
            ProceedPaymentCommand = new RelayCommand<object>(ProceedPayment);
        }

        // Methods
        private void GoBack(object _)
        {
            _navigationService.Navigate(NaviType.PaymentWaitingView);
        }

        private void ProceedPayment(object _)
        {
            _navigationService.Navigate(NaviType.ReceiptOptionView);
        }
>>>>>>> feature/payment
    }
}