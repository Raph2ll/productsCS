using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using api.Data;
using api.Model;
using System.Data.Common;
using api.Storage;

public class ProductService
{
    private readonly IProductRepository _storage;

    public ProductService(IProductRepository storage)
    {
        _storage = storage;
    }

    public List<ProductModel> GetAllProducts()
    {
        return _storage.GetAll();
    }
    public void CreateProducts(List<ProductModel> newProducts)
    {
        _storage.Create(newProducts);
        // return newProducts;
    }
    public void UpdateProduct(ProductModel updatedProduct)
    {
        _storage.Update(updatedProduct);
        // return updatedProduct;
    }
    public void DeleteProductById(int productId)
    {
        _storage.Delete(productId);
        // return _storage.GetById(productId);
    }


}