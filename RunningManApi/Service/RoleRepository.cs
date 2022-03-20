using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningManApi.Repository;
using RunningManApi.Repository.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace RunningManApi.Service
{
    public class RoleRepository : IRoleRepository
    {
        private Repository.RoleDataAccess role;
        private Repository.DetailRoleDataAccess detailRole;
        private readonly AppSetting _appSetting;

        public RoleRepository(IOptionsMonitor<AppSetting> optionsMonitor)
        {

            role = new RoleDataAccess();
            detailRole = new DetailRoleDataAccess();
            _appSetting = optionsMonitor.CurrentValue;
        }
        public ApiResponse GetRoleUser(int id, string Role)
        {
            var checkDetailRole = detailRole.GetRole().Where(x => x.AccountId == id);
            var roleUser = role.GetRole().SingleOrDefault(x => x.NameRoles == Role);
            var result = checkDetailRole.SingleOrDefault(x => x.RolesId == roleUser.Id);
            if (result != null)
            {
                var reponse = new ApiResponse
                {
                    Success = true,
                    Message = "User valid",
                    Data = null
                };
                return reponse;
            }
            else
            {
                var reponse = new ApiResponse
                {
                    Success = false,
                    Message = "User Invalid",
                    Data = null
                };
                return reponse;
            }

        }
    }
}
