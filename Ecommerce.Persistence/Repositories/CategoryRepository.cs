global using Ecommerce.Domain.Entities;
using Ecommerce.Application.Persistence.Abstractions;

namespace Ecommerce.Persistence
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(EntitiesContext context) : base(context)
        {
        }
    }
}
