using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Infrastructure.Persistence.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder) 
        {
            builder.HasKey(r => r.Id);
            builder.ToTable("Roles", schema: "dbo");

            builder.HasIndex(r => r.Name).IsUnique();
            builder.Property(r => r.Name).HasMaxLength(50).IsRequired();
        }
    }
}
