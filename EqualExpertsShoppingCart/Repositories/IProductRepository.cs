using EqualExpertsShoppingCart.Models.DTOs;

namespace EqualExpertsShoppingCart.Repositories
{
    public interface IProductRepository
    {
        public Task<ProductDto?> GetProductBySlug(string slug);
    }
}
