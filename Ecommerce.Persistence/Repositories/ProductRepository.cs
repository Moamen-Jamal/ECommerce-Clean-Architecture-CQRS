using Ecommerce.Application.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(EntitiesContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetAllAsyncWithInclude()
         => await _dbContext.Products.AsNoTracking().Include(x => x.Category).ToListAsync();

        public async Task<bool> IsCategoryExist(int categoryId)
        {
            var result = await _dbContext.Categories.FindAsync(categoryId);
            return result != null;
        }
    }
}
