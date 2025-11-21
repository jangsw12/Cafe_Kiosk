using Cafe_Kiosk.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Cafe_Kiosk.Services.API
{
    public class PaymentApiService : IPaymentApiService
    {
        private readonly HttpClient _httpClient;

        public PaymentApiService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7148/")
            };
        }

        private async Task<PaymentResponse> PostPaymentAsync(string url, PaymentRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, request);

                if (!response.IsSuccessStatusCode)
                {
                    return new PaymentResponse
                    {
                        IsSuccess = false,
                        Message = $"HTTP {response.StatusCode} 응답"
                    };
                }

                var result = await response.Content.ReadFromJsonAsync<PaymentResponse>();

                return result ?? new PaymentResponse
                {
                    IsSuccess = false,
                    Message = "결제 응답 파싱 실패"
                };
            }
            catch (Exception ex)
            {
                return new PaymentResponse
                {
                    IsSuccess = false,
                    Message = $"결제 요청 중 오류: {ex.Message}"
                };
            }
        }
        
        public Task<PaymentResponse> RequestCardPaymentAsync(PaymentRequest request)
        {
            return PostPaymentAsync("api/payment/card", request);
        }

        public Task<PaymentResponse> RequestCashPaymentAsync(PaymentRequest request)
        {
            return PostPaymentAsync("api/payment/cash", request);
        }

        public Task<PaymentResponse> RequestMobilePaymentAsync(PaymentRequest request)
        {
            return PostPaymentAsync("api/payment/mobile", request);
        }
    }
}