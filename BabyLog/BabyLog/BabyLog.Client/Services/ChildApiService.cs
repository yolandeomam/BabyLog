using BabyLog.Client.Models;
using System.Net.Http.Json;

namespace BabyLog.Client.Services
{
    public class ChildApiService
    {
        private readonly HttpClient _httpClient;

        public ChildApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Gets all children for one customer
        public async Task<List<Child>> GetChildrenByCustomerAsync(int customerId)
        {
            return await _httpClient
                .GetFromJsonAsync<List<Child>>(
                    $"api/child/customer/{customerId}"
                )
                ?? new List<Child>();
        }

        // Gets one child by ChildId
        public async Task<Child?> GetChildAsync(int id)
        {
            return await _httpClient
                .GetFromJsonAsync<Child>($"api/child/{id}");
        }

        // Creates a new child profile
        public async Task CreateChildAsync(Child child)
        {
            await _httpClient.PostAsJsonAsync("api/child", child);
        }

        // Updates an existing child profile
        public async Task UpdateChildAsync(Child child)
        {
            await _httpClient.PutAsJsonAsync(
                $"api/child/{child.ChildId}",
                child
            );
        }

        // Deletes a child profile
        public async Task DeleteChildAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/child/{id}");
        }
    }
}