using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wafra.Application.Contracts.Interfaces;
using Wafra.Infrastructure.Data;
namespace Wafra.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] include = null)
        {
            var result = Sync(filter, orderBy, include);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetALLAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] include = null)
        {
            var result = Sync(filter,orderBy,include);
            return await result.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<bool> IsExist(Expression<Func<T, bool>> pre)
        {
            return await _context.Set<T>().AnyAsync(pre);
        }

        public IQueryable<T> Sync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string[] include = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if(filter != null)
                query = query.Where(filter);
            if(include != null)
                foreach(var item in include)
                    query = query.Include(item);
            if (orderBy != null)
                orderBy(query);
            return query;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}