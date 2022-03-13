using RunningManApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RunningManApi.Repository.Entites;

namespace RunningManApi.Repository
{
    public class UserDataAccess
    {
   
        public List<Account> GetAccount()
        {
            var dataBase = new MyDbContext();
            var account = dataBase.Accounts.Select(ac => new Account
            {
                Id = ac.Id,
                UserName = ac.UserName,
                PassWord = ac.PassWord,
                Name = ac.Name,
                Email = ac.Email,
                AccountStatus = ac.AccountStatus
            });
            return account.ToList();
        }
    }
}
