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
        private readonly GamePlayDataAccess gamePlayData;

        public GamePlayRepository()
        {
            teamDetailData = new DetailTeamDataAccess();
            gamePlayData = new GamePlayDataAccess();
        }

        public GamePlayIdDTO CreateGamePlay(int id, GamePlayDTO gamePlayDTO)
        {
            var checkTeam = teamDetailData.GetTeamDetail().Where(x => x.AccountId == id);
            if(checkTeam == null)
            {
                throw new Exception();
            }    

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
