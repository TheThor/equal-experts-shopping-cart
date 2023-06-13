using EqualExpertsShoppingCart.Models;

namespace EqualExpertsShoppingCart.Repositories
{
    public interface ICartRepository
    {
        Cart GetCart();
        void AddItemByQuantity(Product product, int quantity = 1);
        void RemoveItemFromCart(Product product, int quantity = 1);
        IReadOnlyList<Cart.CartItem> GetCartItems();
    }
}
