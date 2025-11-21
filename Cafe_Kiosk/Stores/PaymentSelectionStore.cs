using Cafe_Kiosk.Models;
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
        public CardInfo Card { get; private set; } = new();

        public void SetCardPayment(string number, string expiry, string cvc)
        {
            SelectedMethod = PaymentMethod.Card;
            Card.Number = number;
            Card.Expiry = expiry;
            Card.CVC = cvc;
        }

        public void SetCashPayment()
        {
            SelectedMethod = PaymentMethod.Cash;
            Card = new CardInfo();
        }

        public void SetMobilePayPayment()
        {
            SelectedMethod = PaymentMethod.MobilePay;
            Card = new CardInfo();
        }
    }
}