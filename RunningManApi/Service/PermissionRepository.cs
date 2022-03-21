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
    public class PermissionRepository : IPermissionRepository
    {
        
        
        private Repository.PermissionDataAccess permission;
        private Repository.DetailPermissionDataAccess detailPermission;
        private readonly AppSetting _appSetting;

        public PermissionRepository(IOptionsMonitor<AppSetting> optionsMonitor)
        {

            permission = new PermissionDataAccess();
            detailPermission = new DetailPermissionDataAccess();
            _appSetting = optionsMonitor.CurrentValue;
        }
        public ApiResponse GetPermissionUser(int id, string permissionPolicy)
        {
            var checkDetailPermission = detailPermission.GetDetailPermission().Where(x => x.AccountId == id);
            var permissionUser = permission.GetPermission().SingleOrDefault(x => x.PermissionCode == permissionPolicy);
            var result = checkDetailPermission.SingleOrDefault(x => x.PermissionId == permissionUser.Id);
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
