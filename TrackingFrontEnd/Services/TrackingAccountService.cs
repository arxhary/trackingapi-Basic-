using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using TrackingFrontEnd.Models;

namespace TrackingFrontEnd.Services

{
    public class TrackingAccountService : ITrackingAccountService
    {
        HttpClient _httpClient;
        public TrackingAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> GetToken(string clientId, string clientSecret)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "issue");

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));

            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type","client-credentials" }
            });

            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            var authResult = await JsonSerializer.DeserializeAsync<AuthResult>(responseStream);

            return authResult.access_token;
        }

    }
}
