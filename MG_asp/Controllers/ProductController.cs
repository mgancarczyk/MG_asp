using MG_asp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MG_asp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository repository)
        {
            this.productRepository = repository;
        }
        public ViewResult ListAll() => View(productRepository.Products); // zwraca liste produktów
        public ViewResult List(string category) => View(productRepository.Products.Where(p => p.Category == category)); // przyjmuje wartość string po kategorii za pomocą linku zwracam pordukty
        public ViewResult GetById(int id) => View(productRepository.Products.Single(p => p.ProductID == id));

        public IActionResult Index()
        {
            return View();
        }
    }
}
