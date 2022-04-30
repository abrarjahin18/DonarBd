using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonarBd.Infrastructure.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DonarBDContext>
    {
        public DonarBDContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DonarBDContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-QSU3AFC;Database=DonarBd;Trusted_Connection=True;MultipleActiveResultSets=true");
          

            return new DonarBDContext(optionsBuilder.Options);
        }
    }
}
