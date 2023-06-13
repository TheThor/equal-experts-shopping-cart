using EqualExpertsShoppingCart.Models;
using EqualExpertsShoppingCart.Repositories;
using Moq;

namespace EqualExpertsShoppingCartTests.Repositories
{
    [TestClass]
    public class CartRepositoryTests
    {

        private CartRepository _cartRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _cartRepository = new CartRepository();
        }

        //Start with the basic
        [TestMethod]
        public void GetCart_ReturnsInitializedCart()
        {
            // Act
            var cart = _cartRepository.GetCart();

            // Assert
            Assert.IsNotNull(cart);
            Assert.AreEqual(0, cart.Items.Count); // Assuming the cart is initially empty
        }

        [TestMethod]
        public void AddItemByQuantity_WhenValidProductAndQuantity_AddsItemToCart()
        {
            // Arrange
            var productMock = new Mock<Product>("Cheerios", 8.43m);
            var product = productMock.Object;
            var quantity = 2;

            // Act
            _cartRepository.AddItemByQuantity(product, quantity);
            var cart = _cartRepository.GetCart();

            // Assert
            Assert.AreEqual(1, cart.Items.Count);
            var cartItem = cart.Items.First();
            Assert.AreEqual(product, cartItem.Product);
            Assert.AreEqual(quantity, cartItem.Quantity);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddItemByQuantity_WhenInvalidQuantity_ThrowsArgumentException()
        {
            // Arrange
            var productMock = new Mock<Product>("Cheerios", 8.43m);
            var product = productMock.Object;
            var quantity = 0; // Invalid quantity

            // Act
            _cartRepository.AddItemByQuantity(product, quantity);
        }

        [TestMethod]
        public void GetCartItems_ReturnsListOfCartItems()
        {
            // Arrange
            var productMock1 = new Mock<Product>("Cheerios", 8.43m);
            var product1 = productMock1.Object;
            var productMock2 = new Mock<Product>("Milk", 2.99m);
            var product2 = productMock2.Object;
            _cartRepository.AddItemByQuantity(product1, 2);
            _cartRepository.AddItemByQuantity(product2, 1);

            // Act
            var cartItems = _cartRepository.GetCartItems();

            // Assert
            Assert.AreEqual(2, cartItems.Count);
            Assert.IsTrue(cartItems.Any(item => item.Product == product1 && item.Quantity == 2));
            Assert.IsTrue(cartItems.Any(item => item.Product == product2 && item.Quantity == 1));
        }

        //Until it's implemented
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void RemoveItemFromCart_Always_ThrowsNotImplementedException()
        {
            // Arrange
            var productMock = new Mock<Product>("Cheerios", 8.43m);
            var product = productMock.Object;

            // Act
            _cartRepository.RemoveItemFromCart(product);
        }
    }
}
