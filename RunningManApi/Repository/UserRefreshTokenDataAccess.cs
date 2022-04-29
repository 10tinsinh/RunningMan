using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class UserRefreshTokenDataAccess
    {
        public List<UserRefreshToken> GetUserRefreshTokens()
        {
            var dataBase = new MyDbContext();
            var userRefreshToken = dataBase.UserRefreshTokens.Select(x => new UserRefreshToken
            {
                Id = x.Id,
                UserName = x.UserName,
                RefreshToken = x.RefreshToken,
                IsActive = x.IsActive
            });
            return userRefreshToken.ToList();
        }

        public UserRefreshToken CreateUserRefreshToken(UserRefreshTokenDTO userRefreshToken)
        {
            var dataBase = new MyDbContext();
            var userRefresh = new UserRefreshToken
            {
                UserName = userRefreshToken.UserName,
                RefreshToken = userRefreshToken.RefreshToken
                
            };
            dataBase.Add(userRefresh);
            dataBase.SaveChanges();
            return userRefresh;

        }

        public void DeleteUserRefreshToken(int id)
        {
            var dataBase = new MyDbContext();
            var checkUserRefreshToken = dataBase.UserRefreshTokens.SingleOrDefault(x => x.Id == id);
            if(checkUserRefreshToken != null)
            {
                dataBase.Remove(checkUserRefreshToken);
                dataBase.SaveChanges();
            }    
        }
    }
}
