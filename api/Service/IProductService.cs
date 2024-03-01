using api.Model;

namespace api.Service
{
    public interface IProductService
    {
        List<ProductModel> GetAllProducts();
        ProductModel GetProductById(int id);
        List<ProductModel> CreateProducts(List<ProductModel> newProducts);
        ProductModel UpdateProduct(ProductModel updatedProduct);
        ProductModel DeleteProductById(int productId);
    }
}