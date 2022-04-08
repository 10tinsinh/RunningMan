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
        
        
        private Repository.PermissionDataAccess permissionData;
        private Repository.DetailPermissionDataAccess detailPermissionData;
        private readonly AppSetting _appSetting;

        public PermissionRepository(IOptionsMonitor<AppSetting> optionsMonitor)
        {

            permissionData = new PermissionDataAccess();
            detailPermissionData = new DetailPermissionDataAccess();
            _appSetting = optionsMonitor.CurrentValue;
        }
        public ApiResponse GetPermissionUser(int id, string permissionPolicy)
        {
            var checkDetailPermission = detailPermissionData.GetDetailPermission().Where(x => x.AccountId == id);
            var permissionUser = permissionData.GetPermission().SingleOrDefault(x => x.PermissionCode == permissionPolicy);
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

        public List<PermissionIdDTO> GetPermission(string permissionName)
        {
            var checkPermission = permissionData.GetPermission().AsQueryable();
            if(!string.IsNullOrEmpty(permissionName))
            {
                checkPermission = checkPermission.Where(x => x.PermissionName.Contains(permissionName));
            }
            var result = checkPermission.Select(x => new PermissionIdDTO
            {
                Id = x.Id,
                PermissionCode = x.PermissionCode,
                PermissionName = x.PermissionName
            });
            return result.ToList();
            
        }

        public PermissionIdDTO CreatePermission(PermissionDTO permissionDTO)
        {
            var checkPermission = permissionData.GetPermission().SingleOrDefault(x => x.PermissionCode == permissionDTO.PermissionCode);
            if(checkPermission != null)
            {
                throw new Exception();
            }
            var permission = new PermissionDTO
            {
                PermissionCode = permissionDTO.PermissionCode,
                PermissionName = permissionDTO.PermissionName
            };
            permissionData.CreatePermission(permission);
            var newPermission = permissionData.GetPermission().SingleOrDefault(x => x.PermissionCode == permissionDTO.PermissionCode);
            var result = new PermissionIdDTO
            {
                Id = newPermission.Id,
                PermissionCode = newPermission.PermissionCode,
                PermissionName = newPermission.PermissionName
            };
            return result;
        }

        public void UpdatePermission(int id, PermissionDTO permissionDTO)
        {
            var checkPermission = permissionData.GetPermission().SingleOrDefault(x => x.Id == id);
            if(checkPermission == null)
            {
                throw new Exception();
            }
            permissionData.UpdatePermission(id, permissionDTO);
        }

        public void DeletePermission(string permissionCode)
        {
            var checkPermission = permissionData.GetPermission().SingleOrDefault(x => x.PermissionCode == permissionCode);
            if(checkPermission == null)
            {
                throw new Exception();
            }
            permissionData.DeletePermission(checkPermission.Id);
        }
    }
}
