using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class UserRefreshTokenDTO
    {
        
        public int Id { get; set; }
        
        public string UserName { get; set; }
      
        public string RefreshToken { get; set; }
        public bool? IsActive { get; set; }
    }
}
