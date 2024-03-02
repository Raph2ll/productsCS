using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using api.Service;
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
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.GetAllProducts()).Returns(new List<ProductModel>());

            var controller = new ProductController(productServiceMock.Object);

            // Act
            var result = controller.GetAllProducts();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<ProductModel>>));
            var actionResult = (ActionResult<IEnumerable<ProductModel>>)result;
            
            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<ProductModel>));
        }
    }
}
