using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IGamePlayRepository
    {
        List<GamePlayIdDTO> GetGamePlay(string account);
        GamePlayIdDTO CreateGamePlay(int id,GamePlayDTO gamePlayDTO);
        void UpdateGamePlay(int id, GamePlayDTO gamePlayDTO);

        void DeleteGamePlay(int id);
    }
}
