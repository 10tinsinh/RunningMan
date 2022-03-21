using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class RoleDataAccess
    {
        public List<Role> GetRole()
        {
            var dataBase = new MyDbContext();

            var roles = dataBase.Roles.Select(ac => new Role
            {
                Id = ac.Id,
                RoleCode = ac.RoleCode,
                RoleName = ac.RoleName
               
            });
            return roles.ToList();
        }

    }
}
