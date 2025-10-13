<<<<<<< HEAD
﻿using Cafe_Kiosk.ViewModels;
=======
﻿using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Services.Navi;
using Cafe_Kiosk.ViewModels;
>>>>>>> feature/payment
using System;
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
    public class PaymentProcessingViewModel : ViewModelBase
    {
<<<<<<< HEAD

=======
        // Properties
        private readonly INavigationService _navigationService;

        // Commands
        public ICommand GoBackCommand { get; set; }
        public ICommand ProceedPaymentCommand { get; set; }

        // Constructor
        public PaymentProcessingViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            GoBackCommand = new RelayCommand<object>(GoBack);
            ProceedPaymentCommand = new RelayCommand<object>(ProceedPayment);
        }

        // Methods
        private void GoBack(object _)
        {
            _navigationService.Navigate(NaviType.PaymentMethodView);
        }

        private void ProceedPayment(object _)
        {
            _navigationService.Navigate(NaviType.PaymentWaitingView);
        }
>>>>>>> feature/payment
    }
}