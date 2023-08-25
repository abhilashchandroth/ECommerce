using ECommerce.Api.Customers.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Controllers
{
    [Controller]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider customerProvider;

        public CustomerController(ICustomerProvider customerProvider)
        {
            this.customerProvider = customerProvider;
        }

        [HttpGet]
        public async Task<ActionResult> GetCustomerAsync()
        {
            var result = await customerProvider.GetCustomerAsync();

            if (result.IsSuccess)
            {
                return Ok(result.customers);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerIdAsync(int id)
        {
            var result = await customerProvider.GetCustomerIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.customer);
            }
            return NotFound();
        }
    }
}
