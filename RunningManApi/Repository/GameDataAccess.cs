using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class GameDataAccess
    {
        public List<Game> GetGames ()
        {
            var dataBase = new MyDbContext();
            var game = dataBase.Games.Select(x=> new Game
            {
                Id = x.Id,
                Name = x.Name,
                Level =x.Level,
                AccountId=x.AccountId,
                GameTypeId = x.GameTypeId,
                GameRules = x.GameRules
                
            });
            return game.ToList();
        }

        public void CreateGame (GameDTO game)
        {
            var dataBase = new MyDbContext();
            var _game = new Game
            {
                Name = game.Name,
                AccountId = game.AccountId,
                GameTypeId = game.GameTypeId,
                GameRules = game.GameRules
            };
            dataBase.Add(_game);
            dataBase.SaveChanges();
        }

        public void UpdateGame (int id, GameDTO game)
        {
            var dataBase = new MyDbContext();
            var _game = dataBase.Games.SingleOrDefault(x => x.Id == id);
            if(_game != null)
            {
                if(game.Name != null)
                {
                    _game.Name = game.Name;
                }
                if (game.Level != null)
                {
                    _game.Level = game.Level;
                }
                if (game.GameRules != null)
                {
                    _game.GameRules = game.GameRules;
                }
                if (game.GameTypeId != 0)
                {
                    _game.GameTypeId = game.GameTypeId;
                }

                
                
                dataBase.SaveChanges();
            }
        }

        public void DeleteGame (int id)
        {
            var dataBase = new MyDbContext();
            var _game = dataBase.Games.SingleOrDefault(x => x.Id == id);
            if (_game != null)
            {
                dataBase.Remove(_game);
                dataBase.SaveChanges();
            }
           
        }
    }
}
