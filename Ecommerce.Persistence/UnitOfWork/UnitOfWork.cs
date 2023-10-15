using Ecommerce.Application.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntitiesContext _context;
        private readonly Dictionary<Type, object> _repositories;
        public UnitOfWork(EntitiesContext context)
        {
            _context = context; 
            _repositories = new Dictionary<Type, object>();
        }

        public void Commit()
        {

           _context.SaveChangesAsync();

        }

        //public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
        //{
        //    if (_repositories.ContainsKey(typeof(T)))
        //    {
        //        return (IGenericRepository<T>)_repositories[typeof(T)];
        //    }

        //    var repository = new GenericRepository<T>(_context);
        //    _repositories[typeof(T)] = repository;
        //    repository._dbContext = _context;
        //    return repository;
        //}
    }
}
