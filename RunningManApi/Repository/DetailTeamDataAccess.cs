using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class DetailTeamDataAccess
    {
        public List<TeamDetail> GetTeamDetail()
        {
            var dataBase = new MyDbContext();

            var teamDetail = dataBase.TeamDetails.Select(ac => new TeamDetail
            {
                Id = ac.Id,
                AccountId = ac.AccountId,
                TeamId = ac.TeamId

            });
            return teamDetail.ToList();
        }

        public void CreateTeamDetail(TeamDetail teamDetail)
        {
            var dataBase = new MyDbContext();
            var _teamDetail = new TeamDetail
            {
                Id = teamDetail.Id,
                AccountId = teamDetail.AccountId,
                TeamId = teamDetail.TeamId
                
            };
            dataBase.Add(_teamDetail);
            dataBase.SaveChanges();
        }

        public void UpdateTeamDetail(int id, TeamDetail teamDetail)
        {
            var dataBase = new MyDbContext();
            var _teamDetail = dataBase.TeamDetails.SingleOrDefault(x => x.Id == id);
            if (_teamDetail != null)
            {
                _teamDetail.TeamId = teamDetail.TeamId;
                _teamDetail.AccountId = teamDetail.AccountId;
                    
                dataBase.SaveChanges();
            }
        }

        public void DeleteTeamDetail(int id)
        {
            var dataBase = new MyDbContext();
            var _teamDetail = dataBase.TeamDetails.SingleOrDefault(x => x.Id == id);
            if (_teamDetail != null)
            {
                dataBase.Remove(_teamDetail);
                dataBase.SaveChanges();
            }
        }
    }
}
