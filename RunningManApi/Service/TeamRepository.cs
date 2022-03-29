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
        private readonly UserDataAccess account;
        private readonly PermissionDataAccess permission;
        private readonly DetailPermissionDataAccess detailPermission;

        public TeamRepository()
        {
            teamData = new TeamDataAccess();
            teamDetail = new DetailTeamDataAccess();
            account = new UserDataAccess();
            permission = new PermissionDataAccess();
            detailPermission = new DetailPermissionDataAccess();
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
            var permissionUser = permission.GetPermission().Where(x =>  x.PermissionCode == "RUNNING_MAN_TEAM_LEADER");


            foreach (int temp in permissionUser.Select(x => x.Id))
            {
                var checkPermissionDetail = detailPermission.GetDetailPermission().Where(x => x.PermissionId == temp && x.AccountId == id);
                if (!checkPermissionDetail.Any())
                {
                    var addPermissionDetail = new PermissionDetail
                    {
                        AccountId = id,
                        PermissionId = temp
                    };
                    detailPermission.CreatePermissionDetail(addPermissionDetail);
                }

            }

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

            #region Update PermissionDetail
            /*Update PermissionDetail User leave team */
            var permissionUserMember = permission.GetPermission().SingleOrDefault(x => x.PermissionCode == "RUNNING_MAN_TEAM_MEMBER");
            var permissionUserLeader = permission.GetPermission().SingleOrDefault(x => x.PermissionCode == "RUNNING_MAN_TEAM_LEADER");
            var checkUserLeaveMember = teamDetail.GetTeamDetail().Where(x => x.AccountId == id && x.TeamLead == false);
            if(!checkUserLeaveMember.Any())
            {
                var checkTeamMember = detailPermission.GetDetailPermission().SingleOrDefault(x => x.AccountId == id && x.PermissionId == permissionUserMember.Id);
                if(checkTeamMember != null)
                {
                    detailPermission.DeletePermissionDetail(checkTeamMember.Id);
                }    
                
            }
            var checkUserLeaveLeader = teamDetail.GetTeamDetail().Where(x => x.AccountId == id && x.TeamLead == true);
            if (!checkUserLeaveLeader.Any())
            {
                var checkTeamLeader = detailPermission.GetDetailPermission().SingleOrDefault(x => x.AccountId == id && x.PermissionId == permissionUserLeader.Id);
                if (checkTeamLeader != null)
                {
                    detailPermission.DeletePermissionDetail(checkTeamLeader.Id);
                }
                
            }

            /*Update PermissionDetail User new leader team */
            if(checkTeamExist != null)
            {
                var checkNewLeader = detailPermission.GetDetailPermission().SingleOrDefault(x => x.AccountId == checkTeamExist.AccountId && x.PermissionId == permissionUserLeader.Id);
                if (checkNewLeader == null)
                {
                    var addPermissionDetail = new PermissionDetail
                    {
                        AccountId = checkTeamExist.AccountId,
                        PermissionId = permissionUserLeader.Id
                    };
                    detailPermission.CreatePermissionDetail(addPermissionDetail);
                }
                var checkNewLeaderMember = teamDetail.GetTeamDetail().Where(x => x.AccountId == checkTeamExist.AccountId && x.TeamLead == false);
                if (!checkNewLeaderMember.Any())
                {
                    var checkTeamMember = detailPermission.GetDetailPermission().SingleOrDefault(x => x.AccountId == checkTeamExist.AccountId && x.PermissionId == permissionUserMember.Id);
                    if (checkTeamMember != null)
                    {
                        detailPermission.DeletePermissionDetail(checkTeamMember.Id);
                    }

                }
            }    
            #endregion


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
                        var checkDetail = teamDetail.GetTeamDetail().SingleOrDefault(x => x.AccountId == user && x.TeamId == temp); 
                        var teamTemp = new TeamIdDTO
                        {
                            Id = _team.Id,
                            Name = _team.Name,
                            Rank = _team.Rank,
                            TeamLead = checkDetail.TeamLead
                        };
                        result.Add(teamTemp);

                    }
                  
                
                var resultUser = result.Select(x => new TeamIdDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Rank = x.Rank,
                    TeamLead = x.TeamLead
                    
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
            var permissionUser = permission.GetPermission().Where(x => x.PermissionCode == "RUNNING_MAN_TEAM_MEMBER");


            foreach (int temp in permissionUser.Select(x => x.Id))
            {
                var checkPermissionDetail = detailPermission.GetDetailPermission().Where(x => x.PermissionId == temp && x.AccountId == id);
                if (!checkPermissionDetail.Any())
                {
                    var addPermissionDetail = new PermissionDetail
                    {
                        AccountId = id,
                        PermissionId = temp
                    };
                    detailPermission.CreatePermissionDetail(addPermissionDetail);
                }

            }
        }

        public ApiResponse AddMemberIntoTeam(int idTeamLead, string user, string team)
        {
            var checkUser = account.GetAccount().SingleOrDefault(x => x.UserName == user);
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
           
            var permissionUser = permission.GetPermission().Where(x => x.PermissionCode == "RUNNING_MAN_TEAM_MEMBER");
            foreach (int temp in permissionUser.Select(x => x.Id))
            {
                var checkPermissionDetail = detailPermission.GetDetailPermission().Where(x => x.PermissionId == temp && x.AccountId == checkUser.Id);
                if (!checkPermissionDetail.Any())
                {
                    var addPermissionDetail = new PermissionDetail
                    {
                        AccountId = checkUser.Id,
                        PermissionId = temp
                    };
                    detailPermission.CreatePermissionDetail(addPermissionDetail);
                }

            }

            return new ApiResponse
            {
                Success = true,
                Message = "Add member successfully into" + team
            };
        }
        public ApiResponse KickMember(int idTeamLead, string user, string team)
        {
            var checkUser = account.GetAccount().SingleOrDefault(x => x.UserName == user);
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

            var permissionUserMember = permission.GetPermission().SingleOrDefault(x => x.PermissionCode == "RUNNING_MAN_TEAM_MEMBER");
            var checkMember = teamDetail.GetTeamDetail().Where(x => x.AccountId == checkUser.Id && x.TeamLead == false);
            if(!checkMember.Any())
            {
                var checkPermission = detailPermission.GetDetailPermission().SingleOrDefault(x => x.AccountId == checkUser.Id && x.PermissionId == permissionUserMember.Id);
                detailPermission.DeletePermissionDetail(checkPermission.Id);
            }    

            return new ApiResponse
            {
                Success = true,
                Message = "Kick member " + user + " successfully"
            };
        }

        public ApiResponse ResignTeamLead(int idTeamLead, string user, string team)
        {
            var checkUser = account.GetAccount().SingleOrDefault(x => x.UserName == user);
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
            var checkTeamLead = teamDetail.GetTeamDetail().SingleOrDefault(x => x.TeamId == checkTeam.Id && x.AccountId == idTeamLead && x.TeamLead == true);
            if (checkTeamLead == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "You don't have Leader of team " + team
                };
            }
            if (checkUser.Id == idTeamLead)
            {
                throw new Exception();
            }
            var insertTeamLead = new TeamDetail
            {
               
                AccountId = checkTeamDetail.AccountId,
                TeamId = checkTeamDetail.TeamId,
                TeamLead = true
            };
            teamDetail.UpdateTeamDetail(checkTeamDetail.Id, insertTeamLead);
            var resignTeamLead = new TeamDetail
            {
                AccountId = checkTeamLead.AccountId,
                TeamId = checkTeamLead.TeamId,
                TeamLead = false
            };
            teamDetail.UpdateTeamDetail(checkTeamLead.Id, resignTeamLead);

            var permissionUserMember = permission.GetPermission().SingleOrDefault(x => x.PermissionCode == "RUNNING_MAN_TEAM_MEMBER");
            var permissionUserLeader = permission.GetPermission().SingleOrDefault(x => x.PermissionCode == "RUNNING_MAN_TEAM_LEADER");
            #region change permission Leader Old
            //check if user has different team leader
            var checkLeader = teamDetail.GetTeamDetail().Where(x => x.AccountId == checkTeamLead.AccountId && x.TeamLead == true);
            if (!checkLeader.Any())
            {
               
                var checkPermissionDetail = detailPermission.GetDetailPermission().SingleOrDefault(x => x.PermissionId == permissionUserMember.Id && x.AccountId == checkTeamLead.AccountId);
                var checkPermissionLeader = detailPermission.GetDetailPermission().SingleOrDefault(x => x.PermissionId == permissionUserLeader.Id && x.AccountId == checkTeamLead.AccountId);
                int idLeader = checkPermissionLeader.Id;
                if (checkPermissionDetail == null)//don't have checkPermissionDetail
                {
                        
                    var addPermissionDetail = new PermissionDetail
                    {
                        AccountId = checkTeamLead.AccountId,
                        PermissionId = permissionUserMember.Id
                    };
                    detailPermission.UpdatePermissionDetail(idLeader,addPermissionDetail);
                }
                else
                {
                        
                    detailPermission.DeletePermissionDetail(idLeader);
                }    
            }    
            else
            {
                var checkPermissionDetail = detailPermission.GetDetailPermission().SingleOrDefault(x => x.PermissionId == permissionUserMember.Id && x.AccountId == checkTeamLead.AccountId);
                if (checkPermissionDetail == null)//don't have checkPermissionDetail
                {

                    var addPermissionDetail = new PermissionDetail
                    {
                        AccountId = checkTeamLead.AccountId,
                        PermissionId = permissionUserMember.Id
                    };
                    detailPermission.CreatePermissionDetail(addPermissionDetail);
                }
            }
            #endregion

            #region change permission Leader new

            var checkMember = teamDetail.GetTeamDetail().Where(x => x.AccountId == checkTeamDetail.AccountId && x.TeamLead == false);
            if (!checkMember.Any())
            {

                var checkPermissionDetail = detailPermission.GetDetailPermission().SingleOrDefault(x => x.PermissionId == permissionUserMember.Id && x.AccountId == checkTeamDetail.AccountId);
                var checkPermissionLeader = detailPermission.GetDetailPermission().SingleOrDefault(x => x.PermissionId == permissionUserLeader.Id && x.AccountId == checkTeamDetail.AccountId);
                int idMember = checkPermissionDetail.Id;
                if (checkPermissionLeader == null)//don't have checkPermissionDetail
                {

                    var addPermissionDetail = new PermissionDetail
                    {
                        AccountId = checkTeamDetail.AccountId,
                        PermissionId = permissionUserLeader.Id
                    };
                    detailPermission.UpdatePermissionDetail(idMember, addPermissionDetail);
                }
                else
                {

                    detailPermission.DeletePermissionDetail(idMember);
                }

            }
            else
            {
                var checkPermissionLeader = detailPermission.GetDetailPermission().SingleOrDefault(x => x.PermissionId == permissionUserLeader.Id && x.AccountId == checkTeamDetail.AccountId);
               
                if (checkPermissionLeader == null)//don't have checkPermissionDetail
                {

                    var addPermissionDetail = new PermissionDetail
                    {
                        AccountId = checkTeamDetail.AccountId,
                        PermissionId = permissionUserLeader.Id
                    };
                    detailPermission.CreatePermissionDetail(addPermissionDetail);
                }
            }

            #endregion

            return new ApiResponse
            {
                Success = true,
                Message = "Resign Team Lead Successfully"
            };
        }

        public List<MemberDTO> ShowAllMember(int idTeamLead, string team)
        {
            var checkTeam = teamData.GetTeam().SingleOrDefault(x => x.Name == team);
            if (checkTeam == null)
            {
                throw new Exception();
            }
            var checkMember= teamDetail.GetTeamDetail().SingleOrDefault(x => x.TeamId == checkTeam.Id && x.AccountId == idTeamLead);
            if (checkMember == null)
            {
                throw new Exception();
            }
            var checkTeamDetail = teamDetail.GetTeamDetail().Where(x => x.TeamId == checkTeam.Id);
            var getUser = account.GetAccount().AsQueryable();
            var username = checkTeamDetail.Join(getUser, p => p.AccountId, c => c.Id, (p, c) => new
            {
                c.UserName,
                c.Name
            });

            var result = username.Select(x => new MemberDTO
            {
                Username = x.UserName,
                Name = x.Name
            });




            return result.ToList();
            

        }
    }
}
