using Cafe_Kiosk.Commands;
using Cafe_Kiosk.Services.Navi;
using System;
using System.Collections.Generic;
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
        private readonly INavigationService _navigationService;

        // Commands
        public ICommand GoBackCommand { get; set; }
        public ICommand ProceedPaymentCommand { get; set; }

        // Constructor
        public PaymentStartViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            
            GoBackCommand = new RelayCommand<object>(GoBack);
            ProceedPaymentCommand = new RelayCommand<object>(ProceedPayment);
        }

        // Methods
        private void GoBack(object _)
        {
            MessageBox.Show("이전 창으로");
        }

        private void ProceedPayment(object _)
        {
            _navigationService.Navigate(NaviType.PaymentMethodView);
        }
    }
}