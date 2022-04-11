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
                AccountId = x.AccountId
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
                    AccountId = roundDTO.AccountId
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
                checkRound.Name = roundDTO.Name;
                
                checkRound.LocationId = roundDTO.LocationId;
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
