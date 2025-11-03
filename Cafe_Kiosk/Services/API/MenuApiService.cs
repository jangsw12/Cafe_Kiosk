using Cafe_Kiosk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace Cafe_Kiosk.Services.API
{
    public class MenuApiService : IMenuApiService
    {
        private readonly HttpClient _httpClient;

        public MenuApiService()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:7148/")
            };
        }

        public async Task<List<CafeMenuItem>> GetCafeMenuAsync()
        {
            try
            {
                var menu = await _httpClient.GetFromJsonAsync<List<CafeMenuItem>>("api/CafeMenu");
                return menu ?? new List<CafeMenuItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API 호출 실패: {ex.Message}");
                return new List<CafeMenuItem>();
            }
        }
    }
}