using Microsoft.AspNetCore.Authorization;
using RunningManApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RunningManApi.Helpers
{
    public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement> 
   
    {
        private readonly IRoleRepository roleRepository;

        public RoleRequirementHandler(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            var identity = context.User.Identity as ClaimsIdentity;


            if (identity.IsAuthenticated == true)
            {
                string id = context.User.FindFirst(x => x.Type == "Id").Value;
                var check = roleRepository.GetRoleUser(int.Parse(id), requirement.Role);
                if (check.Success == false)
                {
                    return Task.CompletedTask;
                }
                else
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}
