using System.Linq.Expressions;


namespace Wafra.Application.Contracts.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        Task<List<T>> GetALLAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includes = null);
        IQueryable<T> Sync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includes = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] includes = null);
        Task<T> GetByIdAsync(int id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<bool> IsExist(Expression<Func<T, bool>> pre);
    }
}
