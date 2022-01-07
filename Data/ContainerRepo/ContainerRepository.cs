using Data.Context;
using Data.DataModel;
using Data.Generic;
using Data.Result;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ContainerRepo
{
    public  class ContainerRepository : GenericRepository<Container>, IContainerRepository
    {
        public ContainerRepository(WasteSystemDbContext context, ILogger logger) : base(context, logger)
        {

        }

        public IEnumerable<Container> Where(System.Linq.Expressions.Expression<Func<Container, bool>> where)
        {
            return base.Where(where);
        }

        public async Task<Result<Container>> Add(Container entity)
        {
          return await base.Add(entity);    
        }

        public async Task<Result.Result> Delete(long id)
        {
            return await base.Delete(id); ;
        }

        public Task<Result<List<Container>>> GetAll()
        {
            return base.GetAll();
        }


        public Task<Result<Container>> Update(Container entity)
        {
            return base.Update(entity);
        }
    }
}
