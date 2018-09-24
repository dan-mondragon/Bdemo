using Brio.Data.Configuration;
using Brio.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Brio.Data
{
    public class BrioContext : DbContext
    {
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        private readonly string _dbName;

        public BrioContext(DbContextOptions<BrioContext> options) : base(options)
        {

        }

        public BrioContext(string dbName)
        {
            _dbName = dbName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!string.IsNullOrEmpty(_dbName))
                optionsBuilder.UseSqlServer(_dbName);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new BrandConfiguration(modelBuilder.Entity<Brand>());
            new ProductConfiguration(modelBuilder.Entity<Product>());
        }
    }
}
