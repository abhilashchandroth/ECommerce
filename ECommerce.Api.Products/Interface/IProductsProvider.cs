using ECommerce.Api.Products.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Interface
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductAsync();

        Task<(bool IsSuccess, Product product, string ErrorMessage)> GetProductByIdAsync(int productId);

    }
}
