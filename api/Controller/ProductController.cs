using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Model;
using api.Service;

namespace api.Controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> GetAllProducts()
        {
            try
            {
                var products = _productService.GetAllProducts();
                return Ok(products);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public ActionResult CreateProduct(List<ProductModel> newProducts)
        {
            try
            {
                var products = _productService.CreateProducts(newProducts);
                return StatusCode(201, products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, ProductModel updatedProduct)
        {
            try
            {
                if (_productService.GetProductById(id) == null)
                {
                    return NotFound("Product Not Found");
                }

                updatedProduct.Id = id;
                _productService.UpdateProduct(updatedProduct);

                return Ok(updatedProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                if (_productService.GetProductById(id) == null)
                {
                    return NotFound("Product Not Found");
                }

                var product = _productService.DeleteProductById(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
