using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class DetailRoundDataAccess
    {
        public List<RoundDetail> GetRoundDetail()
        {
            var dataBase = new MyDbContext();
            var roundDetail = dataBase.RoundDetails.Select(x => new RoundDetail
            {
                Id = x.Id,
                RoundId = x.RoundId,
                GameId = x.GameId
                
            });
            return roundDetail.ToList();
        }

        public void CreateRoundDetail(DetailRoundDTO detailRoundDTO)
        {
            var dataBase = new MyDbContext();
            var checkRoundDetail = dataBase.RoundDetails.SingleOrDefault(x=>x.RoundId == detailRoundDTO.RoundId && x.GameId == detailRoundDTO.GameId);
            if(checkRoundDetail == null)
            {
                var result = new RoundDetail
                {
                    RoundId = detailRoundDTO.RoundId,
                    GameId = detailRoundDTO.GameId
                    
                };
                dataBase.Add(result);
                dataBase.SaveChanges();
            }    
        }

        public void UpdateRoundDetail(int id, DetailRoundDTO detailRoundDTO)
        {
            var dataBase = new MyDbContext();
            var checkRoundDetail = dataBase.RoundDetails.SingleOrDefault(x => x.Id == id);
            if(checkRoundDetail != null)
            {
                
                checkRoundDetail.GameId = detailRoundDTO.GameId;
                dataBase.SaveChanges();
            }
        }

        public void DeleteRoundDetail(int id)
        {
            var dataBase = new MyDbContext();
            var checkRoundDetail = dataBase.RoundDetails.SingleOrDefault(x => x.Id == id);
            if (checkRoundDetail != null)
            {
                dataBase.Remove(checkRoundDetail);
                dataBase.SaveChanges();
            }
        }
    }
}
