using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class TeamDetailDTO
    {
        public int Id { get; set; }
        
        public int TeamId { get; set; }
        
        public int AccountId { get; set; }

        public bool? TeamLead { get; set; }
    }
}
