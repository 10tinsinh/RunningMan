using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class DetailRoleDataAccess
    {
        public List<RolesDetail> GetRole()
        {
            var dataBase = new MyDbContext();

            var detailRoles = dataBase.RolesDetails.Select(ac => new RolesDetail
            {
                Id = ac.Id,
                RolesId = ac.RolesId,
                AccountId = ac.AccountId

            });
            return detailRoles.ToList();
        }
    }
}
