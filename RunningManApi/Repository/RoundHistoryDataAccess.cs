using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class RoundHistoryDataAccess
    {
        public List<RoundHistory> GetRoundHistory()
        {
            var dataBase = new MyDbContext();
            var roundHistory = dataBase.RoundHistories.Select(x => new RoundHistory
            {
                Id = x.Id,
                RoundId = x.RoundId,
                AccountId = x.AccountId,
                Times = x.Times

            });
            return roundHistory.ToList();
        }

        public void CreateRoundHistory(RoundHistoryDTO roundHistory)
        {
            var dataBase = new MyDbContext();
            var roundData = new RoundHistory
            {
                RoundId = roundHistory.RoundId,
                AccountId = roundHistory.AccountId,
                Times = roundHistory.Times
            };
            dataBase.Add(roundData);
            dataBase.SaveChanges();
        }

        public void UpdateGameHistory(int id, RoundHistoryDTO roundHistory)
        {
            var dataBase = new MyDbContext();
            var roundData = dataBase.RoundHistories.SingleOrDefault(x => x.Id == id);
            if (roundData != null)
            {
                if (roundHistory.RoundId != 0)
                {
                    roundData.RoundId = roundHistory.RoundId;
                }
                if (roundHistory.AccountId != 0)
                {
                    roundData.AccountId = roundHistory.AccountId;
                }
                if (roundHistory.Times != null)
                {
                    roundData.Times = roundHistory.Times;
                }
                dataBase.SaveChanges();
            }
        }

        public void DeleteGameHistory(int id)
        {
            var dataBase = new MyDbContext();
            var roundData = dataBase.RoundHistories.SingleOrDefault(x => x.Id == id);
            if (roundData != null)
            {
                dataBase.Remove(roundData);
                dataBase.SaveChanges();
            }
        }
    }
}
