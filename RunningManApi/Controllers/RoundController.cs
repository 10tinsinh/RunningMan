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
    public class RoundController : ControllerBase
    {
        private readonly IRoundRepository _roundRepository;

        public RoundController(IRoundRepository roundRepository)
        {
            _roundRepository = roundRepository;
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpGet("GetRound")]
        public IActionResult GetRound(string name)
        {
            try
            {
                var result = _roundRepository.GetRound(name);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPost("CreateRound")]
        public IActionResult CreateRound(RoundDTO roundDTO)
        {
            try
            {
                var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
                var idUser = identity.Value;
                var result = _roundRepository.CreateRound(int.Parse(idUser),roundDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPut("UpdateRound")]
        public IActionResult UpdateRound(int id, RoundDTO roundDTO)
        {
            try
            {
                _roundRepository.UpdateRound(id, roundDTO);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update Round Successfully"
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Update Round false"
                });
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpDelete("DeleteRound")]
        public IActionResult DeleteRound(string name)
        {
            try
            {
                _roundRepository.DeleteRound(name);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Delete Round Successfully"
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Delete Round false"
                });
            }
        }
    }
}
