using System;
using Cafe_Kiosk.Services.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace Cafe_Kiosk.ViewModels.Payment
{
    public class PaymentResultViewModel : ViewModelBase
    {
        // Properties
        private readonly IPaymentFlowManager _paymentFlowManager;

        private bool _isSuccess = true;

        public bool IsSuccess
        {
            get { return _isSuccess; }
            set {
                _isSuccess = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ResultMessage));
                OnPropertyChanged(nameof(ResultColor));
                OnPropertyChanged(nameof(ResultIcon));
            }
        }

        public string ResultMessage => IsSuccess ? "결제가 완료되었습니다!" : "결제가 실패했습니다.";
        public Brush ResultColor => IsSuccess ? Brushes.Green : Brushes.Red;
        public string ResultIcon => IsSuccess ? "Solid_CheckCircle" : "Solid_TimesCircle";

        // Constructor
        public PaymentResultViewModel(IPaymentFlowManager paymentFlowManager)
        {
            _paymentFlowManager = paymentFlowManager;

            StartAutoNext();
        }

        // Methods
        private async void StartAutoNext()
        {
            await Task.Delay(2000);
            _paymentFlowManager.GoToNext();
        }
    }
}