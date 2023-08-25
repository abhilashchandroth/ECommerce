using ECommerce.Api.Search.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductService
    {
        Task<(bool isSuccess, IEnumerable<Product> products, string errorMessage)> GetProductsAsync();
    }
}
