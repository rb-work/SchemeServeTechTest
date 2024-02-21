using Microsoft.AspNetCore.Mvc;
using SchemeServeTest.Core.Services;

namespace SchemeServeTest.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProviderController : ControllerBase
    {
        private IDatabaseService _dbService { get; set; }
        private readonly ICqcApiService _cqcApiService;

        public ProviderController(IDatabaseService dbService, ICqcApiService cqcApiService)
        {
            _dbService = dbService;
            _cqcApiService = cqcApiService;
        }

        [HttpGet]
        public async Task<ActionResult> GetProviders()
        {
            try
            {
                var providerBasicInfo = await _cqcApiService.GetProvidersAsync();
                return Ok(providerBasicInfo);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"An error occurred while calling the API: {ex.Message}", ex);
            }
        }

        [HttpGet("{providerId}")]
        public async Task<ActionResult> GetProvider(string providerId)
        {
            try
            {
                if (string.IsNullOrEmpty(providerId))
                {
                    return NotFound(new { Message = "Please add a ProviderId to your request" });
                }

                var providerDto = await _dbService.GetProviderAsync(providerId);

                if (providerDto != null)
                {
                    return Ok(providerDto);
                }
                else
                {
                    providerDto = await _cqcApiService.GetProviderAsync(providerId);

                    if (providerDto != null)
                    {
                        await _dbService.AddProviderAsync(providerDto);
                        return Ok(providerDto);
                    }
                }
            }
            catch(HttpRequestException ex)
            {
                throw new Exception($"An error occurred while calling the API: {ex.Message}", ex);
            }
            
            return NotFound(new { Message = "No Provider found on the given Provider ID" });
        }
    }
}
