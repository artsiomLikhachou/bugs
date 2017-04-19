using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestEfMemory
{
    public class ProductService
    {
        private readonly TestDbContext _dbContext;

        public ProductService(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<ProductInfo>> GetWithMemoryLeak()
        {
            var products = _dbContext.Product.Include(x => x.Vendor)
                .Where(x => x.Prop1 == "value1")
                .Where(x => x.Prop2 == "value2")
                .OrderBy(x => x.Name)
                .Select(x => MapToProductInfo(x))
                .ToListAsync();

            return await products;
           
        }

        public async Task<IList<ProductInfo>> GetWithoutMemoryLeak()
        {
            var products = await _dbContext.Product.Include(x => x.Vendor)
                .Where(x => x.Prop1 == "value1")
                .Where(x => x.Prop2 == "value2")
                .OrderBy(x => x.Name)
                .ToListAsync();

            return products.Select(x => MapToProductInfo(x)).ToList();
        }

        private ProductInfo MapToProductInfo(Product product)
        {
            return new ProductInfo
            {
                ProductName = product.Name,
                VendorName = product.Vendor.Name,
                CalculatedValue = new Random().Next().ToString()
            };
        }
    }
}