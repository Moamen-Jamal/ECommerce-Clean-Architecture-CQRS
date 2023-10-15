using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Persistence
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category")
                .Property(b => b.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(b => b.Description).HasColumnName("Description").HasMaxLength(300).IsRequired();
            builder.HasIndex(b => b.Name).IsUnique();
            builder.HasMany(b => b.Products).WithOne(b => b.Category);
        }
    }
}
