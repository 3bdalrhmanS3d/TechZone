using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Entities;
using TechZone.Core.Entities.Order;

namespace TechZone.Core.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        // You can add OrderItem-specific queries here later
    }
}
