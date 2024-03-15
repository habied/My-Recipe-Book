using System.Linq.Expressions;

namespace RecipeBook.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<TDto> GetByIdAsync<TDto>(Expression<Func<T, bool>> condition, Expression<Func<T, TDto>> projection, CancellationToken token);
        Task<T> GetByIdWithIncludesAsync(Expression<Func<T, bool>> condition, CancellationToken token, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<TDto>> GetAllAsync<TDto>(Expression<Func<T, TDto>> projection, CancellationToken token);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        public static class OrderBy
        {
            public const string Ascending = "ASC";
            public const string Descending = "DESC";
        }
    }
}