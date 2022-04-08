using RunningManApi.DTO.Models;
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

        public void CreatePermission(PermissionDTO permissionDTO)
        {
            var dataBase = new MyDbContext();
            var permission = new Permission
            {
                PermissionName = permissionDTO.PermissionName,
                PermissionCode = permissionDTO.PermissionCode
            };
            dataBase.Add(permission);
            dataBase.SaveChanges();

        }

        public void UpdatePermission(int id, PermissionDTO permissionDTO)
        {
            var dataBase = new MyDbContext();
            var checkPermission = dataBase.Permissions.SingleOrDefault(x => x.Id == id);
            if(checkPermission != null)
            {
                checkPermission.PermissionCode = permissionDTO.PermissionCode;
                checkPermission.PermissionName = permissionDTO.PermissionName;
                dataBase.SaveChanges();
            }    
        }

        public void DeletePermission(int id)
        {
            var dataBase = new MyDbContext();
            var permission = dataBase.Permissions.SingleOrDefault(x => x.Id == id);
            if(permission != null)
            {
                dataBase.Remove(permission);
                dataBase.SaveChanges();
            }    
        }
    }
}
