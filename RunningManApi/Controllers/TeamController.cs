using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [Authorize]
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
    }
}
