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

        // GET: Brand
        public ActionResult Index()
        {
            var brands = _brandRepository.GetAll();
            return View(brands);
        }

        // GET: Brand/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brand/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Verifikacija Anti-Forgery tokena
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.Add(brand);
                return RedirectToAction("Index"); // Preusmeravanje na listu brandova (ako postoji)
            }

            return View(brand); // Ako podaci nisu validni, vrati korisnika na formu
        }

        // GET: Brand/Edit/5
        public ActionResult Edit(int id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brand/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Brand brand)
        {
            if (ModelState.IsValid)
            {
                _brandRepository.Update(brand);
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        // GET: Brand/Delete/5
        public ActionResult Delete(int id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand == null)
            {
                return HttpNotFound();
            }
            return View(brand);
        }

        // POST: Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _brandRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
