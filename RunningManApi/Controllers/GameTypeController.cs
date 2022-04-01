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
    public class GameTypeController : ControllerBase
    {
        private readonly IGameTypeRepository _gameTypeRepository;

        public GameTypeController(IGameTypeRepository gameTypeRepository)
        {
            _gameTypeRepository = gameTypeRepository;
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpGet("GetGameType")]
        public IActionResult GetGameType(string name)
        {
            try
            {
                var result = _gameTypeRepository.GetGameType(name);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPost("CreateGameType")]
        public IActionResult CreateGameType(GameTypeDTO gameTypeDTO)
        {
            try
            {
                var result = _gameTypeRepository.CreateGameType(gameTypeDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPut("UpdateGameType")]
        public IActionResult UpdateGameType (int id, GameTypeDTO gameTypeDTO)
        {
            try
            {
                _gameTypeRepository.UpdateGameType(id, gameTypeDTO);
                return Ok(new ApiResponse { 
                
                    Success = true,
                    Message = "Update GameType Successfully" 
                });
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpDelete("DeleteGameType")]
        public IActionResult DeleteGameType (int id)
        {
            try
            {
                _gameTypeRepository.DeleteGameType(id);
                return Ok(new ApiResponse
                {

                    Success = true,
                    Message = "Delete GameType Successfully"
                });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
