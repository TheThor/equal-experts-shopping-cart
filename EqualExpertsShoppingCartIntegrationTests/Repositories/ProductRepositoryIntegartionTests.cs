using EqualExpertsShoppingCart.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EqualExpertsShoppingCartIntegrationTests.Repositories
{
    [TestClass]
    public class ProductRepositoryIntegrationTests : IntegrationTestBase
    {
        private IProductRepository _productRepository;

        [TestInitialize]
        public void Initialize()
        {
            _productRepository = ServiceProvider.GetRequiredService<IProductRepository>();
        }

        [TestMethod]
        public async Task GetProductBySlug_WhenSlugIsValid_ReturnsProduct()
        {
            // Arrange
            string slug = "cheerios";

            // Act
            var productDto = await _productRepository.GetProductBySlug(slug);

            // Assert
            Assert.IsNotNull(productDto);
            Assert.AreEqual("Cheerios", productDto.Title);
            Assert.AreEqual(8.43m, productDto.Price);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException))]
        public async Task GetProductBySlug_WhenSlugDoesNotExist_ReturnsNull()
        {
            // Arrange
            string slug = "non-existent-slug";

            // Act
            var productDto = await _productRepository.GetProductBySlug(slug);
        }
    }
}
