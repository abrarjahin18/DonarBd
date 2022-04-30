using DonarBd.Core.Domain.Auth;
using DonarBd.Core.Domain.BloodDonars;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonarBd.Infrastructure.Data
{
    public class DonarBDContext : IdentityDbContext<User, Role, string>
    {
        public DonarBDContext(DbContextOptions<DonarBDContext> options) : base(options)
        {

        }
        public virtual DbSet<BloodDonar> BloodDonars { get; set; }
    }
}
