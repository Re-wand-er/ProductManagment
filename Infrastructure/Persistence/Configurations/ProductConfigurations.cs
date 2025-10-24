using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Infrastructure.Persistence.Configurations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder) 
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Products", schema: "dbo").ToTable(p => p.HasCheckConstraint("CK_Product_Cost", "Cost > 0"));

            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(255);
            builder.Property(p => p.Cost).HasPrecision(10, 3).IsRequired();
            builder.Property(p => p.GeneralNote).HasMaxLength(255);
            builder.Property(p => p.SpecialNote).HasMaxLength(255);

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
