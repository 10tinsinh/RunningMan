using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Helpers
{
    public class RoleRequirement:IAuthorizationRequirement
    {
        public RoleRequirement(string role)
        {
            Role = role;
        }

        public string Role { get; set; }
    }
}
