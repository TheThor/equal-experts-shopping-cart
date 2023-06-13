using EqualExpertsShoppingCart.Models;
using EqualExpertsShoppingCart.Repositories;

namespace EqualExpertsShoppingCart.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product?> GetProductBySlugAsync(string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                throw new ArgumentNullException(nameof(slug), "Slug cannot be null or empty.");
            }
            try
            {
                var productDto = await _productRepository.GetProductBySlug(slug);
                if (productDto == null || productDto.Title == null)
                {
                    throw new InvalidOperationException("No product found or some details missing.");
                }

                return new Product(productDto.Title, productDto.Price);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
