using BabyLog.Client.Models;
using System.Net.Http.Json;

namespace BabyLog.Client.Services
{
    public class SleepApiService
    {
        private readonly HttpClient _httpClient;

        public SleepApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Gets all sleep registrations for one child
        public async Task<List<Sleep>> GetSleepByChildAsync(int childId)
        {
            return await _httpClient.GetFromJsonAsync<List<Sleep>>(
                $"api/sleep/child/{childId}"
            ) ?? new List<Sleep>();
        }

        // Gets one sleep registration
        public async Task<Sleep?> GetSleepAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Sleep>(
                $"api/sleep/{id}"
            );
        }

        // Creates a new sleep registration
        public async Task CreateSleepAsync(Sleep sleep)
        {
            await _httpClient.PostAsJsonAsync("api/sleep", sleep);
        }

        // Updates an existing sleep registration
        public async Task UpdateSleepAsync(Sleep sleep)
        {
            await _httpClient.PutAsJsonAsync(
                $"api/sleep/{sleep.SleepId}",
                sleep
            );
        }

        // Deletes a sleep registration
        public async Task DeleteSleepAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/sleep/{id}");
        }
    }
}