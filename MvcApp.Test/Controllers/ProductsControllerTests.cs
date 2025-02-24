using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcApp.Controllers;
using MvcApp.Models;
using PagedList;
using System.Data.Entity;


namespace MvcApp.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTests
    {
        // Create a fake repository of products
        private List<Product> GetTestProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Apple", Price = 1.99m },
                new Product { Id = 2, Name = "Banana", Price = 0.99m },
                new Product { Id = 3, Name = "Carrot", Price = 0.79m }
            };
        }

        [TestMethod]
        public void Index_Returns_View_With_PagedList()
        {
            // Arrange: Set up a fake context and controller.
            var testProducts = GetTestProducts().AsQueryable();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(testProducts.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(testProducts.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(testProducts.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(testProducts.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Products).Returns(mockSet.Object);

            var controller = new ProductsController();
            // Replace the db context with our mock context:
            typeof(ProductsController)
                .GetField("db", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(controller, mockContext.Object);

            // Act
            var result = controller.Index(null, null, 1) as ViewResult;
            var model = result.Model as IPagedList<Product>;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(model);
            Assert.AreEqual(3, model.TotalItemCount);
        }
    }
}
