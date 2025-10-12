using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechZone.Domain.Consts;

namespace TechZone.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> WhereAsync(
            Expression<Func<T, bool>> criteria,
            int? skip = null,
            int? take = null,
            Expression<Func<T, object>>? orderBy = null,
            OrderBy orderDirection = OrderBy.Ascending,
            params Expression<Func<T, object>>[] includes);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

        Task<int> CountAsync(Expression<Func<T, bool>>? criteria = null);
    }
}