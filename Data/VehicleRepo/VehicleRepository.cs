using Data.Context;
using Data.DataModel;
using Data.Generic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.VehicleRepo
{
    public  class VehicleRepository : GenericRepository<Vehicle> , IVehicleRepository
    {
        public VehicleRepository(WasteSystemDbContext context, ILogger logger) : base(context, logger)
        {

        }

        
        public   IEnumerable<Vehicle> Where(System.Linq.Expressions.Expression<Func< Vehicle, bool>> where)
        {
            return base.Where(where);
        }

        public async Task<bool> Add(Vehicle entity)
        {
           return await base.Add(entity);
        }

        public async Task<bool> Delete(long id)
        {
            var result=  base.Delete(id);
            return true;
        }

        public Task<IEnumerable<Vehicle>> GetAll()
        {
            return base.GetAll();
        }


        public Task<bool> Update(Vehicle entity)
        {
            return base.Update(entity);
        }
    }
}
