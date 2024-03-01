using System;
using System.Collections.Generic;
using api.Model;
using api.Storage;
using api.Service;

public class ProductService : IProductService
{
    private readonly IProductRepository _storage;

    public ProductService(IProductRepository storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public List<ProductModel> GetAllProducts()
    {
        return _storage.GetAll();
    }

    public ProductModel GetProductById(int id)
    {
        return _storage.GetById(id);
    }

    public List<ProductModel> CreateProducts(List<ProductModel> newProducts)
    {
        _storage.Create(newProducts);
        return _storage.GetByOrderDesc(newProducts.Count);
    }

    public ProductModel UpdateProduct(ProductModel updatedProduct)
    {
        _storage.Update(updatedProduct);
        return _storage.GetById(updatedProduct.Id);
    }

    public ProductModel DeleteProductById(int productId)
    {
        var product = _storage.GetById(productId);
        _storage.Delete(productId);
        return product;
    }
}