using RunningManApi.DTO.Models;
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

        public void CreateRole(RoleDTO roleDTO)
        {
            var dataBase = new MyDbContext();

            var role = new Role
            {
                RoleCode = roleDTO.RoleCode,
                RoleName = roleDTO.RoleName
            };
            dataBase.Add(role);
            dataBase.SaveChanges();

        }

        public void UpdateRole(int id, RoleDTO roleDTO)
        {
            var dataBase = new MyDbContext();

            var checkRole = dataBase.Roles.SingleOrDefault(x => x.Id == id);
            if(checkRole != null)
            {
                checkRole.RoleCode = roleDTO.RoleCode;
                checkRole.RoleName = roleDTO.RoleName;
                dataBase.SaveChanges();
            }    
        }

        public void DeleteRole(int id)
        {
            var dataBase = new MyDbContext();
            var checkRole = dataBase.Roles.SingleOrDefault(x => x.Id == id);
            if (checkRole != null)
            {
                dataBase.Remove(checkRole);
                dataBase.SaveChanges();
            }    
        }

    }
}
