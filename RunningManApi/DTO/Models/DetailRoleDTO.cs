﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class DetailRoleDTO
    {
       
        public int AccountId { get; set; }
        public int RolesId { get; set; }
    }
    public class DetailRoleIdDTO:DetailRoleDTO
    {
        public int Id { get; set; }
    }
}
