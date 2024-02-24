using api.Model;
namespace api.Storage
{
    public interface IProductRepository
    {
        List<ProductModel> GetAll();
        ProductModel GetById(int id);
        List<ProductModel> GetByOrderDesc(int quantity);
        void Create(List<ProductModel> product);
        void Update(ProductModel product);
        void Delete(int id);
    }
}