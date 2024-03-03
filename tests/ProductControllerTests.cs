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
        public void Initialize()
        {
            productServiceMock = new Mock<IProductService>();
            controller = new ProductController(productServiceMock.Object);
        }

        [TestMethod]
        public void GetProductById_ReturnsProduct()
        {
            // Arrange
            int productId = 1;
            var product = new ProductModel { Id = productId, Name = "Test Product" };
            productServiceMock.Setup(x => x.GetProductById(productId)).Returns(product);

            // Act
            var result = controller.GetProductById(productId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<ProductModel>));
            var actionResult = (ActionResult<ProductModel>)result;
            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(ProductModel));
            Assert.AreEqual(productId, (okResult.Value as ProductModel).Id);
        }

        [TestMethod]
        public void CreateProduct_ReturnsCreatedProducts()
        {
            // Arrange
            var newProducts = new List<ProductModel> { new ProductModel { Name = "New Product" } };
            productServiceMock.Setup(x => x.CreateProducts(newProducts)).Returns(newProducts);

            // Act
            var result = controller.CreateProduct(newProducts);

            // Assert
            Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
            var statusCodeResult = (StatusCodeResult)result;

            Assert.AreEqual(201, statusCodeResult.StatusCode);
        }

        [TestMethod]
        public void UpdateProduct_ReturnsUpdatedProduct()
        {
            // Arrange
            int productId = 1;
            var updatedProduct = new ProductModel { Id = productId, Name = "Updated Product" };
            productServiceMock.Setup(x => x.GetProductById(productId)).Returns(updatedProduct);

            // Act
            var result = controller.UpdateProduct(productId, updatedProduct);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<ProductModel>));
            var actionResult = (ActionResult<ProductModel>)result;
            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(ProductModel));
            Assert.AreEqual(productId, (okResult.Value as ProductModel).Id);
            Assert.AreEqual("Updated Product", (okResult.Value as ProductModel).Name);
        }

        [TestMethod]
        public void DeleteProduct_ReturnsDeletedProduct()
        {
            // Arrange
            int productId = 1;
            var productToDelete = new ProductModel { Id = productId, Name = "Product to Delete" };
            productServiceMock.Setup(x => x.GetProductById(productId)).Returns(productToDelete);

            // Act
            var result = controller.DeleteProduct(productId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult<ProductModel>));
            var actionResult = (ActionResult<ProductModel>)result;
            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.IsInstanceOfType(okResult.Value, typeof(ProductModel));
            Assert.AreEqual(productId, (okResult.Value as ProductModel).Id);
            Assert.AreEqual("Product to Delete", (okResult.Value as ProductModel).Name);
        }
    }
}
