using EqualExpertsShoppingCart.Models;
using Moq;

namespace EqualExpertsShoppingCartTests.Models
{
    [TestClass]
    public class CartModelUnitTests
    {
        /**
         * TDD  thought process::
         *
         * Start by returning an empty List with just return new List().
         *
        **/
        [TestMethod]
        public void Cart_AddsZeroProducts_ReturnsEmptyCart()
        {
            // Arrange
            var cart = new Cart();

            // Assert
            Assert.AreEqual(0, cart.Items.Count);
        }

        /**
         * TDD  thought process::
         *
         * Start by returning that list as before but the test fails.
         * This fails because AddItemByQuantity was return (empty) new List();
         * Fix to return a list that adds a proper product.
         *
        **/
        [TestMethod]
        public void AddItemByQuantity_AddsOneProduct_CartContainsOneProduct()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;

            // Act
            cart.AddItemByQuantity(product, 1);

            // Assert
            Assert.AreEqual(1, cart.Items.Count);
            Assert.AreEqual(product, cart.Items[0].Product);
            Assert.AreEqual(1, cart.Items[0].Quantity);
        }

        /**
         * TDD  thought process::
         *
         * Add two products but they are not equal. Test passes, complement of previous.
         *
        **/
        [TestMethod]
        public void AddItemByQuantity_AddsTwoProducts_CartContainsTwoProducts()
        {
            // Arrange
            var cart = new Cart();
            var product1Mock = new Mock<Product>("Product 1", 10.0m);
            var product1 = product1Mock.Object;
            var product2Mock = new Mock<Product>("Product 2", 15.0m);
            var product2 = product2Mock.Object;

            // Act
            cart.AddItemByQuantity(product1, 2);
            cart.AddItemByQuantity(product2, 1);

            // Assert
            Assert.AreEqual(2, cart.Items.Count);
            Assert.AreEqual(product1, cart.Items[0].Product);
            Assert.AreEqual(2, cart.Items[0].Quantity);
            Assert.AreEqual(product2, cart.Items[1].Product);
            Assert.AreEqual(1, cart.Items[1].Quantity);
        }


        /**
         * TDD  thought process::
         *
         * This initially fails by not checking if the products are the same.
         * The fix was to change the code from just _items.Add(newItem)
         * to the current code that checks if the item exists first.
         * 
         * This should be the end result for adding to a list scope.
         * 
         **/
        [TestMethod]
        public void AddItemByQuantity_AddsTwoOfTheSameAndOneUnique_CartContainsTwoProducts()
        {
            // Arrange
            var cart = new Cart();
            var product1Mock = new Mock<Product>("Product 1", 10.0m);
            var product1 = product1Mock.Object;
            var product2Mock = new Mock<Product>("Product 2", 15.0m);
            var product2 = product2Mock.Object;

            // Act
            cart.AddItemByQuantity(product1, 2);
            cart.AddItemByQuantity(product2, 1);
            cart.AddItemByQuantity(product1, 1);

            // Assert
            Assert.AreEqual(2, cart.Items.Count);
            Assert.AreEqual(product1, cart.Items[0].Product);
            Assert.AreEqual(3, cart.Items[0].Quantity); // 2 (previous) + 1 (new)
            Assert.AreEqual(product2, cart.Items[1].Product);
            Assert.AreEqual(1, cart.Items[1].Quantity);
        }

