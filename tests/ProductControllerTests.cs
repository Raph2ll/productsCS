using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using api.Storage;
using api.Controller;
using api.Model;

namespace tests
{
    [TestClass]
    public class ProductControllerTests
    {
        // Get All Products
        [TestMethod]
        public void GetAllProducts_ReturnsListOfProducts()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var productServiceMock = new Mock<ProductService>(productRepositoryMock.Object);

            productServiceMock.Setup(x => x.GetAllProducts()).Returns(new List<ProductModel>());
            var productController = new ProductController(productServiceMock.Object);

            // Act
            var result = productController.GetAllProducts().Result;

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result;

            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ProductModel>));
        }
    }
}