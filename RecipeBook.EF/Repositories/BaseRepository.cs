using Microsoft.EntityFrameworkCore;
using RecipeBook.Core.Interfaces;
using System.Linq.Expressions;

namespace RecipeBook.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected RecipeBookDBContext _context;

        public BaseRepository(RecipeBookDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TDto>> GetAllAsync<TDto>(Expression<Func<T, TDto>> projection, CancellationToken token)
        {
            return await _context.Set<T>().Select(projection).ToListAsync(token);
        }

        public async Task<TDto> GetByIdAsync<TDto>(Expression<Func<T, bool>> condition, Expression<Func<T, TDto>> projection, CancellationToken token)
        {
            return await _context.Set<T>().Where(condition).Select(projection).FirstOrDefaultAsync(token);
        }

        public async Task<T> GetByIdWithIncludesAsync(Expression<Func<T, bool>> condition, CancellationToken token, params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(condition).FirstOrDefaultAsync(token);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public T Update(T entity)
        {
            _context.Update(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
    }
}