using Data.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class WasteSystemDbContext : DbContext,IWasteSystemDbContext
    {

        public WasteSystemDbContext(DbContextOptions<WasteSystemDbContext> options) : base(options)
        {
        }
        public DbSet<Vehicle> Vehicle { get; set; }
        public DbSet<Container> Container { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
