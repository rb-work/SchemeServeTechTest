using Newtonsoft.Json;
using SchemeServeTest.Core.Models;
using System.Text.Json;

namespace SchemeServeTest.Core.Services
{
    public interface ICqcApiService
    {
        Task<List<ProviderBasicInfoDto>> GetProvidersAsync();
        Task<ProviderDto> GetProviderAsync(string providerId);
    }

    public class CqcApiService : ICqcApiService
    {
        private readonly HttpClient _httpClient;
        public CqcApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://api.cqc.org.uk/public/v1/providers");
        }

        public async Task<List<ProviderBasicInfoDto>> GetProvidersAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var providersOverviewJson = JsonConvert.DeserializeObject<dynamic>(content);
                    var providersJson = providersOverviewJson.providers.ToString();
                    var providers = JsonConvert.DeserializeObject<List<ProviderBasicInfoDto>>(providersJson);

                    return providers;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"An error occurred while calling the API: {ex.Message}", ex);
            }
        }

        public async Task<ProviderDto> GetProviderAsync(string providerId)
        {
            if (string.IsNullOrEmpty(providerId))
            {
                return null;
            }

            try
            {
                var response = await _httpClient.GetAsync($"providers/{providerId}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var providerDto = System.Text.Json.JsonSerializer.Deserialize<ProviderDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return providerDto;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"An error occurred while calling the API: {ex.Message}", ex);
            }
        }
    }
}
