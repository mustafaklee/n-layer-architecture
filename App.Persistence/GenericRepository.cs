using App.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Persistence
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        protected AppDbContext Context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();
        public async ValueTask AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        //veriler sadece okunacak ve değiştirilmeyecekse asnotracking()
        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable().AsNoTracking();
        }
        //burdan dönen değer nullable olabilir o yüzden ? koyulur.
        public ValueTask<T?> GetByIdAsync(int id)
        {
            return _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking(); 
        }

        public Task<List<T>> GetAllAsynce()
        {
            return _dbSet.ToListAsync();
        }

        public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize)
        {
            return _dbSet.Skip((1 - pageNumber) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
