using EqualExpertsShoppingCart.Repositories;
using EqualExpertsShoppingCart.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EqualExpertsShoppingCartIntegrationTests.Services
{
    [TestClass]
    public class CartServiceIntegrationTests : IntegrationTestBase
    {
        private CartService _cartService;
        private IProductService _productService;
        private ICartRepository _cartRepository;

        [TestInitialize]
        public void Initialize()
        {
            // Retrieve the necessary dependencies from the service provider
            _productService = ServiceProvider.GetRequiredService<IProductService>();
            _cartRepository = ServiceProvider.GetRequiredService<ICartRepository>();

            // Create the CartService instance with the dependencies
            _cartService = new CartService(_productService, _cartRepository);
        }

        /**
         * Test method to try and do the inputs from EE test:
         * 
         * The below is a sample with the correct values you can use to confirm your calculations
         *
         * Inputs
         * Add 1 × cornflakes @ 2.52 each
         * Add another 1 x cornflakes @2.52 each
         * Add 1 × weetabix @ 9.98 each
         *
         * Results
         * Cart contains 2 x cornflakes
         * Cart contains 1 x weetabix
         * Subtotal = 15.02
         * Tax = 1.88
         * Total = 16.90
         *
         **/
        [TestMethod]
        public async Task AddProductToCart_EEScenario_AddsProductToCartAndPrintsState()
        {
            // Arrange
            string cornflakesSlug = "cornflakes";
            int quantity = 1;

            // Act
            await _cartService.AddProductToCart(cornflakesSlug, quantity);
            await _cartService.AddProductToCart(cornflakesSlug, quantity);

            string weetabixSlug = "weetabix";
            await _cartService.AddProductToCart(weetabixSlug, quantity);

            // Assert
            var cartItems = _cartService.GetCart().Items;
            var cart = _cartService.GetCart();

            // Has two differemt products
            Assert.AreEqual(2, cartItems.Count);

            // Cornflakes
            // The first product in the list is cornflakes
            Assert.AreEqual("Corn Flakes", cartItems[0].Product.Title);
            // The quantity is 2 for cornflakes
            Assert.AreEqual(2, cartItems[0].Quantity);
            // The Total value for these two products is
            Assert.AreEqual(5.04m, cartItems[0].TotalPrice);

            // Weetabix
            // The second product in the list is weetabix
            Assert.AreEqual("Weetabix", cartItems[1].Product.Title);
            // The quantity is 2 for cornflakes
            Assert.AreEqual(1, cartItems[1].Quantity);
            // The Total value for these two products is
            Assert.AreEqual(5.04m, cartItems[0].TotalPrice);

            //Cart State Validation
            Assert.AreEqual(15.02m, cart.Subtotal);
            Assert.AreEqual(1.88m, cart.Tax);
            Assert.AreEqual(16.90m, cart.Total);

            // Output for exercise purpose
            Console.WriteLine("Your Shopping Cart:");
            Console.WriteLine("Title ----------- Quantiy ---------- Total Price");
            Console.WriteLine("------------------------------------------------");
            foreach (var item in cartItems)
            {
                Console.WriteLine("{0,-10} {1,11} {2,20}", item.Product.Title, item.Quantity, item.Product.ProductPrice.Amount);
            }
            Console.WriteLine("Subtotal: {0}", cart.Subtotal);
            Console.WriteLine("Tax: {0}", cart.Tax);
            Console.WriteLine("Total: {0}", cart.Total);
        }

        [TestMethod]
        public async Task AddProductToCart_ValidProduct_AddsProductToCart()
        {
            // Arrange
            string productSlug = "cheerios";
            string expectedTitle = "Cheerios";
            int quantity = 2;

            // Act
            await _cartService.AddProductToCart(productSlug, quantity);

            // Assert
            var cartItems = _cartService.GetCart().Items;
            Assert.AreEqual(1, cartItems.Count);
            Assert.AreEqual(expectedTitle, cartItems[0].Product.Title);
            Assert.AreEqual(quantity, cartItems[0].Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpRequestException), "No product found or some details missing.")]
        public async Task AddProductToCart_InvalidProduct_ThrowsException()
        {
            // Arrange
            string productSlug = "invalid-product";
            int quantity = 2;
            var expectedCart = _cartService.GetCart();

            // Act
            await _cartService.AddProductToCart(productSlug, quantity);
            var cartItems = _cartService.GetCart().Items;
        }
    }
}
