﻿using Microsoft.EntityFrameworkCore;
using OtoKiralama.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtoKiralama.Persistance.Data.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public Repository(AppDbContext context)
        {
            _context = context;
            _table = context.Set<T>();
        }



        public async Task Commit()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task Create(T entity)
        {

            try
            {
                var result = _context.Entry(entity);
                result.State = EntityState.Added;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(T entity)
        {

            try
            {
                var result = _context.Entry(entity);
                result.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<T>> GetAll(
       Expression<Func<T, bool>> predicate = null,
       int skip = 0, int take = 0,
       params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            try
            {
                IQueryable<T> query = _table;

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = include(query);
                    }
                }

                // Apply the predicate if provided
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                // Apply skip and take logic
                if (skip > 0)
                {
                    query = query.Skip(skip);
                }

                if (take > 0)
                {
                    query = query.Take(take);
                }

                // Execute the query
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<T> GetEntity(
      Expression<Func<T, bool>> predicate = null,
      int skip = 0, int take = 0,
      params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            try
            {
                IQueryable<T> query = _table;

                if (includes != null)
                {
                    foreach (var include in includes)
                    {
                        query = include(query);
                    }
                }

                // Apply the predicate first if provided
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                // Apply skip and take after the filtering
                if (skip > 0)
                {
                    query = query.Skip(skip);
                }

                if (take > 0)
                {
                    query = query.Take(take);
                }

                // Return the first entity that matches
                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<bool> isExists(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                return predicate is null ? false : await _table.AnyAsync(predicate);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                var result = _context.Entry(entity);
                result.State = EntityState.Modified;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
