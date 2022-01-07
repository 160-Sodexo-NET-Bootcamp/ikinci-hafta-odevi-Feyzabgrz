using Data.Context;
using Data.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ILogger logger;
        protected readonly WasteSystemDbContext context;
        internal DbSet<T> dbSet;

        public GenericRepository(WasteSystemDbContext context, ILogger logger)
        {
           this.logger = logger;   
           this.context = context; 

            dbSet=context.Set<T>(); 

        }



        public async Task<bool> Add(T entity)
        {
            var result = await dbSet.AddAsync(entity);
            return true;
        }

        //public  Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
        //{
        //    return dbSet.Where(predicate);
        //}

        public async Task<bool> Delete(long id)
        {
            var entity  = await dbSet.FindAsync(id);
            
            var result = dbSet.Remove(entity);
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

   
        public  IEnumerable<T> Where(System.Linq.Expressions.Expression<Func< T, bool>> where)
        {
            return dbSet.Where(where).AsQueryable();
        }

        public async Task<T> GetById(long id)
        {
            var model = await dbSet.FindAsync(id);
            return model;

        }
      

        public async Task<bool> Update(T entity)
        {

            var model =  dbSet.Update(entity);
            return true;

        }
    }
}
