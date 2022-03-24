using RunningManApi.DTO.Models;
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

        public void CreateTeam(TeamDTO team)
        {
            var dataBase = new MyDbContext();
            var _team = new Team
            {
                
                Name = team.Name
            };
            dataBase.Add(_team);
            dataBase.SaveChanges();
        }

        public void UpdateTeam(int id, Team team)
        {
            var dataBase = new MyDbContext();
            var _team = dataBase.Teams.SingleOrDefault(x => x.Id == id);
            if (_team != null)
            {
                _team.Name = team.Name;
                _team.Rank = team.Rank;
                dataBase.SaveChanges();
            }
        }

        public void DeleteTeam(int id)
        {
            var dataBase = new MyDbContext();
            var _team = dataBase.Teams.SingleOrDefault(x => x.Id == id);
            if (_team != null)
            {
                dataBase.Remove(_team);
                dataBase.SaveChanges();
            }
        }
    }
}
