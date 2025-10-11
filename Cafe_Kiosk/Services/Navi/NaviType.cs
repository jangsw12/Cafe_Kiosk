using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Services.Navi
{
    public enum NaviType
    {
        PaymentStartView,
        PaymentMethodView,
        PaymentProcessingView,
        PaymentWaitingView,
        PaymentResultView,
        ReceiptOptionView
    }
}