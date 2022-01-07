using Data.Context;
using Data.DataModel;
using Data.Generic;
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

        public async Task<bool> Add(Container entity)
        {
          return await base.Add(entity);    
        }

        public async Task<bool> Delete(long id)
        {
           var result = await base.Delete(id);
            return true;
        }

        public Task<IEnumerable<Container>> GetAll()
        {
            return base.GetAll();
        }


        public Task<bool> Update(Container entity)
        {
            return base.Update(entity);
        }
    }
}
