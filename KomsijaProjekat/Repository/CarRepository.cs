using System.Collections.Generic;
using System.Linq;
using KomsijaProjekat.Models;
using KomsijaProjekat.Data;
using System.Web.Mvc;

namespace KomsijaProjekat.Repository
{
    public class CarRepository : IRepository<Car>
    {
        private readonly AppDbContext _context;

        public CarRepository()
        {
            _context = new AppDbContext();
        }

        public void Add(Car entity)
        {
            _context.Cars.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }

        public Car GetById(int id)
        {
            return _context.Cars.Find(id);
        }

        public IEnumerable<Car> GetAll()
        {
            var cars = _context.Cars.ToList();

            foreach (var car in cars)
            {
                car.Brand = _context.Brands.FirstOrDefault(b => b.Id == car.BrandId);
            }

            return cars;
        }

        



        public void Update(Car entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        IEnumerable<Car> IRepository<Car>.GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
