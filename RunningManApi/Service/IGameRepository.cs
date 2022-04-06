using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IGameRepository
    {
        List<GameIdDTO> GetGame(string name);
        GameDTO CreateGame(int id,GameDTO gameDTO);
        void UpdateGame(int id, GameDTO gameDTO);
        void DeleteGame(string name);
    }
}
