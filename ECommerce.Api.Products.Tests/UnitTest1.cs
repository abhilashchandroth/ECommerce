using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async void GetProductsReturnsProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                            .UseInMemoryDatabase("GetProductsReturnsProducts")
                            .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuartion = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuartion);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.products.Any());
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async void GetProductsReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                            .UseInMemoryDatabase("GetProductsReturnsProductUsingValidId")
                            .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuartion = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuartion);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductByIdAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.product);
            Assert.True(product.product.Id == 1);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async void GetProductsReturnsErrorUsingInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                            .UseInMemoryDatabase("GetProductsReturnsErrorUsingInValidId")
                            .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuartion = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuartion);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductByIdAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductDbContext dbContext)
        {
            for (int i=1; i<=10;i++)
            {
                dbContext.Products.Add(new Db.Product { Id = i, Inventory = 1 +10, Name = Guid.NewGuid().ToString(), Price = i * 4 });
            }
            
            dbContext.SaveChanges();
        }
    }
}