using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class RoundDataAccess
    {
        public List<Round> GetRound()
        {
            var dataBase = new MyDbContext();
            var round = dataBase.Rounds.Select(x => new Round
            {
                Id = x.Id,
                Name = x.Name,
                LocationId = x.LocationId,
                AccountId = x.AccountId,
                BonusPoints = x.BonusPoints,
                Level = x.Level
                
            });
            return round.ToList();
        }

        public void CreateRound(RoundDTO roundDTO)
        {
            var dataBase = new MyDbContext();
            var checkRound = dataBase.Rounds.SingleOrDefault(x => x.Name == roundDTO.Name);
            if(checkRound == null)
            {
                var round = new Round
                {
                    Name = roundDTO.Name,
                    LocationId = roundDTO.LocationId,
                    AccountId = roundDTO.AccountId,
                    BonusPoints = roundDTO.BonusPoints,
                    Level = roundDTO.Level

                };
                dataBase.Add(round);
                dataBase.SaveChanges();
            }
        }

        public void UpdateRound(int id, RoundDTO roundDTO)
        {
            var dataBase = new MyDbContext();
            var checkRound = dataBase.Rounds.SingleOrDefault(x => x.Id == id);
            if(checkRound != null)
            {
                if(roundDTO.Name != null)
                {
                    checkRound.Name = roundDTO.Name;
                }
                if (roundDTO.LocationId != 0)
                {
                    checkRound.LocationId = roundDTO.LocationId;
                }
                if (roundDTO.BonusPoints != 0)
                {
                    checkRound.BonusPoints = roundDTO.BonusPoints;
                }

                dataBase.SaveChanges();
            }
        }

        public void DeleteRound(int id)
        {
            var dataBase = new MyDbContext();
            var checkRound = dataBase.Rounds.SingleOrDefault(x => x.Id == id);
            if(checkRound != null)
            {
                dataBase.Remove(checkRound);
                dataBase.SaveChanges();
            }
        }
    }
}
