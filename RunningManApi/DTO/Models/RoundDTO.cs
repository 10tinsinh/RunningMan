using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class RoundDTO
    {
        public string Name { get; set; }
       
        public int LocationId { get; set; }
       
        public int AccountId { get; set; }
      
        public int BonusPoints { get; set; }
        public int? Level { get; set; }
    }
    public class RoundIdDTO:RoundDTO
    {
        public int Id { get; set; }
    }
}
