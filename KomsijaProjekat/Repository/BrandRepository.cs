using KomsijaProjekat.Models;
using KomsijaProjekat.Data;
using System.Collections.Generic;
using System.Linq;

namespace KomsijaProjekat.Repository
{
    public class BrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository()
        {
            _context = new AppDbContext();
        }

        // Get all brands
        public IEnumerable<Brand> GetAll()
        {
            return _context.Brands.ToList();
        }

        // Get brand by ID
        public Brand GetById(int id)
        {
            return _context.Brands.Find(id);
        }



        // Add brand
        public void Add(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        // Update brand
        public void Update(Brand brand)
        {
            _context.Entry(brand).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        // Delete brand
        public void Delete(int id)
        {
            var brand = _context.Brands.Find(id);
            if (brand != null)
            {
                _context.Brands.Remove(brand);
                _context.SaveChanges();
            }
        }
    }
}
