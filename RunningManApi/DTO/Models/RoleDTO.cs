using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class RoleDTO
    {
        public string RoleCode { get; set; }
      
        public string RoleName { get; set; }
    }

    public class RoleIdDTO:RoleDTO
    {
        public int Id { get; set; }
    }
}
