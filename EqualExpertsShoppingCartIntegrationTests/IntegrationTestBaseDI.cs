using EqualExpertsShoppingCart.Repositories;
using EqualExpertsShoppingCart.Services;
using EqualExpertsShoppingCartIntegrationTests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace EqualExpertsShoppingCartIntegrationTests
{
    public abstract class IntegrationTestBase
    {
        protected ServiceProvider ServiceProvider { get; private set; } = null!;

        [TestInitialize]
        public void OneTimeSetUp()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            services.AddScoped<IHttpClientFactory, DefaultHttpClientFactory>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICartRepository, CartRepository>();

            ServiceProvider = services.BuildServiceProvider();
            // Logging
            /*Console.WriteLine("ServiceProvider built with the following services:");
            foreach (var serviceDescriptor in services)
            {
                Console.WriteLine(serviceDescriptor.ServiceType);
            }*/
        }

        [TestCleanup]
        public void OneTimeTearDown()
        {
            ServiceProvider.Dispose();
        }
    }
}
