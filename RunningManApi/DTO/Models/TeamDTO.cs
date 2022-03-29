using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class TeamDTO
    {
        
        
        public string Name { get; set; }
        
    }
    public class TeamIdDTO:TeamDTO
    {
        public int Id { get; set; }
        public int? Rank { get; set; }
        public bool? TeamLead { get; set; }
    }
}
