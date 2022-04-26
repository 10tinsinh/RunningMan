using RunningManApi.DTO.Models;
using RunningManApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public class GameRepository : IGameRepository
    {
        private readonly GameDataAccess gameData;

        public GameRepository()
        {
            gameData = new GameDataAccess();
        }

        public GameDTO CreateGame(int id,GameDTO gameDTO)
        {
            var checkGame = gameData.GetGames().SingleOrDefault(x=>x.Name == gameDTO.Name);
            if(checkGame != null)
            {
                throw new Exception();
            }
            var result = new GameDTO
            {
                Name = gameDTO.Name,
                AccountId = id,
                GameTypeId = gameDTO.GameTypeId,
                GameRules = gameDTO.GameRules
            };
            gameData.CreateGame(result);
            var game = gameData.GetGames().SingleOrDefault(x => x.Name == gameDTO.Name);
            return new GameIdDTO
            {
                Id = game.Id,
                Name = game.Name,
                Level = game.Level,
                AccountId = game.AccountId,
                GameTypeId = game.GameTypeId,
                GameRules = game.GameRules
            };
        }

        public void DeleteGame(string name)
        {
            var checkGame = gameData.GetGames().SingleOrDefault(x => x.Name == name);
            if(checkGame == null)
            {
                throw new Exception();
            }
            gameData.DeleteGame(checkGame.Id);
        }

        public List<GameIdDTO> GetGame(string name)
        {
            var getGame = gameData.GetGames().AsQueryable();
            if(!string.IsNullOrEmpty(name))
            {
                getGame = getGame.Where(x => x.Name.Contains(name));
            }
            var result = getGame.Select(x => new GameIdDTO
            {
                Id = x.Id,
                Name = x.Name,
                Level = x.Level,
                AccountId = x.AccountId,
                GameTypeId = x.GameTypeId,
                GameRules = x.GameRules

            });
            return result.ToList();
        }

        public void UpdateGame(int id, GameDTO gameDTO)
        {
            var checkGame = gameData.GetGames().SingleOrDefault(x => x.Id == id);
            if(checkGame == null)
            {
                throw new Exception();

            }
            var game = new GameDTO
            {
                Name = gameDTO.Name,
                AccountId = gameDTO.AccountId,
                GameTypeId = gameDTO.GameTypeId,
                GameRules = gameDTO.GameRules,
                Level = gameDTO.Level
            };
            gameData.UpdateGame(id, game);
        }
    }
}
