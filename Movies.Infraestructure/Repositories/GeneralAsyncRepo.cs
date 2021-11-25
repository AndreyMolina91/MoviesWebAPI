using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.DataAccess.Context;
using Movies.Domain.IRepos;
using Movies.Infraestructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movies.Infraestructure.Repositories
{
    public class GeneralAsyncRepo<T> : IGeneralAsyncRepo<T> where T : class
    {
        /* 
         * Herramientas necesarias: 
         * Application Db Context.
         * DbSet de T
        */

        private readonly ApplicationDbContext _context;
        internal DbSet<T> _dbSet;

        public GeneralAsyncRepo(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddModel(T model)
        {
            await _dbSet.AddAsync(model);
        }

        public async Task<IEnumerable<T>> GetAllModel(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string includeproperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                //Select * from where filter = al parametro
                query = query.Where(filter).Take(5);
            }

            if (includeproperties != null)
            {
                //Include properties = al string
                foreach (var item in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            if (orderby != null)
            {
                //Orderby consulta de linq
                return await orderby(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetFirstModel(Expression<Func<T, bool>> filter = null, string includeproperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeproperties != null)
            {
                foreach (var item in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            //Objeto 1 de la lista
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetModelById(int id, string includeproperties = null)
        {
            IQueryable<T> query = _dbSet;

            if (includeproperties != null)
            {
                foreach (var item in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return await _dbSet.FindAsync(id);
        }

        public async Task RemoveModel(T model)
        {
            _dbSet.Remove(model);
            _context.SaveChanges();
        }

        public async Task RemoveModelById(int id)
        {
            T model = await _dbSet.FindAsync(id);
            await RemoveModel(model);
        }

        public async Task RemoveModelsRange(IEnumerable<T> modelList)
        {
            _dbSet.RemoveRange(modelList);
        }
    }
}
