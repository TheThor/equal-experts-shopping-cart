using EqualExpertsShoppingCart.Models;

namespace EqualExpertsShoppingCartTests.Models
{
    [TestClass]
    public class ProductModelUnitTests
    {

        // Start with basics
        [TestMethod]
        public void Product_InitializedWithPriceNegative_ThrowsArgumentException()
        {
            // Arrange
            string name = "Test Product";
            decimal price = -10.0m;

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Product(name, price));
        }

        [TestMethod]
        public void Product_InitializedWithTitleWhiteSpace_ThrowsArgumentException()
        {
            // Arrange
            string name = " ";
            decimal price = 10.0m;

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Product(name, price));
        }

        [TestMethod]
        public void Product_InitializedWithTitleNull_ThrowsArgumentException()
        {
            // Arrange
            string name = null;
            decimal price = 10.0m;

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => new Product(name, price));
        }

        [TestMethod]
        public void Product_InitializedWithNameAndPrice_PropertiesTitlePriceAreSetCorrectly()
        {
            // Arrange
            string name = "Test Product";
            decimal price = 10.0m;

            // Act
            var product = new Product(name, price);

            // Assert
            Assert.AreEqual(name, product.Title);
            Assert.AreEqual(price, product.ProductPrice.Amount);
        }

        [TestMethod]
        public void ProductEquals_WithObjectThatIsNull_ReturnsFalse()
        {
            // Arrange
            var product = new Product("Test", 10.0m);

            // Act
            var result = product.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProductEquals_ComparedToDifferentObjectType_ReturnsFalse()
        {
            // Arrange
            var product = new Product("Test", 10.0m);
            var otherObject = new object();

            // Act
            var result = product.Equals(otherObject);

            // Assert
            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void ProductEqualityOperator_WithEqualObjects_ReturnsTrue()
        {
            // Arrange
            var product1 = new Product("Test", 10.0m);
            var product2 = new Product("Test", 10.0m);

            // Act
            var result = product1 == product2;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProductEqualityOperator_WithDifferentObjects_ReturnsFalse()
        {
            // Arrange
            var product1 = new Product("Test", 10.0m);
            var product2 = new Product("Test1", 5.0m);

            // Act
            var result = product1 == product2;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProductInequalityOperator_WithEqualObjects_ReturnsFalse()
        {
            // Arrange
            var product1 = new Product("Test", 10.0m);
            var product2 = new Product("Test", 10.0m);

            // Act
            var result = product1 != product2;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProductInequalityOperator_WithDifferentObjects_ReturnsFalse()
        {
            // Arrange
            var product1 = new Product("Test", 10.0m);
            var product2 = new Product("Test1", 5.0m);

            // Act
            var result = product1 != product2;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProductGetHashCode_SameProduct_ReturnsSameHashCode()
        {
            // Arrange
            var product1 = new Product("Test Product", 10.0m);
            var product2 = new Product("Test Product", 10.0m);

            // Act
            var hashCode1 = product1.GetHashCode();
            var hashCode2 = product2.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2);
        }

        [TestMethod]
        public void ProductGetHashCode_DifferentProduct_ReturnsDifferentHashCode()
        {
            // Arrange
            var product1 = new Product("Product A", 10.0m);
            var product2 = new Product("Product B", 15.0m);

            // Act
            var hashCode1 = product1.GetHashCode();
            var hashCode2 = product2.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2);
        }

        [TestMethod]
        public void ProductPriceEquals_WithSameValues_ReturnsTrueForEqualPrices()
        {
            // Arrange
            var product1 = new Product("Test Product", 10.0m);
            var product2 = new Product("Test Product", 10.0m);

            // Act
            var result = product1.ProductPrice.Equals(product2.ProductPrice);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProductPriceEquals_WithNullObject_ReturnsTrueForEqualPrices()
        {
            // Arrange
            var product1 = new Product("Test Product", 10.0m);
            var product2 = new Product("Test Product", 10.0m);

            // Act
            var result = product1.ProductPrice.Equals(product2.ProductPrice);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProductPriceEquals_WithDifferentValues_ReturnsFalse()
        {
            // Arrange
            var product1 = new Product("Test Product", 10.0m);
            var product2 = new Product("Test Product", 20.0m);

            // Act
            var result = product1.ProductPrice.Equals(product2.ProductPrice);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProductPriceEquals_WithDifferentObjectType_ReturnsFalse()
        {
            // Arrange
            var product1 = new Product("Test Product", 10.0m);
            var product2 = new Product("Test Product", 20.0m);

            // Act
            var result = product1.ProductPrice.Equals(new object());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProductPriceEquals_WithNullObject_ReturnsFalse()
        {
            // Arrange
            var product1 = new Product("Test Product", 10.0m);
            var product2 = new Product("Test Product", 20.0m);

            // Act
            var result = product1.ProductPrice.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProductPriceEqualityOperator_WithEqualObjects_ReturnsTrue()
        {
            // Arrange
            var product1 = new Product("Test", 10.0m);
            var product2 = new Product("Test", 10.0m);

            // Act
            var result = product1.ProductPrice == product2.ProductPrice;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProductPriceEqualityOperator_WithDifferentObjects_ReturnsFalse()
        {
            // Arrange
            var product1 = new Product("Test1", 10.0m);
            var product2 = new Product("Test2", 20.0m);

            // Act
            var result = product1.ProductPrice == product2.ProductPrice;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProductPriceInequalityOperator_WithEqualObjects_ReturnsFalse()
        {
            // Arrange
            var product1 = new Product("Test", 10.0m);
            var product2 = new Product("Test", 10.0m);

            // Act
            var result = product1.ProductPrice != product2.ProductPrice;

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProductPriceInequalityOperator_WithDifferentObjects_ReturnsTrue()
        {
            // Arrange
            var product1 = new Product("Test1", 10.0m);
            var product2 = new Product("Test2", 20.0m);

            // Act
            var result = product1.ProductPrice != product2.ProductPrice;

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProductPriceGetHashCode_WithSameValues_ReturnsSameValueForEqualPrices()
        {
            // Arrange
            var product1 = new Product("Test Product", 10.0m);
            var product2 = new Product("Test Product", 10.0m);

            // Act
            var hashCode1 = product1.ProductPrice.GetHashCode();
            var hashCode2 = product2.ProductPrice.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2);
        }

        [TestMethod]
        public void ProductPriceGetHashCode_ReturnsDifferentValueForDifferentPrices()
        {
            // Arrange
            var product1 = new Product("Test Product", 10.0m);
            var product2 = new Product("Test Product", 20.0m);

            // Act
            var hashCode1 = product1.ProductPrice.GetHashCode();
            var hashCode2 = product2.ProductPrice.GetHashCode();

            // Assert
            Assert.AreNotEqual(hashCode1, hashCode2);
        }
    }
}
