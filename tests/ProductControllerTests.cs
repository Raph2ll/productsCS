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
        private Mock<IProductService> productServiceMock;
        private ProductController controller;

        [TestInitialize]
        public void TestInitialize()
        {
            // Configurar o Mock e o Controller antes de cada teste
            productServiceMock = new Mock<IProductService>();
            controller = new ProductController(productServiceMock.Object);
        }
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
        [TestMethod]
        public void CreateProduct_ReturnsCreatedProducts()
        {
            // Arrange
            var newProducts = new List<ProductModel> { new ProductModel {
                Name = "New Product",
                Price = 10.99
            } };
            productServiceMock.Setup(x => x.CreateProducts(newProducts)).Returns(newProducts);

            // Act
            var result = controller.CreateProduct(newProducts);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObjectResult));
            var objectResult = (ObjectResult)result;

            Assert.AreEqual(201, objectResult.StatusCode);

            var createdProducts = objectResult.Value as List<ProductModel>;
            Assert.IsNotNull(createdProducts);
            Assert.IsTrue(createdProducts.Any());
        }
    }
}