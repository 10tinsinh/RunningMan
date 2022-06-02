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

        [Authorize(Policy = PolicyCode.TEAM_MEMBER)]
        
        [HttpGet("GetGame")]
        public IActionResult GetGame(int idGamePlay, int page = 1)
        {
            try
            {
                var result = _gamePlayRepository.GetGamePlay(idGamePlay, page);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.TEAM_MEMBER)]
        
        [HttpPost("AnswerQuestion")]
        public IActionResult AnswerQuestion(int gameId, string answer)
        {
            try
            {

                var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
                var usernameToken = identity.Value;
                var result = _gamePlayRepository.AnswerQuestion(gameId, int.Parse(usernameToken), answer);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
