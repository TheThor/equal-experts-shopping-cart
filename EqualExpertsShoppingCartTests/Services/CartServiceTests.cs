using EqualExpertsShoppingCart.Models;
using EqualExpertsShoppingCart.Repositories;
using EqualExpertsShoppingCart.Services;
using Moq;

namespace EqualExpertsShoppingCartTests.Services
{
    [TestClass]
    public class CartServiceTests
    {

        private readonly Mock<ICartRepository> _mockCartRepository;
        private readonly Mock<IProductService> _mockProductService;
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _mockCartRepository = new Mock<ICartRepository>();
            _mockProductService = new Mock<IProductService>();
            _cartService = new CartService(_mockProductService.Object, _mockCartRepository.Object);
        }

        [TestMethod]
        public async Task AddProductToCart_WhenSlugAndQuantityEmpty_ThrowsException()
        {
            // Arrange
            var slug = "";
            var quantity = 0;
            _mockProductService
                .Setup(service => service.GetProductBySlugAsync(slug))
                .Throws(new ArgumentNullException(nameof(slug), "Slug cannot be null or empty."));

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _cartService.AddProductToCart(slug, quantity));
        }

        [TestMethod]
        public async Task AddProductToCart_WhenSlugIsCheeriosAndQuantityEmpty_ThrowsException()
        {
            // Arrange
            var slug = "cheerios";
            var quantity = 0;
            // Create a product with just the title
            var product = new Product("Cheerios", 3.0m);

            // Mocking the GetProductBySlugAsync method to return null
            _mockProductService
                .Setup(service => service.GetProductBySlugAsync(slug))
                .ReturnsAsync(product);
            _mockCartRepository
                .Setup(repo => repo.AddItemByQuantity(product, quantity))
                .Throws(new ArgumentException("Quantity must be greater than or equal to 1."));

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => _cartService.AddProductToCart(slug, quantity));
        }

        [TestMethod]
        public async Task AddProductToCart_NonExistingProductAsSlug_ThrowsException()
        {
            // Arrange
            string slug = "nonexistent-product";
            int quantity = 1;

            _mockProductService.Setup(p => p.GetProductBySlugAsync(slug))
                .ReturnsAsync((Product)null); // Return null to simulate a product not found
            
            // Act and Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => _cartService.AddProductToCart(slug, quantity));
        }
    }
}
