using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Models.Payment
{
    public class PaymentResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;

        public string? ApprovalCode { get; set; }
        public string? TransactionId { get; set; }
        public DateTime? PaidAt { get; set; }

        public object? Data { get; set; }
    }
}