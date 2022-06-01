using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IGamePlayRepository
    {
        List<GameViewDTO> GetGamePlay(int idGamePlay, int page);
        GamePlayIdDTO CreateGamePlay(int teamId,int id);
        void UpdateGamePlay(int id, GamePlayDTO gamePlayDTO);

        void DeleteGamePlay(int id);
    }
}
