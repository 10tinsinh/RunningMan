using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class GameDetailDTO
    {
        public int GameId { get; set; }
       
        public int GameTypeId { get; set; }
      
        public string GameRules { get; set; }
    }
}
