using EqualExpertsShoppingCart.Models;
using static EqualExpertsShoppingCart.Models.Cart;

namespace EqualExpertsShoppingCart.Repositories
{
    public class CartRepository : ICartRepository
    {
        private Cart _cart { get; }

        public CartRepository()
        {
            _cart = new Cart();
        }

        public Cart GetCart()
        {
            return _cart;
        }

        public void AddItemByQuantity(Product product, int quantity = 1)
        {
            try
            {
                _cart.AddItemByQuantity(product, quantity);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void RemoveItemFromCart(Product product, int quantity = 1)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<CartItem> GetCartItems()
        {
            return _cart.Items.ToList();
        }
    }
}
