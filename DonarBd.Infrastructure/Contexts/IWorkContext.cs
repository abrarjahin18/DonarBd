using DonarBd.Core.Domain.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DonarBd.Infrastructure.Contexts
{
    public interface IWorkContext
    {
        Task<User> GetCurrentUserAsync();
        Task<string> GetCurrentUserRoleAsync();
        void SetCurrentUserRoleAsync(string roles);
        Task<bool> IsUserSignedIn();
        Task<List<int>> GetPermissions();
        Task SetPermissions();
    }
}
