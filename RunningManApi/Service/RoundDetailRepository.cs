using RunningManApi.DTO.Models;
using RunningManApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public class RoundDetailRepository : IRoundDetailRepository
    {
        private readonly DetailRoundDataAccess roundDetailData;
        private readonly GameDataAccess gameData;
        private readonly RoundDataAccess roundData;

        public RoundDetailRepository()
        {
            roundDetailData = new DetailRoundDataAccess();
            gameData = new GameDataAccess();
            roundData = new RoundDataAccess();
        }

        public void CreateRoundDetail(int idRound)
        {
            var checkRound = roundData.GetRound().SingleOrDefault(x => x.Id == idRound);
            if (checkRound == null)
            {
                throw new Exception();
            }
            var checkRoundId = roundDetailData.GetRoundDetail().SingleOrDefault(x => x.RoundId == idRound);
            if(checkRoundId != null)
            {
                throw new Exception();
            }
            var getRound = roundData.GetRound().SingleOrDefault(x => x.Id == idRound);
            var getGame = gameData.GetGames().Where(x => x.Level == getRound.Level);
            if(!getGame.Any())
            {
                throw new Exception();
            }
            if(getGame.Count() > 5)
            {
                int i = 1;
                while(i <= 5)
                {
                    Random rnd = new Random();
                    var game = getGame.OrderBy(x => rnd.Next()).Take(1).FirstOrDefault();
                    var checkRndGame = roundDetailData.GetRoundDetail().SingleOrDefault(x => x.RoundId == idRound && x.GameId == game.Id);
                    if(checkRndGame == null)
                    {
                        i++;
                        var result = new DetailRoundDTO
                        {
                            GameId = game.Id,
                            RoundId = idRound
                        };
                        roundDetailData.CreateRoundDetail(result);
                    }
                    
                }
            } 
            else
            {
                foreach (int id in getGame.Select(x => x.Id))
                {
                    var checkRndGame = roundDetailData.GetRoundDetail().SingleOrDefault(x => x.RoundId == idRound && x.GameId == id);
                    if (checkRndGame == null)
                    {
                        var result = new DetailRoundDTO
                        {
                            GameId = id,
                            RoundId = idRound
                        };
                        roundDetailData.CreateRoundDetail(result);
                    }
                }    

            }    

        }

        public void DeleteRoundDetail(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetailRoundDTO> GetRoundDetail(string roundName)
        {
            if(!string.IsNullOrEmpty(roundName))
            {
                var checkRound = roundData.GetRound().SingleOrDefault(x => x.Name == roundName);
                var checkRoundDetail = roundDetailData.GetRoundDetail().Where(x => x.RoundId == checkRound.Id);
                var listGame = new List<DetailRoundDTO>();
                foreach (int id in checkRoundDetail.Select(x => x.Id))
                {
                    var getDetailRound = roundDetailData.GetRoundDetail().SingleOrDefault(x => x.Id == id);
                    var result = new DetailRoundDTO
                    {
                        Id = getDetailRound.Id,
                        GameId = getDetailRound.GameId,
                        RoundId = getDetailRound.RoundId
                    };
                    listGame.Add(result);
                }

                return listGame.ToList();
            }
            var getRoundDetail = roundDetailData.GetRoundDetail().AsQueryable();
            var roundDetail = getRoundDetail.Select(x => new DetailRoundDTO
            {
                Id = x.Id,
                GameId=x.GameId,
                RoundId = x.RoundId
            });
            return roundDetail.ToList();
            
        }

        public void UpdateRoundDetail(int id, DetailRoundDTO detailRoundDTO)
        {
            throw new NotImplementedException();
        }
    }
}
