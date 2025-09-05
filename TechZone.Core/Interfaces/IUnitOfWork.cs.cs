using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechZone.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        // Transaction Methods
        Task<int> CompleteAsync();
        int Complete();
        Task<int> SaveAsync();
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
