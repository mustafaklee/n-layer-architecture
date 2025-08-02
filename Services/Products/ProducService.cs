using App.Repositories.Products;
namespace App.Services.Products
{
    public class ProducService(IProductRepository productRepository):IProductService
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count)
        {
            return productRepository.GetTopPriceProductAsync(count);
        }
    }
}
