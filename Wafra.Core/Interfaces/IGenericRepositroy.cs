
using System.Linq.Expressions;

namespace Wafra.Core.Interfaces
{
    public interface IGenericRepositroy<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null, string[] include = null);
        Task<T> FirstOrDefaultAsync(Expression<Func<T,bool>> filter = null, Func<IQueryable<T> , IOrderedQueryable<T>> order = null , string[] include = null);
        IQueryable<T> Sync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> order = null, string[] include = null);

        Task<bool> IsExist(Expression<Func<T, bool>> pre);
        Task<T> GetByIdAsync(int id);
        Task<bool> CreateAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
    }
}
