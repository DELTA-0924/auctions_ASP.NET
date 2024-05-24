using Auctuons_core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.Abstractions
{
    public  interface IPermissionService
    {
    Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
    }
}
