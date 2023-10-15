using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerce.Application.Persistence.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        T Create(T entity);
        T Update(T entity);
        T DeleteById(int id);
    }
}
