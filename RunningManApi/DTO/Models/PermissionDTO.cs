using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class PermissionDTO
    {
        public string PermissionCode { get; set; }
       
        public string PermissionName { get; set; }
    }

    public class PermissionIdDTO:PermissionDTO
    {
        public int Id { get; set; }
    }
}
