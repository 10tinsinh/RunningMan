using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningManApi.Repository;
using RunningManApi.Repository.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using BCrypt.Net;

namespace RunningManApi.Service
{
    public class AccountRepository : IAccountRepository
    {
        private Repository.UserDataAccess account;
        private Repository.RoleDataAccess role;
        private Repository.DetailRoleDataAccess detailRole;
        private readonly PermissionDataAccess permission;
        private readonly DetailPermissionDataAccess detailPermission;
        private readonly PointDataAccess pointData;
        private readonly UserRefreshTokenDataAccess refreshTokenData;
        private readonly AppSetting _appSetting;
        private readonly IJWTManagerTokenRepository _jwtManager;

        public AccountRepository( IOptionsMonitor<AppSetting> optionsMonitor, IJWTManagerTokenRepository jWTManagerTokenRepository)
        {
            account = new UserDataAccess();
            
            role = new RoleDataAccess();
            detailRole = new DetailRoleDataAccess();
            permission = new PermissionDataAccess();
            detailPermission = new DetailPermissionDataAccess();
            pointData = new PointDataAccess();
            refreshTokenData = new UserRefreshTokenDataAccess();
            _appSetting = optionsMonitor.CurrentValue;
            _jwtManager = jWTManagerTokenRepository;
        }

        public AccountIdDTO CreateAccount(AccountDTO user)
        {
            var checkUser = account.GetAccount().SingleOrDefault(us => us.UserName == user.Username);
            if(checkUser != null)
            {
                throw new Exception("User invalid");
            }
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var addAccount = new Account
            {
                UserName = user.Username,
                Password = hashPassword,
                Name = user.Name,
                Email = user.Email,
                AccountStatus = true
            };
            account.CreateAccount(addAccount);
            var newUser = account.GetAccount().SingleOrDefault(us => us.UserName == addAccount.UserName);

            var roleUser = role.GetRole().SingleOrDefault(x => x.RoleCode == "USER");
            var addRoleDetail = new RolesDetail
            {
                AccountId = newUser.Id,
                RolesId = roleUser.Id
            };
            detailRole.CreateRoleDetail(addRoleDetail);

            var permissionUser = permission.GetPermission().Where(x => x.PermissionCode == "RUNNING_MAN_USER_VIEW" 
            || x.PermissionCode == "RUNNING_MAN_USER_UPDATE"
            || x.PermissionCode == "RUNNING_MAN_TEAM_JOIN"
            || x.PermissionCode == "RUNNING_MAN_TEAM_CREATE");
           
            
            foreach (int temp in permissionUser.Select(x=>x.Id))
            {
                var addPermissionDetail = new PermissionDetail
                {
                    AccountId = newUser.Id,
                    PermissionId = temp
                };
                detailPermission.CreatePermissionDetail(addPermissionDetail);
            }

            /// Create Manager Point User
            var checkPoint = pointData.GetPoint().SingleOrDefault(x => x.AccountId == newUser.Id);
            if(checkPoint == null)
            {
                var point = new PointDTO
                {
                    AccountId = newUser.Id
                    
                };
                pointData.CreatePoint(point);
            }    

            return new AccountIdDTO
            {
                Id = newUser.Id,
                Username = addAccount.UserName,
                Name = addAccount.Name,
                Email = addAccount.Email,
                AccountStatus = addAccount.AccountStatus
            };
            
        }


        public List<AccountIdDTO> GetAllAccount(string search)
        {
            var checkUser = account.GetAccount().AsQueryable();
            if(!string.IsNullOrEmpty(search))
            {
                checkUser = checkUser.Where(user => user.UserName.Contains(search));

            }
            checkUser = checkUser.OrderBy(user => user.UserName);
            var User = checkUser.Select(ac => new AccountIdDTO
            {
                Id = ac.Id,
                Username = ac.UserName,
                Password = ac.Password,
                Name = ac.Name,
                Email = ac.Email,
                AccountStatus = ac.AccountStatus
            });
            return User.ToList();
        }