        /**
         * TDD thought process:
         * 
         * Now that everything is standardized returning an exception only 
         * for value for the invalid quantity, e.g.:
         * public int Quantity set to 0;
         *
         * Quantity should be always >= 0.
         *
         **/
        [TestMethod]
        public void AddItemByQuantity_InitializedWithZero_ThrowsArgumentException()
        {
            // Arrange
            // Arrange
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            var quantity = 0;
            var cart = new Cart();

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => cart.AddItemByQuantity(product, quantity));
        }

        /**
         * TDD  thought process::
         *
         * Same logic as previous but the product is null.
         *
        **/
        [TestMethod]
        public void AddItemByQuantity_AddsNullProduct_CartContainsOneProduct()
        {
            // Arrange
            var cart = new Cart();

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => cart.AddItemByQuantity(null, 1));
        }

        /*
        * TDD  thought process::
        * 
        * The total price can be wrongly calculated. For a single product
        * it can return just an example output of 10.0m, no rounding, just 10.0m.
        * Test passes and we go to the next.
        */
        [TestMethod]
        public void CartItemTotalPrice_AddsOneProductWithQuantityOne_ReturnsCartWithOneProductAndCorrectTotalPrice()
        {
            // Arrange
            var product = new Product("Product", 10.0m);
            var cart = new Cart();

            // Act
            cart.AddItemByQuantity(product, 2);

            // Assert
            Assert.AreEqual(20.0m, cart.Items[0].TotalPrice);
        }


        /*
        * TDD  thought process::
        * 
        * The total price of the previous will always return 10.0m (no calculations done).
        * Now this test fails and we need to change the code from 
        * public decimal TotalPrice => 10.0m; to
        * public decimal TotalPrice => Product.ProductPrice.Amount * Quantity;
        *
        */
        [TestMethod]
        public void CartItemTotalPrice_AddsOneProductWithQuantityTwo_ReturnsCartWithOneProductAndCorrectTotalPrice()
        {
            // Arrange
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            var cart = new Cart();

            // Act
            cart.AddItemByQuantity(product, 2);

            // Assert
            Assert.AreEqual(20.0m, cart.Items[0].TotalPrice);
        }

        /*
        * TDD  thought process::
        * 
        * The total price isn't rounded, as per requirements, to two decimal points.
        * 
        * This test initially fails for the code:
        * public decimal TotalPrice => Product.ProductPrice.Amount * Quantity;
        * 
        * Which is then changes to 
        *  public decimal TotalPrice => Math.Round(Product.ProductPrice.Amount * Quantity, 2);
        * and it passes.
        *
        */
        [TestMethod]
        public void CartItemTotalPrice_AddsPriceNotRounded_ReturnsIncorrectTotalPrice()
        {
            // Arrange
            var productMock = new Mock<Product>("Product", 10.066666m);
            var product = productMock.Object;
            var cart = new Cart();

            // Act
            cart.AddItemByQuantity(product, 1);

            // Assert
            Assert.AreEqual(10.07m, cart.Items[0].TotalPrice);
        }

        /**
         * TDD thought process:
         * 
         * Cart Subtotal is calculated by adding zero products and
         * having it return 0, i.e.:
         * public decimal Subtotal => 0;
         * 
         **/
        [TestMethod]
        public void CartSubtotal_AddsZeroProducts_ReturnsZero()
        {
            // Arrange
            var cart = new Cart();

            // Act
            var subtotal = cart.Subtotal;

            // Assert
            Assert.AreEqual(0.0m, subtotal);
        }

        /**
        * TDD thought process:
        * 
        * To make the previous fail and create a new condition,
        * set a product of cost 10.0m and add to the cart.
        * This will be irrelevant because this will be:
        * public decimal Subtotal => 10.0m;
        *
        * This test passes but the previous fails. So we change this to:
        *
        *  public decimal Subtotal => Items.Count > 0 ? 10.0m : 0.0m;
        *
        **/
        [TestMethod]
        public void CartSubtotal_AddsOneProduct_ReturnsProductPrice()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            cart.AddItemByQuantity(product, 1);

            // Act
            var subtotal = cart.Subtotal;

            // Assert
            Assert.AreEqual(10.0m, subtotal);
        }

        /**
        * TDD thought process:
        * 
        * Since we did soemthing not very generic, let's add more complexity.
        * Adding two products will force us to create a new dynamic way to
        * calculate the Subtotal.
        *
        * This test fails initially so we switch to:
        *
        *  public decimal Subtotal => Items.Sum(item => item.TotalPrice);
        *
        **/
        [TestMethod]
        public void CartSubtotal_AddsTwoProduct_ReturnsProductPrice()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            cart.AddItemByQuantity(product, 1);

            // Act
            var subtotal = cart.Subtotal;

            // Assert
            Assert.AreEqual(10.0m, subtotal);
        }

        /**
        * TDD thought process:
        * 
        * Having solved the grouping for the subtotal let's look into the 
        * rouding. There's no rouding initially so let's have this failling.
        *
        * This failed because it was like this:        *
        *  public decimal Subtotal => Items.Sum(item => item.TotalPrice);
        *  
        * This passes now because we changed it to:
        *  public decimal Subtotal => Math.Round(Items.Sum(item => item.TotalPrice));
        *
        **/
        [TestMethod]
        public void CartSubtotal_AddsPriceNotRounded_ReturnsSumOfProductPrices()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.066666m);
            var product = productMock.Object;
            cart.AddItemByQuantity(product, 1);

            // Act
            var subtotal = cart.Subtotal;

            // Assert
            Assert.AreEqual(10.07m, subtotal);
        }

        /**
        * TDD thought process:
        * 
        * Start by returning a constant value for the tax, e.g.:
        * public decimal Tax => 0.0m;
        *
        **/
        [TestMethod]
        public void CartTax_SubtotalIsZero_ReturnsZeroTax()
        {
            // Arrange
            var cart = new Cart();

            // Act
            var tax = cart.Tax;

            // Assert
            Assert.AreEqual(0.0m, tax);
        }

        /**
         * TDD thought process:
         * 
         * To make the previous test fail, update the tax calculation to a specific value, e.g.:
         * public decimal Tax => 10.0m;
         *
         * This test will fail, and then we can adjust it to a more dynamic calculation. 
         * 
         * After this is done the previous will fail but this will pass. So we do:
         * public decimal Tax => Items.Count > 0 ? 10.0m : 0.0m;
         * 
         * Both pass now.
         *
         **/
        [TestMethod]
        public void CartTax_CalculatesTaxBasedOnSubtotal_ReturnsCorrectTax()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            cart.AddItemByQuantity(product, 1);

            // Act
            var tax = cart.Tax;

            // Assert
            Assert.AreEqual(1.25m, tax);
        }

        /**
        * TDD thought process:
        * 
        * The previous are passing, let's make things more complicated.
        * 
        * We add two products and check the expected result. Since the
        * prevous tests are the only point of validation this will fail.
        * 
        * We change from 
        *   public decimal Tax => Items.Count > 0 ? 1.25m : 0.0m;
        * To:
        *   public decimal Tax => Subtotal * 0.125m;
        *   
        * Now it works according to our 3 tests, combined.
        *
        **/
        [TestMethod]
        public void CartTax_CalculatesTaxBasedOnSubtotalOfTwoProducts_ReturnsCorrectTax()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            var product1Mock = new Mock<Product>("Product 1", 15.0m);
            var product1 = product1Mock.Object;
            cart.AddItemByQuantity(product, 1);
            cart.AddItemByQuantity(product1, 2);
            var expectedResult = cart.Subtotal * 0.125m;
            // Act

            var tax = cart.Tax;

            // Assert
            Assert.AreEqual(expectedResult, tax);
        }

        /**
         * TDD thought process:
         * 
         * Now let's introduce the dynamic calculation but, initially,
         * there's no rounding applied to the tax.
         *
         * This test will fail initially, and then we'll add the rounding to fix it.
         *
         **/
        [TestMethod]
        public void CartTax_CalculatesRoundedTaxBasedOnSubtotal_ReturnsCorrectTax()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            cart.AddItemByQuantity(product, 1);

            // Act
            var tax = cart.Tax;

            // Assert
            Assert.AreEqual(1.25m, tax);
        }

        /**
         * TDD thought process:
         * 
         * Start by returning a constant value for the total, e.g.:
         * public decimal Total => 0.0m;
         *
         **/
        [TestMethod]
        public void CartTotal_NoProductsSubtotalAndTaxAreZero_ReturnsZeroTotal()
        {
            // Arrange
            var cart = new Cart();

            // Act
            var total = cart.Total;

            // Assert
            Assert.AreEqual(0.0m, total);
        }

        /**
         * TDD thought process:
         * 
         * To make the previous test fail, update the total calculation to a specific value, e.g.:
         * public decimal Total => 11.25m;
         *
         * This test will pass now, but the previous fail. Then we can adjust it to a more dynamic calculation. 
         * 
         * After this is done, the previous test will fail but this one will pass. So we do:
         * public decimal Total => Items.Count > 0 ? 10.0m : 0.0m;
         * 
         * Both tests pass now.
         *
         **/
        [TestMethod]
        public void CartTotal_AddsOneProductCalculatesTotalBasedOnSubtotalAndTax_ReturnsCorrectTotal()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            cart.AddItemByQuantity(product, 1);
            var expected = 11.25m;

            // Act
            var total = cart.Total;

            // Assert
            Assert.AreEqual(expected, total);
        }

        /**
         * TDD thought process:
         * 
         * The previous tests are passing, let's make things more complicated.
         * 
         * We add two products and check the expected result. Since the
         * previous tests are the only point of validation, this will fail.
         * 
         * We change from 
         *   public decimal Total => Items.Count > 0 ? 10.0m : 0.0m;
         * To:
         *   public decimal Total => Subtotal + Tax;
         *   
         * Now it works according to our 3 tests, combined.
         *
         **/
        [TestMethod]
        public void CartTotal_AddsTwoProductsCalculatesTotalBasedOnSubtotalAndTax_ReturnsCorrectTotal()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            var product1Mock = new Mock<Product>("Product 1", 15.0m);
            var product1 = product1Mock.Object;
            cart.AddItemByQuantity(product, 1);
            cart.AddItemByQuantity(product1, 2);
            var expectedResult = cart.Subtotal + cart.Tax;

            // Act
            var total = cart.Total;

            // Assert
            Assert.AreEqual(expectedResult, total);
        }

        /**
         * TDD thought process:
         * 
         * Now let's introduce the rounding to the total.
         * Initially, there's no rounding applied to the total.
         *
         * This test will fail initially, and then we'll add the rounding to fix it.
         *
         * Final state:
         *   public decimal Total => Math.Round(Subtotal + Tax, 2);
         *
         **/
        [TestMethod]
        public void CartTotal_CalculatesRoundedTotalBasedOnSubtotalAndTax_ReturnsCorrectTotal()
        {
            // Arrange
            var cart = new Cart();
            var productMock = new Mock<Product>("Product", 10.066666m);
            var product = productMock.Object;
            cart.AddItemByQuantity(product, 1);

            // Act
            var total = cart.Total;

            // Assert
            Assert.AreEqual(11.33m, total);
        }

        /**
         * TDD thought process:
         * 
         * Let's introduce a method to increase the quantity by a given amount.
         * Initially, there's no method to increase the quantity, so we'll have a failing test.
         * The expected value is 3 for the total quantity. Let's set Quantity = 3.
         *
         **/
        [TestMethod]
        public void CartItemIncreaseQuantity_QuantityAmountTwo_ReturnsIncreasedQuantity()
        {
            // Arrange
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            var quantity = 1;
            var increaseAmount = 2;
            var expectedQuantity = quantity + increaseAmount;
            var cart = new Cart();

            // Act
            cart.AddItemByQuantity(product, quantity);
            var resultQuantity = cart.Items[0].Quantity;
            cart.Items[0].IncreaseQuantity(increaseAmount);

            // Assert
            var result = cart.Items[0].Quantity;
            Assert.AreEqual(expectedQuantity, result);
        }

        /**
         * TDD thought process:
         * 
         * We will now increase by 3. This will collide with the previous test
         * and we will need to have a logic to either set to 3 or 2;
         * 
         * if (quantity == 2) return 3;
         * else if (quantity == 3) return 4;
         *
         **/
        [TestMethod]
        public void CartItemIncreaseQuantity_QuantityAmountThree_ReturnsIncreasedQuantity()
        {
            // Arrange
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            var quantity = 1;
            var increaseAmount = 3;
            var expectedQuantity = quantity + increaseAmount;
            var cart = new Cart();

            // Act
            cart.AddItemByQuantity(product, quantity);
            var resultQuantity = cart.Items[0].Quantity;
            cart.Items[0].IncreaseQuantity(increaseAmount);

            // Assert
            var result = cart.Items[0].Quantity;
            Assert.AreEqual(expectedQuantity, result);
        }

        /**
         * TDD thought process:
         * 
         * This is becoming too complex to have ifs deciding.
         * Let's add proper logic, i.e.:
         * 
         * Quantity += quantity;
         * 
         * This will now cover everyt scenario in terms of quantity.
         *
         **/
        [TestMethod]
        public void CartItemIncreaseQuantity_QuantityAmountFour_ReturnsIncreasedQuantity()
        {
            // Arrange
            var productMock = new Mock<Product>("Product", 10.0m);
            var product = productMock.Object;
            var quantity = 1;
            var increaseAmount = 4;
            var expectedQuantity = quantity + increaseAmount;
            var cart = new Cart();

            // Act
            cart.AddItemByQuantity(product, quantity);
            var resultQuantity = cart.Items[0].Quantity;
            cart.Items[0].IncreaseQuantity(increaseAmount);

            // Assert
            var result = cart.Items[0].Quantity;
            Assert.AreEqual(expectedQuantity, result);
        }

    }
}
