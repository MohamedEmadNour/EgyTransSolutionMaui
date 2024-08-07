using EgyTrans.OwenSystem.DBContext.Entites;
using EgyTrans.OwenSystem.SystemOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace EgyTrans.OwenSystem.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiClient(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task PushChangeAsync<T>(T entity) where T : class, IIdentifiable
        {
            var entityType = typeof(T).Name.ToLower(); // Assumes entity names are used in API endpoints
            var url = $"{_baseUrl}/api/{entityType}";

            var response = await _httpClient.PostAsJsonAsync(url, entity);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error (e.g., log the error, throw an exception, etc.)
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to push change for {entityType}: {content}");
            }
        }

        public async Task<List<T>> GetNewDataAsync<T>() where T : class, IIdentifiable
        {
            var entityType = typeof(T).Name.ToLower();
            var url = $"{_baseUrl}/api/{entityType}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to get new data for {entityType}: {content}");
            }

            var newData = await response.Content.ReadFromJsonAsync<List<T>>();
            return newData ?? new List<T>();
        }
    }

}
