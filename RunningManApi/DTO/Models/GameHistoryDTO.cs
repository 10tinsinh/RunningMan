using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class GameHistoryDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int AccountId { get; set; }
        public int? Times { get; set; }
    }
}
