using Ecommerce.Application.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Ecommerce.Persistence
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public EntitiesContext _dbContext;
        DbSet<T> dbSet;
        public GenericRepository(EntitiesContext context)
        {
            _dbContext = context;
            dbSet = _dbContext.Set<T>();
        }

        public T Create(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            return dbSet.Add(entity).Entity;
        }

        public  T DeleteById(int id)
        {
            T selected = dbSet.Find(id);
            dbSet.Remove(selected);
            return selected;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
         => await dbSet.ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        => await dbSet.FindAsync(id);
        

        public T Update(T entity)
        {
            if (!dbSet.Local.Any(i => i.Id == entity.Id))
                dbSet.Attach(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
            entity.ModifiedDate = DateTime.Now;

            return entity;
        }

    }
}
