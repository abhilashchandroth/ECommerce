using ECommerce.Api.Products.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductsProvider productsProvider;

        public ProductController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<ActionResult> GetProductsAsync()
        {
            var result = await productsProvider.GetProductAsync();

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> GetProductByIdAsync(int productId)
        {
            var result = await productsProvider.GetProductByIdAsync(productId);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
