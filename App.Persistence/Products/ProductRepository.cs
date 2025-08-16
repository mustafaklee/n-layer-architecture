using App.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using App.Domain.Entities;
namespace App.Persistence.Products
{
    internal class ProductRepository(AppDbContext context) : GenericRepository<Product>(context), IProductRepository
    {
        public Task<List<Product>> GetTopPriceProductAsync(int count)
        {
            return Context.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
        }
    }
}
