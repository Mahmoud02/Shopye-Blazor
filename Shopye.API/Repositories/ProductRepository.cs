using Microsoft.EntityFrameworkCore;
using Shopye.Api.Data;
using Shopye.Api.Entities;
using Shopye.Api.Repositories.Contracts;

namespace Shopye.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopyeDbContext ShopyeDbContext;

        public ProductRepository(ShopyeDbContext ShopyeDbContext)
        {
            this.ShopyeDbContext = ShopyeDbContext;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await this.ShopyeDbContext.ProductCategories.ToListAsync();
           
            return categories; 
        
        }

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await ShopyeDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await ShopyeDbContext.Products
                                .Include(p => p.ProductCategory)
                                .SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await this.ShopyeDbContext.Products
                                     .Include(p => p.ProductCategory).ToListAsync();

            return products;
        
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await this.ShopyeDbContext.Products
                                     .Include(p => p.ProductCategory)
                                     .Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }
    }
}
