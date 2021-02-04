using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MG_asp.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly AppDbContext ctx;

        public EFProductRepository(AppDbContext ctx) // przekazujemy kontekst bazodanowy 
        {
            this.ctx = ctx;
        }
        public IQueryable<Product> Products => ctx.Products; // zwracamy kolekcje produktów z kontekstu

        public void SaveProduct(Product product )
        {
            if (product.ProductID == 0)
            {
                ctx.Products.Add(product); // jeśli ID jest 0 to znaczy że produkt jest dodawany
            }
            else
            {
                Product dbEntry = ctx.Products // jeśli jest inny to należy zmodyfikować
                    .FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            ctx.SaveChanges();

        }

        public Product DeleteProduct(int productID)
        {
            Product dbEntry = ctx.Products
                .FirstOrDefault(p => p.ProductID == productID); //pobieranie kolekcji atrybutów 
            if (dbEntry != null)
            {
                ctx.Products.Remove(dbEntry); // usunąć z kolekcji
                ctx.SaveChanges(); // wywołanie na kontekscie baz danych
            }
            return dbEntry;
        }
    }
}
