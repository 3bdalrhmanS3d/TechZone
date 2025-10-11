using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Consts;
using TechZone.Core.Entities;
using TechZone.Core.Interfaces;

namespace TechZone.EF.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(TechZone.EF.Application.ApplicationDbContext context) : base(context)
        {
        }

        public Task AddRangeAsync(IEnumerable<Order> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<Order> entities)
        {
            throw new NotImplementedException();
        }

        public Task<Order> FirstOrDefaultAsync(Expression<Func<Order, bool>> criteria, params Expression<Func<Order, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllAsync(params Expression<Func<Order, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByIdAsync(int id, params Expression<Func<Order, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> WhereAsync(Expression<Func<Order, bool>> criteria, int? skip = null, int? take = null, Expression<Func<Order, object>>? orderBy = null, OrderBy orderDirection = OrderBy.Ascending, params Expression<Func<Order, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
