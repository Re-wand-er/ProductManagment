using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Infrastructure.Persistence.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder) 
        { 
            builder.HasKey(c => c.Id);
            builder.ToTable("Categories", schema:"dbo");

            builder.HasIndex(c => c.Name).IsUnique();
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
        }
    }
}
