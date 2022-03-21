using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class PermissionDataAccess
    {
        public List<Permission> GetPermission()
        {
            var dataBase = new MyDbContext();

            var permissions = dataBase.Permissions.Select(ac => new Permission
            {
                Id = ac.Id,
                PermissionCode = ac.PermissionCode,
                PermissionName =ac.PermissionName

            });
            return permissions.ToList();
        }
    }
}
