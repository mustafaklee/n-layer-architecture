using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Products
{
    //IGenericRepository'deki CRUD'lar yetmediyse IGenericRepository'i implemente edip burda ekstra metodlar yazabilirim.
    public interface IProductRepository:IGenericRepository<Product>
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count);
    }
}
