using Shared.RequestViewModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Consumer.ExternalServices
{
    public class FakeStoreService
    {
        string baseUrl = "https://fakestoreapi.com";

        private readonly HttpClient _httpClient;

        public FakeStoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> CreateproductRequest(CreateProductVM productVM)
        {
            
            var response = await _httpClient.PostAsJsonAsync($"{baseUrl}/products", productVM);
            return response;
        }
    }
}
