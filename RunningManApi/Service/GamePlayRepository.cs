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
        private readonly RoundDataAccess roundData;
        private readonly DetailRoundDataAccess roundDetailData;

        public GamePlayRepository()
        {
            teamDetailData = new DetailTeamDataAccess();
            teamData = new TeamDataAccess();
            gamePlayData = new GamePlayDataAccess();
            gameData = new GameDataAccess();
            roundData = new RoundDataAccess();
            roundDetailData = new DetailRoundDataAccess();
        }

        public GamePlayIdDTO CreateGamePlay(int teamId,int id)
        {
            var teamUser = teamDetailData.GetTeamDetail().SingleOrDefault(x => x.TeamId == teamId && x.AccountId == id);
            if(teamUser == null)
            {
                throw new Exception();
            }
            var team = teamData.GetTeam().SingleOrDefault(x => x.Id == teamUser.TeamId);
            var roundPlay = roundData.GetRound().Where(x => x.Level == team.Rank);
            int count = roundPlay.Count();
            var result = new GamePlayDTO();
            int i = 0;
            while (i <= count)
            {
                Random rnd = new Random();
                var gamePlay = roundPlay.OrderBy(x => rnd.Next()).Take(1).FirstOrDefault();

                result.Rank = team.Rank;
                result.Date = DateTime.UtcNow;
                result.RoundId = gamePlay.Id;
                result.TeamId = teamUser.TeamId;
                
                var checkGamePlay = gamePlayData.GetGamePlay().SingleOrDefault(x => x.RoundId == result.RoundId && x.TeamId == result.TeamId);
                if (checkGamePlay == null)
                {
                    gamePlayData.CreateGamePlay(result);
                    break;
                }
                i++;
            }
            var getGamePlay = gamePlayData.GetGamePlay().SingleOrDefault(x => x.RoundId == result.RoundId && x.TeamId == result.TeamId);
            return new GamePlayIdDTO
            {
                Id = getGamePlay.Id,
                Rank = getGamePlay.Rank,
                Date = getGamePlay.Date,
                RoundId = getGamePlay.RoundId,
                TeamId = getGamePlay.TeamId
            };

        }

        public void DeleteGamePlay(int id)
        {
            throw new NotImplementedException();
        }

        public List<GameViewDTO> GetGamePlay(int idGamePlay, int page)
        {
            var gameplay = gamePlayData.GetGamePlay().SingleOrDefault(x => x.Id == idGamePlay);
            if(gameplay == null)
            {
                throw new Exception();
            }
            var round = roundData.GetRound().SingleOrDefault(x => x.Id == gameplay.RoundId);
            var roundDetail = roundDetailData.GetRoundDetail().Where(x => x.RoundId == round.Id);

            #region Paging
            roundDetail = roundDetail.Skip((page - 1) * 1).Take(1);
            #endregion

            var AllQuestion = new List<GameViewDTO>();
            foreach(int idGame in roundDetail.Select(x=>x.GameId))
            {
                var game = gameData.GetGames().SingleOrDefault(x => x.Id == idGame);
                var newquestion = new GameViewDTO
                {
                    Name = game.Name,
                    GameRules = game.GameRules,
                    Question = game.Question,
                    Hint1 = game.Hint1,
                    Hint2 = game.Hint2,
                    Answer = game.Answer
                };
                AllQuestion.Add(newquestion);
            }

            return AllQuestion.ToList();
        }

        public void UpdateGamePlay(int id, GamePlayDTO gamePlayDTO)
        {
            throw new NotImplementedException();
        }
    }
}
