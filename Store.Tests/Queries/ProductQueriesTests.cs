using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Queries;

namespace Store.Tests.Queries
{
    [TestClass]
    public class ProductQueriesTests
    {
        private IList<Product> _products;

        public ProductQueriesTests()
        {
            // Arrange
            _products = new List<Product>
            {
                new Product("Product 01", 10, true),
                new Product("Product 02", 20, true),
                new Product("Product 03", 30, true),
                new Product("Product 04", 40, false),
                new Product("Product 05", 50, false)
            };
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Given_query_for_active_products_should_return_3()
        {
            // Act
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());

            // Assert
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Given_query_for_inactive_products_should_return_2()
        {
            // Act
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());

            // Assert
            Assert.AreEqual(result.Count(), 2);
        }
    }
}
