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
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpGet("GetPermission")]
        public IActionResult GetPermission(string permissionName)
        {
            try
            {
                var result = _permissionRepository.GetPermission(permissionName);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPost("CreatePermission")]
        public IActionResult CreatePermission(PermissionDTO permissionDTO)
        {
            try
            {
                var result = _permissionRepository.CreatePermission(permissionDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPut("UpdatePermission")]
        public IActionResult UpdatePermission(int id, PermissionDTO permissionDTO)
        {
            try
            {
                _permissionRepository.UpdatePermission(id, permissionDTO);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update Permission Successfully"
                });
            }
            catch
            {
                return BadRequest(new ApiResponse 
                {
                    Success = false,
                    Message = "Update Permission False"
                });
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpDelete("DeletePermission")]
        public IActionResult DeletePermission(string permissionCode)
        {
            try
            {
                _permissionRepository.DeletePermission(permissionCode);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Delete Permission Successfully"
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Delete Permission False"
                });
            }
        }
    }
}
