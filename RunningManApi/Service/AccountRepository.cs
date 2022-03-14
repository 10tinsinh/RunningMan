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

namespace RunningManApi.Service
{
    public class AccountRepository : IAccountRepository
    {
        private Repository.UserDataAccess account;
        private readonly AppSetting _appSetting;

        public AccountRepository( IOptionsMonitor<AppSetting> optionsMonitor)
        {
            account = new UserDataAccess();
            _appSetting = optionsMonitor.CurrentValue;
        }

        public AccountIdDTO CreateAccount(AccountDTO user)
        {
            var checkUser = account.GetAccount().SingleOrDefault(us => us.UserName == user.UserName);
            if(checkUser != null)
            {
                throw new Exception("User invalid");
            }

            var addAccount = new Account
            {
                UserName = user.UserName,
                PassWord = user.PassWord,
                Name = user.Name,
                Email = user.Email,
                AccountStatus = true
            };
            account.CreateAccount(addAccount);
            return new AccountIdDTO
            {
                
                UserName = addAccount.UserName,
                PassWord = addAccount.PassWord,
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
                UserName = ac.UserName,
                PassWord = ac.PassWord,
                Name = ac.Name,
                Email = ac.Email,
                AccountStatus = ac.AccountStatus
            });
            return User.ToList();
        }

        public ApiResponse Login(Login login)
        {
            var checkUser = account.GetAccount().SingleOrDefault(us => us.UserName == login.UserName && us.PassWord == login.PassWord);
            if (checkUser == null)
            {
                var result = new ApiResponse
                {
                    Success = false,
                    Message = "Username or Password invalid",
                    Data = null
                };
                return result;
            }
            else 
            { 
                var result = new ApiResponse
                {
                    Success = true,
                    Message = "Loggin successfully",
                    Data = GenerateToken(checkUser)
                };
                return result; 
            }
        }

        private string GenerateToken(Account account)
        {
            var jwtToken = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSetting.SecretKey);
            var jwtTokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim (ClaimTypes.Name, account.Name),
                    new Claim (ClaimTypes.Email, account.Email),
                    new Claim ("Username", account.UserName),
                    new Claim ("Id", account.Id.ToString()),
                    new Claim ("AccountStatus", account.AccountStatus.ToString()),

                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha512Signature)

            };
            var token = jwtToken.CreateToken(jwtTokenDescription);
            return jwtToken.WriteToken(token);
        }
    }
}
