using Graphite.Database;
using Graphite.Source.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Graphite.Source.Infrastructure.Repositories
{
    public class Repository<T>(ApplicationDatabaseContext context) : IRepository<T> where T : class
    {
        private readonly ApplicationDatabaseContext _context = context;

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
