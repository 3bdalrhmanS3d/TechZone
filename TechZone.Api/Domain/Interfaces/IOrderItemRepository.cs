using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Domain.Entities;
using TechZone.Domain.Entities.Order;

namespace TechZone.Domain.Interfaces
{
    public interface IOrderItemRepository : IBaseRepository<OrderItem>
    {
        // You can add OrderItem-specific queries here later
    }
}
