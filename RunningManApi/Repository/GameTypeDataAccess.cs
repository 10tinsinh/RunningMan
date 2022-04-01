using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class GameTypeDataAccess
    {
        public List<GameType> GetGameType()
        {
            var dataBase = new MyDbContext();
            var gameType = dataBase.GameTypes.Select(x => new GameType
            {
                Id = x.Id,
                Name = x.Name
            });
            return gameType.ToList();
        }

        public void CreateGameType(GameTypeDTO gameType)
        {
            var dataBase = new MyDbContext();
            var _gameType = new GameType
            {
                Name = gameType.Name
            };
            dataBase.Add(_gameType);
            dataBase.SaveChanges();
        }

        public void UpdateGameType(int id, GameTypeDTO gameType)
        {
            var dataBase = new MyDbContext();
            var _gameType = dataBase.GameTypes.SingleOrDefault(x => x.Id == id);
            if (_gameType != null)
            {
                _gameType.Name = gameType.Name;
                
                dataBase.SaveChanges();
            }
        }

        public void DeleteGameType(int id)
        {
            var dataBase = new MyDbContext();
            var _gameType = dataBase.GameTypes.SingleOrDefault(x => x.Id == id);
            if (_gameType != null)
            {
                dataBase.Remove(_gameType);
                dataBase.SaveChanges();
            }

        }
    }
}
