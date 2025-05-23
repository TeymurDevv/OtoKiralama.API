﻿using System.Linq.Expressions;

namespace OtoKiralama.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetEntity(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, params Func<IQueryable<T>, IQueryable<T>>[] includes);
        Task<List<T>> GetAll(Expression<Func<T, bool>> predicate = null, int skip = 0, int take = 0, params Func<IQueryable<T>, IQueryable<T>>[] includes);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task<bool> isExists(Expression<Func<T, bool>> predicate = null);
        Task Commit();
        Task<IQueryable<T>> GetQuery(
            Expression<Func<T, bool>> predicate = null, bool AsnoTracking = false, bool AsSplitQuery = false,
            params Func<IQueryable<T>, IQueryable<T>>[] includes);
    }
}
