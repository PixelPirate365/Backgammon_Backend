﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Interfaces.Repository {
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
