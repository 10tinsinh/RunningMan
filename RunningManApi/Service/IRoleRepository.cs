using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IRoleRepository
    {
        ApiResponse GetRoleUser(int id, string Role);

        List<RoleIdDTO> GetRole(string roleName);

        RoleIdDTO CreateRole(RoleDTO roleDTO);

        void UpdateRole(int id, RoleDTO roleDTO);

        void DeleteRole(string roleCode);
    }
}
