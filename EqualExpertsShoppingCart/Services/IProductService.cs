using EqualExpertsShoppingCart.Models;

namespace EqualExpertsShoppingCart.Services
{
    public interface IProductService
    {
        public Task<Product?> GetProductBySlugAsync(string slug);
    }
}