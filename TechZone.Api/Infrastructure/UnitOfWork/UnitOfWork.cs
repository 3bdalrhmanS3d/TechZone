using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;
using TechZone.Domain.Interfaces;
using TechZone.Infrastructure.Application;
using TechZone.Infrastructure.Repositories;

namespace TechZone.EF.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        // Repository instances
        public ILaptopRepository Laptops { get; }
        public IOrderRepository Orders { get; }
        public IOrderItemRepository OrderItems { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            // Initialize repositories
            Laptops = new LaptopRepository(_context);
            Orders = new OrderRepository(_context);
            OrderItems = new OrderItemRepository(_context);
        }

        // Transaction methods
        public Task<IDbContextTransaction> BeginTransactionAsync()
            => _context.Database.BeginTransactionAsync();

        public int Complete() => _context.SaveChanges();

        public Task<int> CompleteAsync() => _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
