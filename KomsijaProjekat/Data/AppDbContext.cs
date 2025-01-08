using System.Collections.Generic;
using System.Data.Entity;
using KomsijaProjekat.Models;

namespace KomsijaProjekat.Data
{
    public class AppDbContext : DbContext
    {

        

        static AppDbContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<AppDbContext>());
        }
        public AppDbContext() : base("DefaultConnection") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public DbSet<Product> Products { get; set; }


    }
}
