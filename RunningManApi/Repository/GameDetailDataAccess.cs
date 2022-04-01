using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class GameDetailDataAccess
    {
        public List<GameDetail> GetGameDetail()
        {
            var dataBase = new MyDbContext();
            var gameDetail = dataBase.GameDetails.Select(x => new GameDetail
            {
                GameId = x.GameId,
                GameTypeId = x.GameTypeId,
                GameRules = x.GameRules
            });
            return gameDetail.ToList();
        }

        public void CreateGameDetail(GameDetailDTO gameDetail)
        {
            var dataBase = new MyDbContext();
            var _gameDetail = new GameDetail
            {
                GameId = gameDetail.GameId,
                GameTypeId = gameDetail.GameTypeId,
                GameRules = gameDetail.GameRules
            };
            dataBase.Add(_gameDetail);
            dataBase.SaveChanges();
        }

        public void UpdateGameDetail(int gameId, int gameTypeId, GameDetailDTO game)
        {
            var dataBase = new MyDbContext();
            var _gameDetail = dataBase.GameDetails.SingleOrDefault(x => x.GameId == gameId && x.GameTypeId == gameTypeId);
            if (_gameDetail != null)
            {
                _gameDetail.GameRules = game.GameRules;
                
                dataBase.SaveChanges();
            }
        }

        public void DeleteGameDetail(int gameId, int gameTypeId)
        {
            var dataBase = new MyDbContext();
            var _gameDetail = dataBase.GameDetails.SingleOrDefault(x => x.GameId == gameId && x.GameTypeId == gameTypeId);
            if (_gameDetail != null)
            {
                dataBase.Remove(_gameDetail);
                dataBase.SaveChanges();
            }

        }
    }
}
