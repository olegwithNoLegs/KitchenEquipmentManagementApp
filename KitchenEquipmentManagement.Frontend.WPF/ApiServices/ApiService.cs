using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace KitchenEquipmentManagement.Frontend.WPF.ApiServices
{
    public class ApiService
    {
        protected readonly HttpClient _client;

        public ApiService()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7022/") // Change if needed
            };
        }

        public void SetToken(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> PostAsync<TInput, T>(string endpoint, TInput data)
        {
            var response = await _client.PostAsJsonAsync(endpoint, data);
            if (response.IsSuccessStatusCode)
            {
                var contentType = response.Content.Headers.ContentType?.MediaType;

                if (contentType == "application/json")
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else if (typeof(T) == typeof(string))
                {
                    var rawString = await response.Content.ReadAsStringAsync();
                    return (T)(object)rawString;
                }
            }

            return default;
        }

        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var response = await _client.GetAsync(endpoint);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<T>();
            return default;
        }

        public async Task<T?> PutAsync<TInput, T>(string endpoint, TInput data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var mediaType = response.Content.Headers.ContentType?.MediaType;

                if (mediaType == "application/json")
                {
                    return await response.Content.ReadFromJsonAsync<T>();
                }
                else if (mediaType == "text/plain")
                {
                    var raw = await response.Content.ReadAsStringAsync();
                    if (typeof(T) == typeof(string))
                    {
                        return (T)(object)raw;
                    }
                }
            }

            return default;
        }


        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _client.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }
    }
}
