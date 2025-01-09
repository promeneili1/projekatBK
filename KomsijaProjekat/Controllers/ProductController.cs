using KomsijaProjekat.Authorize;
using KomsijaProjekat.Data;
using KomsijaProjekat.Models;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System;



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
        public ActionResult Index(int? page)
        {
            var pageNumber = page ?? 1;
            int pageSize = 4; 

            var products = _context.Products.OrderBy(p => p.Name).ToList(); 

            var totalItems = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var productsForPage = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(productsForPage); 
        }

        [AuthorizeRole(UserRole.USER)] 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.USER)] 
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

        [AuthorizeRole(UserRole.USER)] 
        public ActionResult Edit(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.USER)] 
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

        
        [AuthorizeRole(UserRole.USER)]
        public ActionResult Delete(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.USER)]
        
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
