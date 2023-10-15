using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Persistence.Abstractions;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<List<Product>> GetAllAsyncWithInclude();
    Task<bool> IsCategoryExist(int categoryId);
}
