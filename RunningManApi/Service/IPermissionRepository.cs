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
    }
}
