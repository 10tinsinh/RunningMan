using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RunningManApi.Repository;

namespace RunningManApi.Service
{
    public class AccountRepository : IAccountRepository
    {
        private Repository.UserDataAccess account;
        public AccountRepository()
        {
            account = new UserDataAccess();
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
