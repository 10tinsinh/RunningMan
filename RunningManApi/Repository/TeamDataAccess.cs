using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class TeamDataAccess
    {
        public List<Team> GetTeam()
        {
            var dataBase = new MyDbContext();

            var team = dataBase.Teams.Select(ac => new Team
            {
                Id = ac.Id,
                Name = ac.Name,
                Rank = ac.Rank

            });
            return team.ToList();
        }

        public void CreateTeam(Team team)
        {
            var dataBase = new MyDbContext();
            var _team = new Team
            {
                Id = team.Id,
                Name = team.Name
            };
            dataBase.Add(_team);
            dataBase.SaveChanges();
        }

        public void UpdateRole(int id, RolesDetail rolesDetail)
        {
            var dataBase = new MyDbContext();
            var roles = dataBase.RolesDetails.SingleOrDefault(x => x.Id == id);
            if (roles != null)
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
