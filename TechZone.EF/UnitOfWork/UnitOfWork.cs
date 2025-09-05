using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechZone.Core.Interfaces;
using TechZone.EF.Application;

namespace TechZone.EF.UnitOfWork
{
    internal class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {


        // Transaction Methods
        public Task<IDbContextTransaction> BeginTransactionAsync() => context.Database.BeginTransactionAsync();

        public int Complete() => context.SaveChanges();

        public Task<int> CompleteAsync() => context.SaveChangesAsync();

        public void Dispose() => context.Dispose();

        public Task<int> SaveAsync() => context.SaveChangesAsync();
        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
    }
}
