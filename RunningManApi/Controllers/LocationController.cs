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
    public class LocationController : ControllerBase
    {
        private readonly ILocationRepository _locationRepository;

        public LocationController(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpGet("GetLocation")]
        public IActionResult GetLocation(string address)
        {
            try
            {
                var result = _locationRepository.GetLocation(address);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPost("CreateLocation")]
        public IActionResult CreateLocation(LocationDTO locationDTO)
        {
            try
            {
                var result = _locationRepository.CreateLocation(locationDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPut("UpdateLocation")]
        public IActionResult UpdateLocation(int id, LocationDTO locationDTO)
        {
            try
            {
                _locationRepository.UpdateLocation(id, locationDTO);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update Location Successfully"

                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Update Location false"

                });
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpDelete("DeleteLocation")]
        public IActionResult DeleteLocation(string address)
        {
            try
            {
                _locationRepository.DeleteLocation(address);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Delete Location Successfully"

                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Delete Location false"

                });
            }
        }
    }
}
