using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class DetailPermissionDataAccess
    {
        public List<DetailPermission> GetDetailPermission()
        {
            var dataBase = new MyDbContext();

            var detailPermissions = dataBase.DetailPermissions.Select(ac => new DetailPermission
            {
                Id = ac.Id,
                PermissionId = ac.PermissionId,
                AccountId = ac.AccountId

            });
            return detailPermissions.ToList();
        }
    }
}
