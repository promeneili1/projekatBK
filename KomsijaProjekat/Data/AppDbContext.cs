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

    // Definiši inicijalizator baze sa Seed metodom
    public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            // Dodavanje Admin korisnika ako ne postoji
            if (!context.Users.Any(u => u.Role == UserRole.ADMIN))
            {
                context.Users.Add(new User
                {
                    Name = "ADMIN",
                    Email = "admin@example.com",
                    Password = "admin123", // Hash ako koristiš
                    Role = UserRole.ADMIN
                });
            }

            // Dodavanje User korisnika ako ne postoji
            if (!context.Users.Any(u => u.Role == UserRole.USER))
            {
                context.Users.Add(new User
                {
                    Name = "USER",
                    Email = "user@example.com",
                    Password = "user123", // Hash ako koristiš
                    Role = UserRole.USER
                });
            }

            context.SaveChanges(); // Sačuvaj promene u bazi

            base.Seed(context);
        }
    }
}
