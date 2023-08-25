using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interface;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order { Id = 1, CustomerId = 1, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 1, OrderId = 1, ProductId = 1, Qauntity = 3, UnitPrice = 10 } } });
                dbContext.Orders.Add(new Db.Order { Id = 2, CustomerId = 2, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 2, OrderId = 2, ProductId = 3, Qauntity = 3, UnitPrice = 23 } } });
                dbContext.Orders.Add(new Db.Order { Id = 3, CustomerId = 1, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 3, OrderId = 3, ProductId = 2, Qauntity = 3, UnitPrice = 15 } } });
                dbContext.Orders.Add(new Db.Order { Id = 4, CustomerId = 4, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 4, OrderId = 4, ProductId = 4, Qauntity = 3, UnitPrice = 10 } } });
                dbContext.Orders.Add(new Db.Order { Id = 5, CustomerId = 2, Items = new List<Db.OrderItem>() { new Db.OrderItem { Id = 5, OrderId = 2, ProductId = 1, Qauntity = 3, UnitPrice = 20 } } });

                dbContext.SaveChanges();

            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();

                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
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
