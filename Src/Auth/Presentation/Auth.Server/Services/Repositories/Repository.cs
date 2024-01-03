using Auth.Server.Entities;
using Auth.Server.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Services.Repository {
    public class Repository<T> : IRepository<T> where T : class {
        private readonly UserContext _context;
        private DbSet<T> _entities;
        protected virtual DbSet<T> Entities => _entities ??= _context.Set<T>();
        public DbSet<T> Table => Entities;
        public IQueryable<T> TableNoTracking => Entities.AsNoTracking();
        public Repository(UserContext context) {
            _context = context;
        }
        public async Task<bool> Add(T entity) {
            await Entities.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Add(IList<T> entities) {
            await Entities.AddRangeAsync(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(T entity) {
            _context.Entry(entity).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(IList<T> entities) {
            _context.UpdateRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(T entity) {
            Entities.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(IList<T> entities) {
            Entities.RemoveRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
