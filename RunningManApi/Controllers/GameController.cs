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
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpGet("GetGame")]
        public IActionResult GetGame(string game)
        {
            try
            {
                var result = _gameRepository.GetGame(game);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPost("CreateGame")]
        public IActionResult CreateGame(GameDTO gameDTO)
        {
            try 
            {
                var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
                var usernameToken = identity.Value;
                var result = _gameRepository.CreateGame(int.Parse(usernameToken), gameDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPut("UpdateGame")]
        public IActionResult UpdateGame (int id, GameDTO gameDTO)
        {
            try
            {
                
                _gameRepository.UpdateGame(id,gameDTO);
                return Ok(new ApiResponse 
                { 
                    Success = true,
                    Message = "Update Game Successfully"
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Update Game False"
                });
            }

        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpDelete("DeleteGame")]
        public IActionResult DeleteGame (string name)
        {
            try
            {
                _gameRepository.DeleteGame(name);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Delete Game Successfully"
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Delete Game False"
                });
            }
        }
    }
}
