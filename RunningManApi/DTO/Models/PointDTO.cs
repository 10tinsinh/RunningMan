using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class PointDTO
    {
        public int Id { get; set; }
        
        public int? Point1 { get; set; }
        
        public int? AccountId { get; set; }
       
        public int? TeamId { get; set; }
    }
}
