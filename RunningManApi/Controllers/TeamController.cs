using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunningManApi.DTO.Models;
using RunningManApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [Authorize(Policy = PolicyCode.TEAM_MEMBER)]
        [HttpGet("GetTeamUserLogin")]
        public IActionResult GetTeamUserLogin()
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var usernameToken = identity.Value;
            try
            {
                var result = _teamRepository.GetTeam(int.Parse(usernameToken));
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy =PolicyCode.CREATE_TEAM)]
        [HttpPost("CreateATeam")]
        public IActionResult CreateTeam(TeamDTO team)
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var idUser = identity.Value;
            try
            {
                var result = _teamRepository.CreateNewTeam(int.Parse(idUser), team);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.TEAM_MEMBER)]
        [HttpDelete("LeaveTeam")]
        public IActionResult LeaveTeam(string team)
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var idUser = identity.Value;
            try
            {
                _teamRepository.DeleteTeam(int.Parse(idUser), team);
                return Ok (new ApiResponse
                {
                    Success = true,
                    Message = "You have successfully leave the Team",
                    Data = null
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "You have left the failure team or You don't exist in team",
                    Data = null
                });
            }
        }

        [Authorize(Policy =PolicyCode.JOIN_TEAM)]
        [HttpPost("JoinTeam")]
        public IActionResult JoinTeam(string team)
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var idUser = identity.Value;
            try
            {
                _teamRepository.JoinTeam(int.Parse(idUser), team);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "You have successfully join the Team",
                    Data = null
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "You already exist in the team or the team does not exist",
                    Data = null
                });
            }
        }

        [Authorize(Policy =PolicyCode.TEAM_LEAD)]
        [HttpPost("AddMember")]
        public IActionResult AddMember(string member, string team)
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var idUser = identity.Value;
            try
            {
                return Ok(_teamRepository.AddMemberIntoTeam(int.Parse(idUser), member, team));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.TEAM_LEAD)]
        [HttpDelete("KickMember")]
        public IActionResult KickMember(string member, string team)
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var idUser = identity.Value;
            try
            {
                return Ok(_teamRepository.KickMember(int.Parse(idUser), member, team));
            }
            catch
            {
                return BadRequest();
            }
        }
        [Authorize(Policy = PolicyCode.TEAM_LEAD)]
        [HttpPut("UpdateTeam")]
        public IActionResult UpdateTeam(string nameTeam, TeamDTO team)
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var idUser = identity.Value;
            try
            {
                _teamRepository.UpdateTeam(int.Parse(idUser), nameTeam, team);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.TEAM_LEAD)]
        [HttpPut("ResignTeamLead")]
        public IActionResult ResignTeamLead(string member, string team)
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var idUser = identity.Value;
            try
            {
                return Ok(_teamRepository.ResignTeamLead(int.Parse(idUser), member, team));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.TEAM_LEAD)]
        [HttpGet("GetMember")]
        public IActionResult GetMember (string team)
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var idUser = identity.Value;
            try
            {
                return Ok(_teamRepository.ShowAllMember(int.Parse(idUser), team));
            }
            catch
            {
                return BadRequest();
            }
        }


    }
}
