using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IGameTypeRepository
    {
        List<GameTypeDTO> GetGameType(string name);
        GameTypeDTO CreateGameType(GameTypeDTO gameTypeDTO);
        void UpdateGameType(int id, GameTypeDTO gameTypeDTO);
        void DeleteGameType(int id);
    }
}
