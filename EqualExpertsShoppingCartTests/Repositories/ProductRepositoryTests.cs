using EqualExpertsShoppingCart.Models.DTOs;
using EqualExpertsShoppingCart.Repositories;
using Moq;
using Moq.Protected;
using System.Net;
using System.Text;
using System.Text.Json;

namespace EqualExpertsShoppingCartTests.Repositories
{
    [TestClass]
    public class ProductRepositoryTests
    {
        private ProductRepository _productRepository;
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private HttpClient _httpClient;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _productRepository = new ProductRepository(_mockHttpClientFactory.Object);
        }

        [TestMethod]
        public async Task GetProductBySlug_WhenEndpointSlugIsNull_ThrowsArgumentException()
        {
            // Arrange
            string endpointSlug = null;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _productRepository.GetProductBySlug(endpointSlug));
        }

        [TestMethod]
        public async Task GetProductBySlug_WhenEndpointSlugIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            string endpointSlug = string.Empty;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _productRepository.GetProductBySlug(endpointSlug));
        }

        [TestMethod]
        public async Task GetProductBySlug_WhenValidSlug_ReturnsProductDto()
        {
            // Arrange
            var slug = "cheerios";
            var expectedProductDto = new ProductDto
            {
                Title = "Cheerios",
                Price = 8.43m
            };
            var expectedJson = JsonSerializer.Serialize(expectedProductDto);
            var expectedContent = new StringContent(expectedJson, Encoding.UTF8, "application/json");
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = expectedContent
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            var productDto = await _productRepository.GetProductBySlug(slug);

            // Assert
            Assert.IsNotNull(productDto);
            Assert.AreEqual(expectedProductDto.Title, productDto.Title);
            Assert.AreEqual(expectedProductDto.Price, productDto.Price);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetProductBySlug_WhenInvalidSlug_ThrowsException()
        {
            // Arrange
            var slug = "non-existing";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.NotFound);

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            var productDto = await _productRepository.GetProductBySlug(slug);
        }

        [TestMethod]
        [ExpectedException(typeof(JsonException))]
        public async Task GetProductBySlug_WhenInvalidJson_ThrowsException()
        {
            // Arrange
            var slug = "invalid-json";
            var invalidJson = "Invalid JSON";
            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(invalidJson)
            };

            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expectedResponse);

            // Act
            var productDto = await _productRepository.GetProductBySlug(slug);
        }
    }
}
