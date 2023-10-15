using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Persistence
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product")
                .Property(b => b.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(b => b.Description).HasColumnName("Description").HasMaxLength(300).IsRequired();
            builder.HasIndex(b => b.Name).IsUnique();
            builder.Property(b => b.Price).HasColumnName("Price").HasColumnType("decimal(18,2)").IsRequired();
            builder.HasOne(b => b.Category).WithMany(c => c.Products).HasForeignKey(d => d.CategoryId);
        }
    }
}
