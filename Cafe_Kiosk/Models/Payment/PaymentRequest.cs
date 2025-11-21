using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Models.Payment
{
    public class PaymentRequest
    {
        public string OrderId { get; set; } = string.Empty;
        public int Amount { get; set; }
        public string Method { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = new();
        public CardInfo? Card { get; set; }
    }
}