using KitchenEquipmentManagement.Frontend.WPF.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using KitchenEquipmentManagement.Frontend.WPF.Helpers;

namespace KitchenEquipmentManagement.Frontend.WPF.ApiServices
{
    public class AuthService : ApiService
    {
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("api/authentication/login", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return new AuthResponse
                {
                    ErrorMessage = responseBody.Trim('"')
                };
            }

            var result = JsonSerializer.Deserialize<LoginResult>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            TokenStorage.Token = result.token;
            TokenStorage.UserType = result.userType;
            SetToken(result.token);

            return new AuthResponse
            {
                Token = result.token,
                UserType = result.userType
            };
        }

        public async Task<string> SignupAsync(SignupRequest request)
        {
            var response = await PostAsync<SignupRequest, string>("api/authentication/signup", request);
            return response;
        }

        public async Task SignoutAsync()
        {
            if (!string.IsNullOrEmpty(TokenStorage.Token))
            {
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", TokenStorage.Token);

                await _client.PostAsync("api/authentication/logout", null);
            }

            TokenStorage.Clear();
            SetToken(null);
        }
    }
}
