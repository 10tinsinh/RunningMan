using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class DetailPermissionDataAccess
    {
        public List<PermissionDetail> GetDetailPermission()
        {
            var dataBase = new MyDbContext();

            var detailPermissions = dataBase.PermissionDetails.Select(ac => new PermissionDetail
            {
                Id = ac.Id,
                PermissionId = ac.PermissionId,
                AccountId = ac.AccountId

            });
            return detailPermissions.ToList();
        }
        public void CreatePermissionDetail(PermissionDetail permissionDetail)
        {
            var dataBase = new MyDbContext();
            var _permission = new PermissionDetail
            {
                AccountId = permissionDetail.AccountId,
                PermissionId = permissionDetail.PermissionId
            };
            dataBase.Add(_permission);
            dataBase.SaveChanges();
        }

        public void UpdatePermissionDetail(int id, PermissionDetail permissionDetail)
        {
            var dataBase = new MyDbContext();
            var permission = dataBase.PermissionDetails.SingleOrDefault(x => x.Id == id);
            if (permission != null)
            {
                permission.AccountId = permissionDetail.AccountId;
                permission.PermissionId = permissionDetail.PermissionId;
                dataBase.SaveChanges();
            }
        }

        public void DeletePermissionDetail(int id)
        {
            var dataBase = new MyDbContext();
            var permission = dataBase.PermissionDetails.SingleOrDefault(x => x.Id == id);
            if (permission != null)
            {
                dataBase.Remove(permission);
                dataBase.SaveChanges();
            }
        }
    }
}
