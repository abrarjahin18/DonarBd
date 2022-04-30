using DonarBd.Core.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonarBd.Infrastructure.Contexts
{
    public class WorkContext : IWorkContext
    {
        public Task<User> GetCurrentUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCurrentUserRoleAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<int>> GetPermissions()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUserSignedIn()
        {
            throw new NotImplementedException();
        }

        public void SetCurrentUserRoleAsync(string roles)
        {
            throw new NotImplementedException();
        }

        public Task SetPermissions()
        {
            throw new NotImplementedException();
        }
    }
}
