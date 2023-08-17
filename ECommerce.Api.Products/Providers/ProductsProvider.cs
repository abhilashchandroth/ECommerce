using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Interface;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new DB.Product { Id = 1, Inventory = 20, Name = "Mouse", Price = 150 });
                dbContext.Products.Add(new DB.Product { Id = 2, Inventory = 23, Name = "Keyboad", Price = 1000 });
                dbContext.Products.Add(new DB.Product { Id = 3, Inventory = 21, Name = "Monitor", Price = 25000 });
                dbContext.Products.Add(new DB.Product { Id = 4, Inventory = 12, Name = "Hard Disk", Price = 2500 });
                dbContext.Products.Add(new DB.Product { Id = 5, Inventory = 34, Name = "Pen Drive", Price = 2000 });

                dbContext.SaveChanges();

            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> products, string ErrorMessage)> GetProductAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();

                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
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

        public async Task<(bool IsSuccess, Models.Product product, string ErrorMessage)> GetProductByIdAsync(int productId)
        {
            try
            {
                var product = await dbContext.Products.FirstAsync(p => p.Id == productId);

                if (product != null)
                {
                    var result = mapper.Map<DB.Product, Models.Product>(product);
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
