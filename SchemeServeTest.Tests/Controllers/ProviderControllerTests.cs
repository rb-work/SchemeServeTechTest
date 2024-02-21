using Microsoft.AspNetCore.Mvc;
using Moq;
using SchemeServeTest.API.Controllers;
using SchemeServeTest.Core.Models;
using SchemeServeTest.Core.Services;

namespace SchemeServeTest.Tests.Controllers
{
    public class ProviderControllerTests
    {
        private readonly ProviderController _providerController;
        private readonly Mock<ICqcApiService> _mockCqcApiService = new();
        private readonly Mock<IDatabaseService> _mockDbService = new();

        public ProviderControllerTests()
        {
            _providerController = new ProviderController(_mockDbService.Object, _mockCqcApiService.Object);
        }
        
        [Fact]
        public async Task GetProviders_ReturnsOkObjectResult_WithProviders()
        {
            // Arrange
            _mockCqcApiService.Setup(x => x.GetProvidersAsync()).ReturnsAsync(new List<ProviderBasicInfoDto> { new ProviderBasicInfoDto() });
            
            // Act
            var result = await _providerController.GetProviders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<List<ProviderBasicInfoDto>>(okResult.Value);
        }

        [Fact]
        public async Task GetProvider_ReturnsOkObjectResult_WhenProviderExistsInDb()
        {
            // Arrange
            var providerId = "1-1000205933";
            _mockDbService.Setup(x => x.GetProviderAsync(providerId)).ReturnsAsync(new ProviderDto { ProviderId = providerId });

            // Act
            var result = await _providerController.GetProvider(providerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var providerDto = Assert.IsType<ProviderDto>(okResult.Value);
            Assert.Equal(providerId, providerDto.ProviderId);
        }

        [Fact]
        public async Task GetProvider_ReturnsNotFound_WhenProviderNotInDbOrApi()
        {
            // Arrange
            var providerId = "none";
            _mockDbService.Setup(x => x.GetProviderAsync(providerId)).ReturnsAsync((ProviderDto)null);
            _mockCqcApiService.Setup(x => x.GetProviderAsync(providerId)).ReturnsAsync((ProviderDto)null);

            // Act
            var result = await _providerController.GetProvider(providerId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}