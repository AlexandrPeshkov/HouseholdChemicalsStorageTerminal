using HC.Data;
using Microsoft.EntityFrameworkCore;

namespace HC.EF.Sqlite.Core5
{
    public class TestContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Storage> Storages { get; set; }

        public TestContext()
        {
            // var folder = Environment.SpecialFolder.LocalApplicationData;
            // var path = Environment.GetFolderPath(folder);
            // DbPath = Path.Combine(path, "hc.db");
            //DbPath = "C:\\Temp\\HC.db";

            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //options.UseSqlite($"Data Source=hc.db");
            options.UseSqlite($"Filename=C:\\Temp\\hc.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Storage>(e =>
            {
                e.HasKey(x => x.Id);
                //e.HasMany<Product>(x => x.Products).WithOne().HasForeignKey(x => x.StorageId);
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.HasKey(x => x.Id);
                //e.HasOne<Storage>().WithOne().HasForeignKey<Product>(x => x.StorageId);
            });
        }
    }
}