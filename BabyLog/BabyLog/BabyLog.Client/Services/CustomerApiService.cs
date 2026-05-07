using BabyLog.Client.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BabyLog.Client.Services
{
    public class CustomerApiService
    {
        private readonly HttpClient _httpClient;
        private readonly TokenStorageService _tokenStorage;

        public CustomerApiService(
            HttpClient httpClient,
            TokenStorageService tokenStorage)
        {
            _httpClient = httpClient;
            _tokenStorage = tokenStorage;
        }

        // Gets current logged-in customer from BabyFællesskab API
        public async Task<CustomerInfo?> GetCurrentCustomerAsync()
        {
            // No token found
            if (string.IsNullOrWhiteSpace(_tokenStorage.Token))
                return null;

            // Add JWT token to Authorization header
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _tokenStorage.Token
                );

            // Call BabyFællesskab API
            return await _httpClient
                .GetFromJsonAsync<CustomerInfo>("api/customer/me");
        }
    }
}