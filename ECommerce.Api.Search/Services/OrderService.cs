using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrderService> logger;

        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        public async Task<(bool isSuccess, IEnumerable<Order> orders, string errorMessage)> GetOrderAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrderService");
                var respone = await client.GetAsync($"api/orders/{customerId}");
                if (respone.IsSuccessStatusCode)
                {
                    var content = await respone.Content.ReadAsByteArrayAsync();
                    var serializationOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, serializationOptions);
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
