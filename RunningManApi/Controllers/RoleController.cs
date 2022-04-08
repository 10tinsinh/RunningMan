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
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpGet("GetRole")]
        public IActionResult GetRole(string roleName)
        {
            try
            {
                var result = _roleRepository.GetRole(roleName);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPost("CreateRole")]
        public IActionResult CreateRole (RoleDTO roleDTO)
        {
            try
            {
                var result = _roleRepository.CreateRole(roleDTO);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpPut("UpdateRole")]
        public IActionResult UpdateRole (int id, RoleDTO roleDTO)
        {
            try
            {
                _roleRepository.UpdateRole(id, roleDTO);
                return Ok(new ApiResponse { 
                    Success = true,
                    Message = "Update Role Successfully"
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Role Id invalid"
                });
            }
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpDelete("DeleteRole")]
        public IActionResult DeleteRole (string roleCode)
        {
            try
            {
                _roleRepository.DeleteRole(roleCode);
                return Ok();
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Role Code invalid"
                });
            }
        }
    }
}
