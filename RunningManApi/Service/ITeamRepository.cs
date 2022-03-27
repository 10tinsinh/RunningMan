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
        TeamIdDTO CreateNewTeam(int id, TeamDTO team);
        void UpdateTeam(int idUser, string nameTeam, TeamDTO team);

        void DeleteTeam(int id, string teamName);
        void JoinTeam(int id, string team);
        ApiResponse AddMemberIntoTeam(int idTeamLead, string user, string team);
        ApiResponse KickMember(int idTeamLead, string user, string team);

    }
}
