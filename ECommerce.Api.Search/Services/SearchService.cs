using ECommerce.Api.Search.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var customerResult = await customerService.GetCustomerAsync(customerId);
            var (isSuccess, orders, errorMessage) = await orderService.GetOrderAsync(customerId);
            var productResult = await productService.GetProductsAsync();
            if (isSuccess)
            {
                foreach (var order in orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.isSuccess ? productResult.products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                                                            "Product information not available.";
                    }

                }
                var result = new
                {
                    Customer = customerResult.isSuccess ?
                                    customerResult.customer :
                                    new { Name = "Customer information not available" },
                    Orders = orders
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
