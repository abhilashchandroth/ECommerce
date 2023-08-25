using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool isSuccess, dynamic customer, string errorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var client = httpClientFactory.CreateClient("CustomerService");
                var respone = await client.GetAsync($"api/customer/{id}");
                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsByteArrayAsync();
                    var serializationOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<dynamic>(content, serializationOptions);
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
