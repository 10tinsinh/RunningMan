using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IRoundRepository
    {
        List<RoundIdDTO> GetRound(string name);
        RoundIdDTO CreateRound(int id,RoundDTO roundDTO);
        void UpdateRound(int id, RoundDTO roundDTO);
        void DeleteRound(string name);
    }
}
