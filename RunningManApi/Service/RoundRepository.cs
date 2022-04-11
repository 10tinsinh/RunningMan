using RunningManApi.DTO.Models;
using RunningManApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public class RoundRepository:IRoundRepository
    {
        private readonly RoundDataAccess roundData;

        public RoundRepository()
        {
            roundData = new RoundDataAccess();
        }

        public RoundIdDTO CreateRound(int id,RoundDTO roundDTO)
        {
            var checkRound = roundData.GetRound().SingleOrDefault(x => x.Name == roundDTO.Name);
            if(checkRound != null)
            {
                throw new Exception();
            }
            var round = new RoundDTO
            {
                Name = roundDTO.Name,
                LocationId = roundDTO.LocationId,
                AccountId = id
            };
            roundData.CreateRound(round);
            var newRound = roundData.GetRound().SingleOrDefault(x => x.Name == roundDTO.Name);
            var result = new RoundIdDTO
            {
                Id = newRound.Id,
                Name = newRound.Name,
                LocationId = newRound.LocationId,
                AccountId = newRound.AccountId
            };
            return result;
        }

        public void DeleteRound(string name)
        {
            var checkRound = roundData.GetRound().SingleOrDefault(x => x.Name == name);
            if(checkRound == null)
            {
                throw new Exception();
            }
            roundData.DeleteRound(checkRound.Id);
        }

        public List<RoundIdDTO> GetRound(string name)
        {
            var round = roundData.GetRound().AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                round = round.Where(x => x.Name.Contains(name));
            }
            var result = round.Select(x => new RoundIdDTO
            {
                Id = x.Id,
                Name = x.Name,
                LocationId = x.LocationId,
                AccountId = x.AccountId
            });
            return result.ToList();
        }

        public void UpdateRound(int id, RoundDTO roundDTO)
        {
            var checkRound = roundData.GetRound().SingleOrDefault(x => x.Id == id);
            if(checkRound == null)
            {
                throw new Exception();
            }
            var round = new RoundDTO
            {
                Name = roundDTO.Name,
                LocationId = roundDTO.LocationId
            };
            roundData.UpdateRound(id, round);
        }
    }
}
