using Microsoft.AspNetCore.Authorization;
using RunningManApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Helpers
{
    public class PermissionRequirement: IAuthorizationRequirement
    {
        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }

        public string Permission { get; set; }
    }
}
