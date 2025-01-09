    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
using KomsijaProjekat.Authorize;
using KomsijaProjekat.Models;
    using KomsijaProjekat.Repository;

    namespace KomsijaProjekat.Controllers
    {
        public class CarController : Controller
        {
            private readonly CarRepository _carRepository;
            private readonly BrandRepository _brandRepository;

            public CarController()
            {
                _carRepository = new CarRepository();
                _brandRepository = new BrandRepository();
            }

            public ActionResult Index()
            {
                var cars = _carRepository.GetAll();
                return View(cars);
            }

            
            public ActionResult Add()
            {
                return View();
            }
        
        [AuthorizeRole(UserRole.ADMIN)]
        public ActionResult Create()
            {
                var brands = _brandRepository.GetAll();  
                ViewBag.Brands = new SelectList(brands, "Id", "Name");  
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.ADMIN)]
        public ActionResult Create(Car car)
            {
                if (ModelState.IsValid)
                {
                    _carRepository.Add(car);
                    return RedirectToAction("Index");
                }

                ViewBag.Brands = new SelectList(_brandRepository.GetAll(), "Id", "Name", car.BrandId);
                return View(car);
            }

        [AuthorizeRole(UserRole.ADMIN)]
        public ActionResult Edit(int id)
            {
                var car = _carRepository.GetById(id);
                if (car == null)
                {
                    return HttpNotFound();
                }

                ViewBag.Brands = new SelectList(_brandRepository.GetAll(), "Id", "Name", car.BrandId);
                return View(car);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
        [AuthorizeRole(UserRole.ADMIN)]
        public ActionResult Edit(int id, Car car)
            {
                if (ModelState.IsValid)
                {
                    car.Id = id; 
                    _carRepository.Update(car);
                    return RedirectToAction("Index");
                }

                ViewBag.Brands = new SelectList(_brandRepository.GetAll(), "Id", "Name", car.BrandId);
                return View(car);
            }

        [AuthorizeRole(UserRole.ADMIN)]
        public ActionResult Delete(int id)
            {
                var car = _carRepository.GetById(id);
                if (car == null)
                {
                    return HttpNotFound();
                }

                return View(car);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [AuthorizeRole(UserRole.ADMIN)] 
            public ActionResult Delete(int id, FormCollection collection)
            {
                _carRepository.Delete(id);
                return RedirectToAction("Index");
            }
        }
    }
