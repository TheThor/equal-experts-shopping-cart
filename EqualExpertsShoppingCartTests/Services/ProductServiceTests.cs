using EqualExpertsShoppingCart.Models.DTOs;
using EqualExpertsShoppingCart.Repositories;
using EqualExpertsShoppingCart.Services;
using Moq;

namespace EqualExpertsShoppingCartTests.Services
{
    [TestClass]
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductService ProductService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            ProductService = new ProductService(_mockProductRepository.Object);
        }


        /**
         * Start with the basic
         **/
        [TestMethod]
        public async Task GetProductBySlug_WhenSlugIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            string slug = null;
            var productTask = ProductService.GetProductBySlugAsync(slug);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async () => await productTask
            );
        }

        // Invalid DTO
        [TestMethod]
        public async Task GetProductBySlug_WhenProductDtoIsNull_ThrowsInvalidOperationException()
        {
            // Arrange
            var slug = "existing-slug";
            ProductDto productDto = null;
            _mockProductRepository
                .Setup(repo => repo.GetProductBySlug(slug))
                .ReturnsAsync(productDto);
            var productTask = ProductService.GetProductBySlugAsync(slug);

            // Act and Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                async () =>
                {
                    var product = await productTask;
                }
            );
        }

        // Valid Dto
        [TestMethod]
        public async Task GetProductBySlug_WhenProductDtoIsValid_ReturnsProduct()
        {
            // Arrange
            var slug = "existing-slug";
            var productDto = new ProductDto { Title = "Product Title", Price = 10.0m };
            _mockProductRepository
                .Setup(repo => repo.GetProductBySlug(slug))
                .ReturnsAsync(productDto);


            // Act
            var productTask = ProductService.GetProductBySlugAsync(slug);
            var product = await productTask;

            // Assert
            Assert.IsNotNull(product);
            Assert.AreEqual("Product Title", product.Title);
            Assert.AreEqual(10.0m, product.ProductPrice.Amount);

        }

        // Test different slugs
        [TestMethod]
        public async Task GetProductBySlugAsync_WhenSlugIsInvalid_ShouldThrowException()
        {
            // Arrange
            var slug = "non-existing";

            // Act
            var productTask = ProductService.GetProductBySlugAsync(slug);

            // Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(
                async () =>
                {
                    var product = await productTask;
                }
            );
        }

        // valid slug now
        [TestMethod]
        public async Task GetProductBySlugAsync_WhenSlugIsValid_ShouldReturnAProductWithValues()
        {
            // Arrange
            var slug = "cheerios";
            var expectedProductDto = new ProductDto
            {
                Title = "Cheerios",
                Price = 8.43m
            };
            _mockProductRepository
                .Setup(repo => repo.GetProductBySlug(slug))
                .ReturnsAsync(expectedProductDto);

            // Act
            // Invoke the service method
            var productTask = ProductService.GetProductBySlugAsync(slug);
            var product = await productTask;

            // Assert
            // Validate if product is returned and if the properties are set
            Assert.IsNotNull(product);
            Assert.AreEqual(product.Title, "Cheerios");
            Assert.IsNotNull(product.ProductPrice);
        }

        //Empty slug
        [TestMethod]
        public async Task GetProductBySlugAsync_WhenSlugIsEmpty_ShouldThrowException()
        {
            // Arrange
            var slug = "";

            // Act
            var productTask = ProductService.GetProductBySlugAsync(slug);

            // Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(
                async () =>
                {
                    var product = await productTask;
                }
            );
        }
    }
}
