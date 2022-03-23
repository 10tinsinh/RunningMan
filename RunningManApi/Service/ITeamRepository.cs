using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface ITeamRepository
    {
        List<TeamIdDTO> GetTeam(int user);
        Team CreateNewTeam(TeamDTO team);
        void UpdateTeam(TeamIdDTO team);

        void DeleteTeam(int id);

    }
}
