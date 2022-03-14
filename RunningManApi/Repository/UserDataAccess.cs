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
        //private readonly MyDbContext dataBase;

        //public UserDataAccess(MyDbContext myDbContext)
        //{
        //    dataBase = myDbContext;
        //}
        //public UserDataAccess() { }


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
        public void CreateAccount(Account account)
        {
            var dataBase = new MyDbContext();
            var _account = new Account
            {

                UserName = account.UserName,
                PassWord = account.PassWord,
                Name = account.Name,
                Email = account.Email,
                AccountStatus = account.AccountStatus
            };
            dataBase.Add(_account);
            dataBase.SaveChanges();

        }
        public void DeleteAccount(int id)
        {
            var dataBase = new MyDbContext();
            var account = dataBase.Accounts.SingleOrDefault(ac => ac.Id == id);
            if(account != null)
            {
                dataBase.Remove(account);
                dataBase.SaveChanges();
            }    
        }

        public void UpdateAccount(int id,Account account)
        {
            var dataBase = new MyDbContext();
            var _account = dataBase.Accounts.SingleOrDefault(ac => ac.Id == id);
            if (account != null)
            {
                _account.UserName = account.UserName;
                _account.PassWord = account.PassWord;
                _account.Name = account.Name;
                _account.Email = account.Email;
                dataBase.SaveChanges();
            }
        }
    }
}
