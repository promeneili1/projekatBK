using KomsijaProjekat.Authorize;
using KomsijaProjekat.Models;
using KomsijaProjekat.Repository;
using System.Web.Mvc;

namespace KomsijaProjekat.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandRepository _brandRepository;

        public BrandController()
        {
            _brandRepository = new BrandRepository();
        }

        public ActionResult Index()
        {
            var brands = _brandRepository.GetAll();
            return View(brands);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.ADMIN)] 
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.Add(brand);
                return RedirectToAction("Index"); 
            }

            return View(brand); 
        }

        [AuthorizeRole(UserRole.ADMIN)]
        public ActionResult Edit(int id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.ADMIN)]
        public ActionResult Edit(int id, Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.Update(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        [AuthorizeRole(UserRole.ADMIN)]
        public ActionResult Delete(int id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.ADMIN)]
        public ActionResult DeleteConfirmed(int id)
        {
            _brandRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
