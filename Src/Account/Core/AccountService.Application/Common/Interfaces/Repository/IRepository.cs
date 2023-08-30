using Microsoft.EntityFrameworkCore;

namespace AccountService.Application.Common.Interfaces.Repository
{
    public interface IRepository<T> where T : class {
        Task<bool> Add(T entity);

        Task<bool> Add(IList<T> entities);

        Task<bool> Update(T entity);

        Task<bool> Update(IList<T> entities);

        Task<bool> Delete(T entity);

        Task<bool> Delete(IList<T> entities);
        DbSet<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }
}
