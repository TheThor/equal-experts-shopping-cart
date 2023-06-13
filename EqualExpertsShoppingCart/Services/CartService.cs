using EqualExpertsShoppingCart.Models;
using EqualExpertsShoppingCart.Repositories;

namespace EqualExpertsShoppingCart.Services
{
    public class CartService
    {
        private readonly IProductService _productService;
        private readonly ICartRepository _cartRepository;

        public CartService(IProductService productService, ICartRepository cartRepository)
        {
            _productService = productService;
            _cartRepository = cartRepository;
        }

        public Cart GetCart()
        {
            return _cartRepository.GetCart() ?? new Cart();
        }

        public async Task<Cart> AddProductToCart(string slug, int quantity)
        {
            var cart = _cartRepository.GetCart();

            try
            {
                var product = await _productService.GetProductBySlugAsync(slug);
                if (product is null)
                {
                    throw new InvalidOperationException("No product found.");
                }
                _cartRepository.AddItemByQuantity(product, quantity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return cart;
        }
    }
}
