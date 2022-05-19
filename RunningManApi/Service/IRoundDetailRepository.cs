using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface IRoundDetailRepository
    {
        List<DetailRoundDTO> GetRoundDetail(string roundName);

        void CreateRoundDetail(int idRound);

        void DeleteRoundDetail(int id);

        void UpdateRoundDetail(int id, DetailRoundDTO detailRoundDTO);
    }
}
