using MG_asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MG_asp.Controllers


{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index() => View(repository.Products); // wyświetla wszystkie produkty do widoku przekazujemy model ktory jest kolekcja 

        public ViewResult Edit(int productId) =>                 // przyjmuje productId i podaje widok w ktorym przekazywany jest model z tego produktu
            View(repository.Products
                .FirstOrDefault(p => p.ProductID  == productId));

        [HttpPost]

        public IActionResult Save(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"Zapisano {product.Name}."; // TempData przekazuje komunikat na widok
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", product);
            }
        }

        public ViewResult Create() => View("Edit", new Product()); // przekazywanyy jest widok do edycji ale przekazywany jest new prodact (Edycja nowego produktu)

        [HttpPost]

        public IActionResult Delete(int productId)
        {
            Product deleteProduct = repository.DeleteProduct(productId);
            if (deleteProduct != null)
            {
                TempData["message"] = $"Usunięto {deleteProduct.Name}";
            }
            return RedirectToAction("Index"); // po wykonaniu jest przekierowanie do ekranu z listą produktów
        }

    }
}