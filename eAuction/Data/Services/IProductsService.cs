using eAuction.Models;

namespace eAuction.Data.Services
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductModel>> GetAllAsync();

        Task<ProductModel> GetByIdAsync(int id);

        Task AddAsync(ProductModel product);

        Task<ProductModel> UpdateAsync(int id, ProductModel product);

        Task DeleteAsync(int id);

    }
}
