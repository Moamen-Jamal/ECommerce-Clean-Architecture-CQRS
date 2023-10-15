using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Persistence
{
    public class EntitiesContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public EntitiesContext(DbContextOptions<EntitiesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
        //        .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        //    {
        //        entry.Entity.ModifiedDate = DateTime.Now;
        //        if (entry.State == EntityState.Added)
        //        {
        //            entry.Entity.CreatedDate = DateTime.Now;
        //        }
        //    }
        //    return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false); ;
        //}
    }
}