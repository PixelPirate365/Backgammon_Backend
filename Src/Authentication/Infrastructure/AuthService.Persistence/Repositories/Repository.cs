using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Persistence.Repositories {
    public class Repository<T> : IRepository<T> where T : class {
        #region Private Members
        private readonly ApplicationDbContext _context;
        private DbSet<T> _entities;
        #endregion Private Members
        #region Properties

        protected virtual DbSet<T> Entities => _entities ??= _context.Set<T>();
        public DbSet<T> Table => Entities;
        public IQueryable<T> TableNoTracking => Entities.AsNoTracking();
        #endregion Properties
        #region Constructors
        public Repository(ApplicationDbContext context) {
            _context = context;
        }
        #endregion Constructors
        #region Methods
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
        #endregion Methods
    }
}
