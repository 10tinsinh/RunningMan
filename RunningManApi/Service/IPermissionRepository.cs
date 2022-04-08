using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IPermissionRepository
    {
        ApiResponse GetPermissionUser(int id, string permissionPolicy);

        List<PermissionIdDTO> GetPermission(string permissionName);

        PermissionIdDTO CreatePermission(PermissionDTO permissionDTO);

        void UpdatePermission(int id, PermissionDTO permissionDTO);

        void DeletePermission(string permissionCode);

    }
}
