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
        public TeamIdDTO CreateNewTeam(int id,TeamDTO team)
        {
            var checkTeam = teamData.GetTeam().SingleOrDefault(x => x.Name == team.Name);
            if (checkTeam != null)
            {
                throw new Exception("User invalid");
            }
            var _team = new TeamDTO
            {
                Name = team.Name
            };
            teamData.CreateTeam(_team);
            var newTeam = teamData.GetTeam().SingleOrDefault(x => x.Name == team.Name);
            var newTeamDetail = new TeamDetailDTO
            {
                AccountId = id,
                TeamId = newTeam.Id,
                TeamLead = true
                

            };
            teamDetail.CreateTeamDetail(newTeamDetail);


            return new TeamIdDTO
            {
                Id = newTeam.Id,
                Name = newTeam.Name,
                Rank = newTeam.Rank
            };

        }

        public void DeleteTeam(int id, string teamName)
        {
            var checkTeam = teamData.GetTeam().SingleOrDefault(x => x.Name == teamName);
            if (checkTeam == null )
            {
                throw new Exception();
            }
            var checkTeamDetail = teamDetail.GetTeamDetail().SingleOrDefault(x => x.AccountId == id && x.TeamId == checkTeam.Id);
            if( checkTeamDetail == null)
            {
                throw new Exception();
            }    
            teamDetail.DeleteTeamDetail(checkTeamDetail.Id);
            var checkTeamExist = teamDetail.GetTeamDetail().SingleOrDefault(x => x.TeamId == checkTeam.Id);
            if(checkTeamExist == null)
            {
                teamData.DeleteTeam(checkTeam.Id);
            }    
            
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

        public void JoinTeam(int id, string team)
        {
            var checkTeam = teamData.GetTeam().SingleOrDefault(x => x.Name == team);
            if(checkTeam == null)
            {
                throw new Exception();
            }
            var checkTeamDetail = teamDetail.GetTeamDetail().SingleOrDefault(x => x.TeamId == checkTeam.Id && x.AccountId == id);
            if(checkTeamDetail != null)
            {
                throw new Exception();
            }
            var _teamDetail = new TeamDetailDTO
            {
                AccountId = id,
                TeamId = checkTeam.Id,
                TeamLead = false
            };
            teamDetail.CreateTeamDetail(_teamDetail);
        }
    }
}
