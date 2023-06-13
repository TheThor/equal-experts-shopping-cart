using EqualExpertsShoppingCart.Models.DTOs;
using System.Text.Json;

namespace EqualExpertsShoppingCart.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly HttpClient _client;

        public ProductRepository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _client = _httpClientFactory.CreateClient();
            _client.BaseAddress = new Uri("https://equalexperts.github.io/backend-take-home-test-data/");
            _client.Timeout = new TimeSpan(0, 0, 30);
            _client.DefaultRequestHeaders.Clear();
        }

        public async Task<ProductDto?> GetProductBySlug(string endpointSlug)
        {
            if (string.IsNullOrEmpty(endpointSlug))
            {
                throw new ArgumentException("Endpoint slug cannot be null or empty.", nameof(endpointSlug));
            }
            try
            {
                var slug = _client.BaseAddress + endpointSlug + ".json";
                var response = await _client.GetAsync(slug, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStreamAsync();
                var product = await JsonSerializer.DeserializeAsync<ProductDto>(
                        stream,
                        new JsonSerializerOptions{ PropertyNameCaseInsensitive = true }
                    );
                return product;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Error occurred during HTTP request: " + ex.Message);
                throw;
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Error occurred during JSON deserialization: " + ex.Message);
                throw;
            }
        }
    }
}
