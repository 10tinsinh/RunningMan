using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class AccountDTO
    {

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool AccountStatus { get; set; }
    }
    public class AccountIdDTO : AccountDTO
    {
        public int Id { get; set; }
    }
    public class Login
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
    public class MemberDTO
    {
        public string Username { get; set; }
        public string Name { get; set; }
    }
   
        
}
