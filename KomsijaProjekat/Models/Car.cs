using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomsijaProjekat.Models
{
    public class Car
    {
        public int Id { get; set; }  
        public string Model { get; set; }  
        public int Year { get; set; }  
        public int BrandId { get; set; } 
        public Brand Brand { get; set; }  
    }
}