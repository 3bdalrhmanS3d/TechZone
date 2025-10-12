using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Domain.Entities;

namespace TechZone.Domain.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        // You can add Order-specific queries here later
    }
}