        public ApiResponse Login(Login login)
        {
            var checkUser = account.GetAccount().SingleOrDefault(us => us.UserName == login.Username);

            if (checkUser == null)
            {
                var result = new ApiResponse
                {
                    Success = false,
                    Message = "Username invalid",
                    Data = null
                };
                return result;
            }
            else 
            { 
                if(BCrypt.Net.BCrypt.Verify(login.Password,checkUser.Password))
                {
                    if(checkUser.AccountStatus == true)
                    {
                        var token = _jwtManager.GenerateToken(checkUser);
                        var result = new ApiResponse
                        {
                            Success = true,
                            Message = "Loggin successfully",
                            Data = token
                        };
                        var refreshToken = new UserRefreshTokenDTO
                        {
                            RefreshToken = token.RefreshToken,
                            UserName = checkUser.UserName
                        };
                        var refresh = refreshTokenData.CreateUserRefreshToken(refreshToken);

                        return result;
                    }
                    else
                    {
                        var result = new ApiResponse
                        {
                            Success = false,
                            Message = "User has been locked",
                            Data = null
                        };
                        return result;
                    }    
                   
                }
                else
                {
                    var result = new ApiResponse
                    {
                        Success = false,
                        Message = "User or Password invalid",
                        Data = null
                    };
                    return result;
                }    
                

            }

        }

        private string GetRoleAccount(int id)
        {
            var detailRoleLogin = detailRole.GetRole().SingleOrDefault(x => x.AccountId == id);

            var roleData = role.GetRole().SingleOrDefault(x => x.Id == detailRoleLogin.RolesId);
            return roleData.RoleCode;
        }


        public void Update(int id, AccountDTO _account)
        {
            var checkId = account.GetAccount().SingleOrDefault(ac => ac.Id == id);
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(checkId.Password);
            if (checkId != null)
            {
                var Result = new Account
                {
                    Id = checkId.Id,
                    UserName = _account.Username,
                    Password = hashPassword,
                    Name = _account.Name,
                    Email = _account.Email,
                    AccountStatus = _account.AccountStatus
                };
                account.UpdateAccount(id,Result);
                
                
            }
            else
            {
                throw new Exception("User invalid");
            }    

        }

        public void Delete(int id)
        {
            var checkId = account.GetAccount().SingleOrDefault(ac => ac.Id == id);
            if( checkId == null)
            {
                throw new Exception("User invalid");
            }
            else
            {
                var permissioncheck = detailPermission.GetDetailPermission().Where(x => x.AccountId == id);
                if(permissioncheck != null)
                {
                    foreach(int temp in permissioncheck.Select(x=>x.Id))
                    {
                        detailPermission.DeletePermissionDetail(temp);
                    }    
                }    

                var rolecheck = detailRole.GetRole().Where(x => x.AccountId == id);
                if (rolecheck != null)
                {
                    foreach (int temp in rolecheck.Select(x => x.Id))
                    {
                        detailRole.DeleteRole(temp);
                    }
                }

                var pointCheck = pointData.GetPoint().SingleOrDefault(x => x.AccountId == id);
                if(pointCheck !=null)
                {
                    pointData.DeletePoint(pointCheck.Id);
                }    
               
                account.DeleteAccount(id);
 
            }

        }

        public AccountIdDTO GetInformationAccountLogin(int id)
        {
            var checkUser = account.GetAccount().SingleOrDefault(x => x.Id == id);
            var User =  new AccountIdDTO
            {
                Id = checkUser.Id,
                Username = checkUser.UserName,
                Password = checkUser.Password,
                Name = checkUser.Name,
                Email = checkUser.Email,
                AccountStatus = checkUser.AccountStatus
            };
            return User;
        }

        public ApiResponse RenewToken(TokensDTO tokensDTO)
        {
            //var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var princaipal = _jwtManager.GetPrincaipalFromExpiredToken(tokensDTO.AccessToken);
            var checkRefresh = refreshTokenData.GetUserRefreshTokens().SingleOrDefault(x => x.RefreshToken == tokensDTO.RefreshToken);
            if (checkRefresh == null)
            {
                throw new Exception();
            }
            var user = account.GetAccount().SingleOrDefault(x => x.UserName == checkRefresh.UserName);
            
            var checkRefreshToken = refreshTokenData.GetUserRefreshTokens().SingleOrDefault(x=>x.RefreshToken == tokensDTO.RefreshToken);
            if(checkRefreshToken == null)
            {
                throw new Exception();
            }
            var newJwtToken = _jwtManager.GenerateRefreshToken(user);
            refreshTokenData.DeleteUserRefreshToken(checkRefreshToken.Id);
            var newRefreshToken = new UserRefreshTokenDTO
            {
                RefreshToken = newJwtToken.RefreshToken,
                UserName = user.UserName
            };
            refreshTokenData.CreateUserRefreshToken(newRefreshToken);
            return new ApiResponse
            {
                Success = true,
                Message = "Loggin successfully",
                Data = newJwtToken
            };


        }
    }
}
