using Data.Context;
using Data.DataModel;
using Data.Result;
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



        public async Task<Result<T>> Add(T entity)
        {


            var result = new Result<T>();
            try
            {
                var model = await dbSet.AddAsync(entity);

                result.Data = entity;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                throw;
            }
            return result;
        }

      
        public async Task<Result.Result> Delete(long id)
        {
            var result = new Result.Result();
            try
            {
                var entity = await dbSet.FindAsync(id);
                var model = dbSet.Remove(entity);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                throw;
            }
            return result;
        }


        
        public async Task<Result<List<T>>> GetAll()
        {
            //hata mesajlarını rahat okuyabilmek için result yapısı oluşturdum.

            var result = new  Result<List<T>>();
            try
            {
                var model = await dbSet.ToListAsync();

                result.Data = model;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                throw;
            }
            return result;
        }

   
        public  IEnumerable<T> Where(System.Linq.Expressions.Expression<Func< T, bool>> where)
        {
            return dbSet.Where(where).AsQueryable();
        }

        public async Task<Result<T>> GetById(long id)
        {
            //hata mesajlarını rahat okuyabilmek için result yapısı oluşturdum.

            var result = new Result<T>();

            try
            {
                var model = await dbSet.FindAsync(id);

                result.Data = model;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                throw;
            }
            return result;

        }
      

        public async Task<Result<T>> Update(T entity)
        {

            //hata mesajlarını rahat okuyabilmek için result yapısı oluşturdum.

            var result = new Result<T>();
          
            try
            {
                var model = dbSet.Update(entity);
                result.Data = entity;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                throw;
            }
            return result;

        }

    }
}
