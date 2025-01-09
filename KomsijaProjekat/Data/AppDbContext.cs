using System.Data.Entity;
using System.Linq;
using KomsijaProjekat.Models;

namespace KomsijaProjekat.Data
{
    public class AppDbContext : DbContext
    {
        

        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            
            if (!context.Users.Any(u => u.Role == UserRole.ADMIN))
            {
                context.Users.Add(new User
                {
                    Name = "ADMIN",
                    Email = "admin@example.com",
                    Password = "admin123", 
                    Role = UserRole.ADMIN
                });
            }

            if (!context.Users.Any(u => u.Role == UserRole.USER))
            {
                context.Users.Add(new User
                {
                    Name = "USER",
                    Email = "user@example.com",
                    Password = "user123", 
                    Role = UserRole.USER
                });
            }

            context.SaveChanges(); 

            base.Seed(context);
        }
    }
}
