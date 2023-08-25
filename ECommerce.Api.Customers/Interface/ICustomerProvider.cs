using ECommerce.Api.Customers.Db;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Interface
{
    public interface ICustomerProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomerAsync();

        Task<(bool IsSuccess, Models.Customer customer, string ErrorMessage)> GetCustomerIdAsync(int customerId);
    }
}
