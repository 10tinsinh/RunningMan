using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningManApi.Repository;
using RunningManApi.Repository.Entites;

namespace RunningManApi.Service
{
    public class AccountRepository : IAccountRepository
    {
        private Repository.UserDataAccess account;
        public AccountRepository()
        {
            account = new UserDataAccess();
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

    }
}
