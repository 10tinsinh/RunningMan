using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class GamePlayDTO
    {

        public int? Rank { get; set; }
       
        public DateTime? Date { get; set; }
        
        public int? RoundId { get; set; }

        public int? TeamId { get; set; }

    }

    public class GamePlayIdDTO : GamePlayDTO
    {
        public int Id { get; set; }
    }
}
