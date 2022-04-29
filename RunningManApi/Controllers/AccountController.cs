using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunningManApi.DTO.Models;
using RunningManApi.Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using RunningManApi.Helpers;
using Microsoft.AspNet.OData;

namespace RunningManApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Authorize(Policy = PolicyCode.ADMIN)]
        [HttpGet("AdminSearchUser")]
        [EnableQuery]
        public IActionResult GetAllAccount(string usernameToken)
        {
           
            try
            {
                var result = _accountRepository.GetAllAccount(usernameToken);
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [Authorize(Policy = PolicyCode.VIEW_USER)]
        [HttpGet("InformationUserLogin")]
        public IActionResult GetAccount()
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //IEnumerable<Claim> claim = identity.Claims;

            //var usernameToken = claim.First(x => x.Type == "Username").Value;
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var usernameToken = identity.Value;
            try
            {
                var result = _accountRepository.GetInformationAccountLogin(int.Parse(usernameToken));
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [HttpPost]
        public IActionResult CreateNewAccount(AccountDTO account)
        {
            try
            {
                var result = _accountRepository.CreateAccount(account);
                return Ok(result);
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Username had exist"
                });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(Login login)
        {
            try
            {
                return Ok(_accountRepository.Login(login));
            }
            catch
            {
                return BadRequest(new ApiResponse {
                    Success = false,
                    Message = "Password or Username invalid"

                });
            }
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken(TokensDTO tokens)
        {
            try
            {
                return Ok(_accountRepository.RenewToken(tokens));
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid request"

                });
            }
        }

        [Authorize(Policy = PolicyCode.DELETE_USER)]
        [HttpDelete]
        public IActionResult DeleteAccount(int id)
        {
            
            try
            {
               _accountRepository.Delete(id);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Delete User Successfully"
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Id User don't have exist"
                });
            }
        }

        [Authorize(Policy = PolicyCode.UPDATE_USER)]
        [HttpPut]
        public IActionResult UpdateAccount( AccountDTO account)
        {
            //var identity = HttpContext.User.Identity as ClaimsIdentity;
            //IEnumerable<Claim> claim = identity.Claims;

            //var idUsernameToken = int.Parse(claim.First(x => x.Type == "Id").Value);
            var identity = User.Claims.FirstOrDefault(x => x.Type.Equals("Id"));
            var idUsernameToken = int.Parse(identity.Value);

            try
            {
                _accountRepository.Update(idUsernameToken, account);
                return Ok(new ApiResponse
                {
                    Success = true,
                    Message = "Update User Successfully"
                });
            }
            catch
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Id User don't have exist"
                });
            }
        }

    }
}
