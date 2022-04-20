using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class GamePlayDataAccess
    {
        private readonly MyDbContext dataBase;

        public GamePlayDataAccess()
        {
            dataBase = new MyDbContext();
        }

        public List<GamePlay> GetGamePlay()
        {

            var gamePlay = dataBase.GamePlays.Select(x => new GamePlay
            {
                Id = x.Id,
                Rank= x.Rank,
                BonusPoints =x.BonusPoints,
                Date = x.Date,
                GameId = x.GameId,
                TeamId = x.TeamId
            });

            return gamePlay.ToList();
        }

        public void CreateGamePlay(GamePlayDTO gamePlayDTO)
        {
            var gamePlay = new GamePlay
            {
                Rank = gamePlayDTO.Rank,
                BonusPoints = gamePlayDTO.BonusPoints,
                Date = gamePlayDTO.Date,
                GameId = gamePlayDTO.GameId,
                TeamId = gamePlayDTO.TeamId
            };
            dataBase.Add(gamePlay);
            dataBase.SaveChanges();
        }

        public void UpdateGamePlay(int id, GamePlayDTO gamePlayDTO)
        {
            var checkGamePlay = dataBase.GamePlays.SingleOrDefault(x => x.Id == id);
            if(checkGamePlay != null)
            {
                if(gamePlayDTO.Rank != null)
                {
                    checkGamePlay.Rank = gamePlayDTO.Rank;
                }
                if (gamePlayDTO.BonusPoints != null)
                {
                    checkGamePlay.BonusPoints = gamePlayDTO.BonusPoints;
                }
                if (gamePlayDTO.GameId != null)
                {
                    checkGamePlay.GameId = gamePlayDTO.GameId;
                }
                if (gamePlayDTO.TeamId != null)
                {
                    checkGamePlay.TeamId = gamePlayDTO.TeamId;
                }
                dataBase.SaveChanges();
            }    
        }

        public void DeleteGamePlay(int id)
        {
            var checkGamePlay = dataBase.GamePlays.SingleOrDefault(x => x.Id == id);
            if (checkGamePlay != null)
            {
                dataBase.Remove(checkGamePlay);
            }    
        }
    }
}
