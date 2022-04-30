using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class DetailRoundDTO
    {
        public int Id { get; set; }
       
        public int? RoundId { get; set; }
        
        public int? GameId { get; set; }
    }


}
