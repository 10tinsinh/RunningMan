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
    }
}
