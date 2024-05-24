using Auctuons_core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.Infastructure
{
    public class PermissionRequirement
        : IAuthorizationRequirement
    {
        public PermissionRequirement(Permission[] permissions)=>Permissions=permissions;
        public Permission[] Permissions { get; set; } = [];
    }
}
