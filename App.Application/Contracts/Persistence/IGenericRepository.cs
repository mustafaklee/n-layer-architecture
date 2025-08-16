using System.Linq.Expressions;

namespace App.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T:class
    {
        //valuetask  task'a göre bellekte daha az yer kaplar.
        Task<List<T>> GetAllAsynce();
        Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize);
        IQueryable<T> Where(Expression<Func<T,bool>> predicate);
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
