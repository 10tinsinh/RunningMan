using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class DetailPermissionDTO
    {
        public int id { get; set; }
        public int AccountId { get; set; }
        public int PermissionId { get; set; }
    }
    public class PermissionUserDTO
    {
        public string Permission { get; set; }
    }
    public class PermissionDTO
    {
        public int id { get; set; }
        public int NamePermission { get; set; }
    }
}
