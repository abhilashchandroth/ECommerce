using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProductService> logger;

        public ProductService(IHttpClientFactory httpClientFactory, ILogger<ProductService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ProductService");
                var respone = await client.GetAsync($"api/product");
                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsByteArrayAsync();
                    var serializationOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, serializationOptions);
                    return (true, result, null);
                }
                return (false, null, respone.ReasonPhrase);
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.ToString());
            }
        }
    }
}
