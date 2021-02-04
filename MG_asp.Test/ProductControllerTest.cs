using MG_asp.Controllers;
using MG_asp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MG_asp.test
{
    public class ProductControllerTest
    {
        [Fact]
        public void GetProductByIdTest()
        {
            //Arrange — tworzenie imitacji repozytorium.
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
            }.AsQueryable<Product>());

            var controller = new ProductController(productRepositoryMock.Object);

            //Act — utworzenie kontrolera.
            var result = controller.GetById(1);
            Product product = result.ViewData.Model as Product;

            //Assert
            Assert.Equal("P1", product.Name);
        }
        [Theory]
        [InlineData(1, "P1")]
        [InlineData(2, "P2")]
        public void GetProductsByIdTest(int id, string expectedName)
        {
            //Arrange
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.Products).Returns(new Product[] {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"}
            }.AsQueryable<Product>());

            var controller = new ProductController(productRepositoryMock.Object);

            //Act
            var result = controller.GetById(id);
            Product product = result.ViewData.Model as Product;

            //Assert
            Assert.Equal(expectedName, product.Name);
        }
        [Fact]
        public void GetAllProducts()
        {
            //Arrange
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1"},
                new Product {ProductID = 2, Name = "P2"},
                new Product {ProductID = 3, Name = "P3"},
                new Product {ProductID = 4, Name = "P4"},
                new Product {ProductID = 5, Name = "P5"},
                new Product {ProductID = 6, Name = "P6"},
            }.AsQueryable<Product>());
            var controller = new ProductController(productRepositoryMock.Object);
            //Act
            Product[] result = GetViewModel<IEnumerable<Product>>(controller.ListAll()).ToArray();

            //Assert
            Assert.Equal(6, result.Length);
            Assert.True(result[0].Name == "P1");
            Assert.True(result[result.Length - 1].Name == "P6");
        }
        [Fact]
        public void GetCategoryProducts()
        {
            //Arrange
            Mock<IProductRepository> productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category="C1"},
                new Product {ProductID = 2, Name = "P2", Category="C2"},
                new Product {ProductID = 3, Name = "P3", Category="C3"},
                new Product {ProductID = 4, Name = "P4", Category="C2"},
                new Product {ProductID = 5, Name = "P5", Category="C2"},
                new Product {ProductID = 6, Name = "P6", Category="C3"},
            }.AsQueryable<Product>());

            //utworzenie kontrolera i ustawienie 3-elementowej strony.
            var controller = new ProductController(productRepositoryMock.Object);
            //Act
            Product[] result = GetViewModel<IEnumerable<Product>>(controller.List("C2")).ToArray();

            //Assert
            Assert.Equal(3, result.Length);
            Assert.True(result[0].Name == "P2" && result[1].Category == "C2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "C2");
            Assert.True(result[2].Name == "P5" && result[1].Category == "C2");
        }




        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
