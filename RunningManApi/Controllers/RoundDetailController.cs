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
    public class RoundDetailController : ControllerBase
    {
        private readonly IRoundDetailRepository _roundDetailRepository;

        public RoundDetailController(IRoundDetailRepository roundDetailRepository)
        {
            _roundDetailRepository = roundDetailRepository;
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpGet("GetRoundDetail")]
        public IActionResult GetRound(string name)
        {
            try
            {
                var result = _roundDetailRepository.GetRoundDetail(name);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPost("CreateRound")]
        public IActionResult CreateRound(int idRound)
        {
            try
            {
                _roundDetailRepository.CreateRoundDetail( idRound);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPut("UpdateRound")]
        public IActionResult UpdateRound(int id, DetailRoundDTO detailRoundDTO)
        {
            try
            {
                _roundDetailRepository.UpdateRoundDetail(id, detailRoundDTO);
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
        public IActionResult DeleteRound(int id)
        {
            try
            {
                _roundDetailRepository.DeleteRoundDetail(id);
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
