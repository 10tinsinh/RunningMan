using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class GameHistoryDataAccess
    {
        public List<GameHistory> GetGameHistory()
        {
            var dataBase = new MyDbContext();
            var gameHistory = dataBase.GameHistories.Select(x=> new GameHistory 
            {
                Id = x.Id,
                GameId = x.GameId,
                AccountId = x.AccountId,
                Times = x.Times
            
            });
            return gameHistory.ToList();
        }

        public void CreateGameHistory(GameHistoryDTO gameHistory)
        {
            var dataBase = new MyDbContext();
            var gameData = new GameHistory
            {
                GameId = gameHistory.GameId,
                AccountId = gameHistory.AccountId,
                Times = 1
            };
            dataBase.Add(gameData);
            dataBase.SaveChanges();
        }

        public void UpdateGameHistory(int id, GameHistoryDTO gameHistory)
        {
            var dataBase = new MyDbContext();
            var gameData = dataBase.GameHistories.SingleOrDefault(x => x.Id == id);
            if (gameData != null)
            {
                if (gameHistory.GameId != 0)
                {
                    gameData.GameId = gameHistory.GameId;
                }
                if (gameHistory.AccountId != 0)
                {
                    gameData.AccountId = gameHistory.AccountId;
                }
                if (gameHistory.Times != null)
                {
                    gameData.Times = gameHistory.Times;
                }
                dataBase.SaveChanges();
            }
        }

        public void DeleteGameHistory(int id)
        {
            var dataBase = new MyDbContext();
            var gameData = dataBase.GameHistories.SingleOrDefault(x => x.Id == id);
            if(gameData != null)
            {
                dataBase.Remove(gameData);
                dataBase.SaveChanges();
            }    
        }
    }
}
