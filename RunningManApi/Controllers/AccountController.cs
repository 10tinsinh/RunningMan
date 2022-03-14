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
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet("Search")]
        public IActionResult GetAccount(string search)
        {
            try
            {
                var result = _accountRepository.GetAllAccount(search);
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
                return BadRequest();
            }
        }
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
        [HttpPut]
        public IActionResult UpdateAccount(int id, AccountDTO account)
        {

            try
            {
                _accountRepository.Update(id,account);
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
