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
    public class GamePlayController : ControllerBase
    {
        private readonly IGamePlayRepository _gamePlayRepository;

        public GamePlayController(IGamePlayRepository gamePlayRepository)
        {
            _gamePlayRepository = gamePlayRepository;
        }

        [Authorize(Policy = PolicyCode.TEAM_MEMBER)]
        [HttpPost("CreateGamePlay")]
        public IActionResult CreateGamePlay(int teamId)
        {
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var usernameToken = identity.Value;
            try
            {
                var result = _gamePlayRepository.CreateGamePlay(teamId, int.Parse(usernameToken));
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
