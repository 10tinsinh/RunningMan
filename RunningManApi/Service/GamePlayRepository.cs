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
            var checkTeam = teamDetailData.GetTeamDetail().SingleOrDefault(x => x.AccountId == id && x.TeamId == teamId);
            if(checkTeam == null)
            {
                throw new Exception();
            }

            var teamPlay = teamData.GetTeam().SingleOrDefault(x => x.Id == teamId);

            var checkGame = gameData.GetGames().Where(x => x.Level == teamPlay.Rank);
            if(!checkGame.Any())
            {
                throw new Exception();
            }
            
            Random rnd = new Random();
            // Get Random One Game in List checkGame
            var game = checkGame.OrderBy(x => rnd.Next()).Take(1).FirstOrDefault();
            

            var gamePlay = new GamePlayDTO
            {
                Rank = teamPlay.Rank,
                BonusPoints = 100,
                Date = DateTime.UtcNow,
                GameId = game.Id,
                TeamId = teamId

            };
            var result = gamePlayData.GetGamePlay().SingleOrDefault(x => x.TeamId == teamId && x.GameId == game.Id);
            if(result != null)
            {
                throw new Exception();
            }    
            gamePlayData.CreateGamePlay(gamePlay);
            var getGamePlay = gamePlayData.GetGamePlay().SingleOrDefault(x => x.TeamId == teamId && x.GameId == game.Id);
            return new GamePlayIdDTO
            { 
                Id = getGamePlay.Id,
                Rank = getGamePlay.Rank,
                BonusPoints = getGamePlay.BonusPoints,
                Date = getGamePlay.Date,
                GameId = getGamePlay.GameId,
                TeamId = getGamePlay.TeamId
            };

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
