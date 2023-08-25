using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext dbContext;
        private readonly ILogger<CustomerProvider> logger;
        private readonly IMapper mapper;

        public CustomerProvider(CustomerDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer { Id = 1, Name = "Jose", Address = "ABCD" });
                dbContext.Customers.Add(new Db.Customer { Id = 2, Name = "Jacod", Address = "dsfd" });
                dbContext.Customers.Add(new Db.Customer { Id = 3, Name = "Jershin", Address = "xcv" });
                dbContext.Customers.Add(new Db.Customer { Id = 4, Name = "Jim", Address = "cxv" });
                dbContext.Customers.Add(new Db.Customer { Id = 5, Name = "Javed", Address = "sdf" });

                dbContext.SaveChanges();

            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> customers, string ErrorMessage)> GetCustomerAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (System.Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Customer customer, string ErrorMessage)> GetCustomerIdAsync(int customerId)
        {
            try
            {
                var customer = await dbContext.Customers.FirstAsync(p => p.Id == customerId);

                if (customer != null)
                {
                    var result = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (System.Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
