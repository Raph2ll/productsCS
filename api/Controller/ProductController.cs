using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Model;

namespace api.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

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
                // Tratar exceções conforme necessário
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        [HttpPost]
        public ActionResult CreateProduct(List<ProductModel> newProducts)
        {
            try
            {
                _productService.CreateProducts(newProducts);
                return Ok(newProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}
