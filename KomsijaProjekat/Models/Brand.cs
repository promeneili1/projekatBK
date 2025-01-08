using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KomsijaProjekat.Models
{
    public class Brand
    {
        public int Id { get; set; }  
        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; }
    }
}