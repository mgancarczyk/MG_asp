using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MG_asp.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać dodatnią cenę.")]

        public decimal Price { get; set; }

        [Display(Name = "Kategoria")]

        public string Category { get; set; }
    }
}
