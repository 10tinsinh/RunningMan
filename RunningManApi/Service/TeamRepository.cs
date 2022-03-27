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
        private readonly UserDataAccess Account;

        public TeamRepository()
        {
            teamData = new TeamDataAccess();
            teamDetail = new DetailTeamDataAccess();
            Account = new UserDataAccess();
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
            var checkTeamExist = teamDetail.GetTeamDetail().FirstOrDefault(x => x.TeamId == checkTeam.Id);
            if(checkTeamExist == null)
            {
                teamData.DeleteTeam(checkTeam.Id);
            }    
            else
            {
                if(checkTeamDetail.TeamLead == true)
                {
                    var result = new TeamDetail
                    {
                        AccountId = checkTeamExist.AccountId,
                        TeamId = checkTeamExist.TeamId,
                        TeamLead = true
                    };
                    teamDetail.UpdateTeamDetail(checkTeamExist.Id, result);
                }
                
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

        public void UpdateTeam(int idUser,string nameTeam,TeamDTO team)
        {
            var checkTeam = teamData.GetTeam().SingleOrDefault(x => x.Name == nameTeam);
            if(checkTeam == null)
            {
                throw new Exception
                {
                    Source = "Team not exist"
                };
            }
            var checkTeamLead = teamDetail.GetTeamDetail().SingleOrDefault(x => x.AccountId == idUser && x.TeamId == checkTeam.Id && x.TeamLead == true);
            if (checkTeamLead == null)
            {
                throw new Exception{
                    Source = "You don't have Leader of team " + nameTeam
                };
            }
            var updateTeam = new Team
            {
                Name = team.Name
            };
            teamData.UpdateTeam(checkTeam.Id, updateTeam);
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

        public ApiResponse AddMemberIntoTeam(int idTeamLead, string user, string team)
        {
            var checkUser = Account.GetAccount().SingleOrDefault(x => x.UserName == user);
            if (checkUser == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Username not exist"
                };
            }
            var checkTeam = teamData.GetTeam().SingleOrDefault(x => x.Name == team);
            if (checkTeam == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Team not exist"
                };
            }
            var checkTeamDetail = teamDetail.GetTeamDetail().SingleOrDefault(x => x.TeamId == checkTeam.Id && x.AccountId == checkUser.Id);
            if (checkTeamDetail != null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "User already exist in the team"
                };
            }
            var chackTeamLead = teamDetail.GetTeamDetail().SingleOrDefault(x => x.TeamId == checkTeam.Id && x.AccountId == idTeamLead && x.TeamLead == true);
            if (chackTeamLead == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "You don't have Leader of team " + team
                };
            }

            var _teamDetail = new TeamDetailDTO
            {
                AccountId = checkUser.Id,
                TeamId = checkTeam.Id,
                TeamLead = false
            };
            teamDetail.CreateTeamDetail(_teamDetail);
            return new ApiResponse
            {
                Success = true,
                Message = "Add member successfully into" + team
            };
        }
        public ApiResponse KickMember(int idTeamLead, string user, string team)
        {
            var checkUser = Account.GetAccount().SingleOrDefault(x => x.UserName == user);
            if (checkUser == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Username not exist"
                };
            }
            var checkTeam = teamData.GetTeam().SingleOrDefault(x => x.Name == team);
            if (checkTeam == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Team not exist"
                };
            }
            var checkTeamDetail = teamDetail.GetTeamDetail().SingleOrDefault(x => x.TeamId == checkTeam.Id && x.AccountId == checkUser.Id);
            if (checkTeamDetail == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "User does not exist in the team"
                };
            }
            var chackTeamLead = teamDetail.GetTeamDetail().SingleOrDefault(x => x.TeamId == checkTeam.Id && x.AccountId == idTeamLead && x.TeamLead == true);
            if (chackTeamLead == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "You don't have Leader of team " + team
                };
            }
            if(checkUser.Id == idTeamLead)
            {
                throw new Exception();
            }
            teamDetail.DeleteTeamDetail(checkTeamDetail.Id);
            return new ApiResponse
            {
                Success = true,
                Message = "Kick member " + user + " successfully"
            };
        }


    }
}
