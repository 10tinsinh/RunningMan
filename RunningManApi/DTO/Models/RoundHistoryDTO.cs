using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class RoundHistoryDTO
    {
        public int Id { get; set; }
        public int RoundId { get; set; }
        public int AccountId { get; set; }
        public int? Times { get; set; }
    }
}
