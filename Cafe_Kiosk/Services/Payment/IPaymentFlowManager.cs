using Cafe_Kiosk.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Services.Payment
{
    public interface IPaymentFlowManager
    {
        void SetSelectedMethod(PaymentMethod method);
        PaymentMethod GetSelectedMethod();
        void GoToNext();
        void GoToPrevious();
        void GoToStart();
        void GoToResult(bool isSuccess);
        void CompletePayment();
    }
}