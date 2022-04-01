using RunningManApi.DTO.Models;
using RunningManApi.Repository;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public class GameTypeRepository : IGameTypeRepository
    {
        private readonly GameTypeDataAccess gameTypeData;

        public GameTypeRepository()
        {
            gameTypeData = new GameTypeDataAccess();
        }

        public GameTypeDTO CreateGameType( GameTypeDTO gameTypeDTO)
        {
            var _gameType = new GameTypeDTO
            {
                Name = gameTypeDTO.Name

            };
            gameTypeData.CreateGameType(_gameType);
            var gametype = gameTypeData.GetGameType().SingleOrDefault(x => x.Name == gameTypeDTO.Name);
            var result = new GameTypeDTO
            {
                Id = gametype.Id,
                Name = gametype.Name
            };
            return result;

        }

        public void DeleteGameType(int id)
        {
            var checkGameType = gameTypeData.GetGameType().SingleOrDefault(x => x.Id == id);
            if(checkGameType != null)
            {
                gameTypeData.DeleteGameType(id);
            }
        }

        public List<GameTypeDTO> GetGameType(string name)
        {
            var gameType = gameTypeData.GetGameType().AsQueryable();
            if(name != null)
            {
                gameType = gameType.Where(x => x.Name.Contains(name));
            }
            var result = gameType.Select(x => new GameTypeDTO {
                Id = x.Id,
                Name = x.Name
            
            });
            return result.ToList();
        }

        public void UpdateGameType(int id, GameTypeDTO gameTypeDTO)
        {
            var checkGameType = gameTypeData.GetGameType().SingleOrDefault(x => x.Id == id);
            if(checkGameType != null)
            {
                gameTypeData.UpdateGameType(id, gameTypeDTO);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
