using Cafe_Kiosk.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Services.API
{
    public interface IPaymentApiService
    {
        Task<PaymentResponse> RequestCardPaymentAsync(PaymentRequest request);
        Task<PaymentResponse> RequestCashPaymentAsync(PaymentRequest request);
        Task<PaymentResponse> RequestMobilePaymentAsync(PaymentRequest request);
    }
}