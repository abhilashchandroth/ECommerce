using ECommerce.Api.Search.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool isSuccess, dynamic customer, string errorMessage)> GetCustomerAsync(int id);
    }
}
