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
        private Repository.RoleDataAccess roleData;
        private Repository.DetailRoleDataAccess detailRoleData;
        private readonly AppSetting _appSetting;

        public RoleRepository(IOptionsMonitor<AppSetting> optionsMonitor)
        {

            roleData = new RoleDataAccess();
            detailRoleData = new DetailRoleDataAccess();
            _appSetting = optionsMonitor.CurrentValue;
        }
        public ApiResponse GetRoleUser(int id, string Role)
        {
            var checkDetailRole = detailRoleData.GetRole().Where(x => x.AccountId == id);
            var roleUser = roleData.GetRole().SingleOrDefault(x => x.RoleCode == Role);
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

        public List<RoleIdDTO> GetRole(string roleName)
        {
            var checkRole = roleData.GetRole().AsQueryable();
            if(!string.IsNullOrEmpty(roleName))
            {
                checkRole = checkRole.Where(x => x.RoleName.Contains(roleName));
            }
            var role = checkRole.Select(x => new RoleIdDTO
            {
                Id = x.Id,
                RoleCode = x.RoleCode,
                RoleName = x.RoleName
            });
            return role.ToList();
        }

        public RoleIdDTO CreateRole(RoleDTO roleDTO)
        {
            var checkRole = roleData.GetRole().SingleOrDefault(x => x.RoleCode == roleDTO.RoleCode);
            if(checkRole != null)
            {
                throw new Exception();
            }
            var role = new RoleDTO
            {
                RoleCode = roleDTO.RoleCode,
                RoleName = roleDTO.RoleName
            };
            roleData.CreateRole(role);
            var newRole = roleData.GetRole().SingleOrDefault(x => x.RoleCode == roleDTO.RoleCode);
            var result = new RoleIdDTO
            {
                Id = newRole.Id,
                RoleName = newRole.RoleName,
                RoleCode = newRole.RoleCode
            };
            return result;
        }

        public void UpdateRole(int id, RoleDTO roleDTO)
        {
            var checkRole = roleData.GetRole().SingleOrDefault(x => x.Id == id);
            if(checkRole == null)
            {
                throw new Exception();
            }
            var role = new RoleDTO
            {
                RoleCode = roleDTO.RoleCode,
                RoleName = roleDTO.RoleName
            };
            roleData.UpdateRole(id, role);

        }

        public void DeleteRole(string roleCode)
        {
            var checkRole = roleData.GetRole().SingleOrDefault(x => x.RoleCode == roleCode);
            if(checkRole == null)
            {
                throw new Exception();
            }
            roleData.DeleteRole(checkRole.Id);
        }
    }
}
