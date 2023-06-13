using EqualExpertsShoppingCart.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EqualExpertsShoppingCartIntegrationTests.Services
{
    [TestClass]
    public class ProductServiceIntegrationTests : IntegrationTestBase
    {
        private IProductService _productService;

        [TestInitialize]
        public void Initialize()
        {
            _productService = ServiceProvider.GetRequiredService<IProductService>();
        }

        [TestMethod]
        public async Task GetProductBySlugAsync_WhenSlugIsValid_ReturnsProduct()
        {
            // Arrange
            string slug = "cheerios";

            // Act
            var product = await _productService.GetProductBySlugAsync(slug);

            // Assert
            Assert.IsNotNull(product);
            Assert.AreEqual("Cheerios", product.Title);
            Assert.AreEqual(8.43m, product.ProductPrice.Amount);
        }

        [TestMethod]
        public async Task GetProductBySlugAsync_WhenSlugIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            string slug = null;

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _productService.GetProductBySlugAsync(slug));
        }

        [TestMethod]
        public async Task GetProductBySlugAsync_WhenSlugDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            string slug = "non-existent-slug";

            // Act and Assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => _productService.GetProductBySlugAsync(slug));
        }
    }
}
