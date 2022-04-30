using RunningManApi.DTO.Models;
using RunningManApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public class GamePlayRepository : IGamePlayRepository
    {
        private readonly DetailTeamDataAccess teamDetailData;
        private readonly TeamDataAccess teamData;
        private readonly GamePlayDataAccess gamePlayData;
        private readonly GameDataAccess gameData;

        public GamePlayRepository()
        {
            teamDetailData = new DetailTeamDataAccess();
            teamData = new TeamDataAccess();
            gamePlayData = new GamePlayDataAccess();
            gameData = new GameDataAccess();
        }

        public GamePlayIdDTO CreateGamePlay(int teamId,int id)
        {

            throw new NotImplementedException();
        }

        public void DeleteGamePlay(int id)
        {
            throw new NotImplementedException();
        }

        public List<GamePlayIdDTO> GetGamePlay(string account)
        {
            throw new NotImplementedException();
        }

        public void UpdateGamePlay(int id, GamePlayDTO gamePlayDTO)
        {
            throw new NotImplementedException();
        }
    }
}
