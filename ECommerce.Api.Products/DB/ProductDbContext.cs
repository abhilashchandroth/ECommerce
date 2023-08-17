using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.DB
{
    public class ProductDbContext : DbContext
    {

        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}
