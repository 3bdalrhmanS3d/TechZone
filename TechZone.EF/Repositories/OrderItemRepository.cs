using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Consts;
using TechZone.Core.Entities.Order;
using TechZone.Core.Interfaces;
using TechZone.EF.Application;

namespace TechZone.EF.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public Task AddRangeAsync(IEnumerable<OrderItem> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<OrderItem> entities)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> FirstOrDefaultAsync(Expression<Func<OrderItem, bool>> criteria, params Expression<Func<OrderItem, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderItem>> GetAllAsync(params Expression<Func<OrderItem, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<OrderItem> GetByIdAsync(int id, params Expression<Func<OrderItem, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderItem>> WhereAsync(Expression<Func<OrderItem, bool>> criteria, int? skip = null, int? take = null, Expression<Func<OrderItem, object>>? orderBy = null, OrderBy orderDirection = OrderBy.Ascending, params Expression<Func<OrderItem, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
