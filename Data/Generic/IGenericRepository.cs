using Data.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<Result<T>> Add(T entity);
        Task<Result<T>> Update(T entity);
        Task<Result.Result> Delete(long id);
        Task<Result<T>> GetById(long id);
        Task<Result<List<T>>> GetAll ();
        IEnumerable<T> Where(System.Linq.Expressions.Expression<Func<T, bool>> where);
    }
}
