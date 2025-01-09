using KomsijaProjekat.Authorize;
using KomsijaProjekat.Data;
using KomsijaProjekat.Models;
using System.Linq;
using System.Web.Mvc;

namespace KomsijaProjekat.Controllers
{
    public class ProductController : Controller
    {
        private AppDbContext _context;

        public ProductController()
        {
            _context = new AppDbContext();
        }

        // Dispose context
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // Index - Lista svih proizvoda
        public ActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // Create - Prikazuje formu za kreiranje proizvoda
        [AuthorizeRole(UserRole.USER)] // Samo korisnik sa ulogom USER može da vidi ovu stranicu
        public ActionResult Create()
        {
            return View();
        }

        // Create (POST) - Kreira proizvod
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.USER)] // Samo korisnik sa ulogom USER može da kreira proizvod
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Edit - Prikazuje formu za izmenu proizvoda
        [AuthorizeRole(UserRole.USER)] // Samo korisnik sa ulogom USER može da vidi ovu stranicu
        public ActionResult Edit(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // Edit (POST) - Spasi izmenjeni proizvod
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.USER)] // Samo korisnik sa ulogom USER može da izmeni proizvod
        public ActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            var productInDb = _context.Products.SingleOrDefault(p => p.Id == product.Id);
            if (productInDb == null)
            {
                return HttpNotFound();
            }

            productInDb.Name = product.Name;
            productInDb.Price = product.Price;
            productInDb.Description = product.Description;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Delete - Prikazuje formu za brisanje proizvoda
        [AuthorizeRole(UserRole.USER)]
        [AuthorizeRole(UserRole.ADMIN)] // Samo korisnik sa ulogom USER može da vidi ovu stranicu
        public ActionResult Delete(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // Delete (POST) - Briše proizvod
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.USER)]
        [AuthorizeRole(UserRole.ADMIN)] 
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
