using RunningManApi.DTO.Models;
using RunningManApi.Repository;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public class TeamRepository : ITeamRepository
    {
        private readonly TeamDataAccess teamData;
        private readonly DetailTeamDataAccess teamDetail;

        public TeamRepository()
        {
            teamData = new TeamDataAccess();
            teamDetail = new DetailTeamDataAccess();
        }
        public Team CreateNewTeam(TeamDTO team)
        {
            throw new NotImplementedException();
        }

        public void DeleteTeam(int id)
        {
            throw new NotImplementedException();
        }

        public List<TeamIdDTO> GetTeam(int user)
        {
            var checkTeamDetail = teamDetail.GetTeamDetail().Where(x => x.AccountId == user);
            if(checkTeamDetail != null)
            {


                List<TeamIdDTO> result = new List<TeamIdDTO>();
                
                    foreach (int temp in checkTeamDetail.Select(x => x.TeamId))
                    {
                        var _team = teamData.GetTeam().SingleOrDefault(a => a.Id == temp);
                        var teamTemp = new TeamIdDTO
                        {
                            Id = _team.Id,
                            Name = _team.Name,
                            Rank = _team.Rank
                        };
                        result.Add(teamTemp);

                    }
                  
                
                var resultUser = result.Select(x => new TeamIdDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Rank = x.Rank
                });
                return resultUser.ToList();
            }
            else
            {
                return new List<TeamIdDTO> { };
            }    
           
            

        }

        public void UpdateTeam(TeamIdDTO team)
        {
            throw new NotImplementedException();
        }
    }
}
