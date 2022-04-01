using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class GameDTO
    {
        
    
        public string Name { get; set; }
        public int? Level { get; set; }
      
        public int? AccountId { get; set; }
    }
    public class GameIdDTO:GameDTO
    {
        public int Id { get; set; }
    }
}
