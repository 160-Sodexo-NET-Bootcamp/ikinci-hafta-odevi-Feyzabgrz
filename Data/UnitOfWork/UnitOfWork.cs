using Data.ContainerRepo;
using Data.Context;
using Data.DataModel;
using Data.VehicleRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger logger;
        private readonly IConfiguration configuration1;
        private readonly WasteSystemDbContext context;

        public IVehicleRepository Vehicle { get; private set; }
        public IContainerRepository Container { get; private set; }

        public UnitOfWork(WasteSystemDbContext context, ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            this.context = context;
            this.logger = loggerFactory.CreateLogger("patika");


            Vehicle = new VehicleRepository(context, logger);
            Container = new ContainerRepository(context, logger);

        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
