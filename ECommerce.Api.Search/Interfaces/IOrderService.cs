using ECommerce.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool isSuccess, IEnumerable<Order> orders, string errorMessage)> GetOrderAsync(int customerId);
    }
}
