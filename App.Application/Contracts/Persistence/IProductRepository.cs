

using App.Domain.Entities;

namespace App.Application.Contracts.Persistence
{
    //IGenericRepository'deki CRUD'lar yetmediyse IGenericRepository'i implemente edip burda ekstra metodlar yazabilirim.
    public interface IProductRepository:IGenericRepository<Product>
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count);
    }
}
