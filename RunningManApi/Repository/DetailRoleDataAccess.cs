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

        public void CreateRoleDetail(RolesDetail rolesDetail)
        {
            var dataBase = new MyDbContext();
            var _roles = new RolesDetail
            {
                AccountId = rolesDetail.AccountId,
                RolesId = rolesDetail.RolesId
            };
            dataBase.Add(_roles);
            dataBase.SaveChanges();
        }

        public void UpdateRole(int id, RolesDetail rolesDetail)
        {
            var dataBase = new MyDbContext();
            var roles = dataBase.RolesDetails.SingleOrDefault(x => x.Id == id);
            if(roles != null)
            {
                roles.AccountId = rolesDetail.AccountId;
                roles.RolesId = rolesDetail.RolesId;
                dataBase.SaveChanges();
            }    
        }

        public void DeleteRole(int id)
        {
            var dataBase = new MyDbContext();
            var roles = dataBase.RolesDetails.SingleOrDefault(x => x.Id == id);
            if (roles != null)
            {
                dataBase.Remove(roles);
                dataBase.SaveChanges();
            }
        }
    }
}
