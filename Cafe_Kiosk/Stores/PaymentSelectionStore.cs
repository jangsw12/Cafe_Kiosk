using Cafe_Kiosk.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Stores
{
    public class PaymentSelectionStore
    {
        public PaymentMethod SelectedMethod { get; private set; } = PaymentMethod.None;

        public string CardNumber { get; private set; } = string.Empty;
        public string CardExpiry { get; private set; } = string.Empty;
        public string CardCVC { get; private set; } = string.Empty;
    
        public void SetCardPayment(string cardNumber, string expiry, string cvc)
        {
            SelectedMethod = PaymentMethod.Card;
            CardNumber = cardNumber;
            CardExpiry = expiry;
            CardCVC = cvc;
        }

        public void SetCashPayment()
        {
            SelectedMethod = PaymentMethod.Cash;
            CardNumber = CardExpiry = CardCVC = string.Empty;
        }

        public void SetMobilePayPayment()
        {
            SelectedMethod = PaymentMethod.MobilePay;
            CardNumber = CardExpiry = CardCVC = string.Empty;
        }
    }
}