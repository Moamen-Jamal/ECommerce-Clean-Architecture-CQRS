using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Persistence.Abstractions
{
    public interface IUnitOfWork
    {
        //IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
        void Commit();
    }
}
