using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using RunningManApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace RunningManApi.Helpers
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement> 
    {
        private readonly IPermissionRepository permissionRepository;

        public PermissionRequirementHandler (IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            

            if (identity.IsAuthenticated == true)
            {
                string id = context.User.FindFirst(x => x.Type == "Id").Value; // Get Id User had login
                var check = permissionRepository.GetPermissionUser(int.Parse(id), requirement.Permission); // Call Constructor check permission at Service/permissionRepository
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
