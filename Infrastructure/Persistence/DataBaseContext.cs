using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProductManagment.Domain.Entities;
using ProductManagment.Infrastructure.Persistence.Configurations;

namespace ProductManagment.Infrastructure.Persistence
{
    public class DataBaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());
        }
        public DataBaseContext() { Database.EnsureCreated(); }
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
    }
}
