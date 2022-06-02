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
        public int GameTypeId { get; set; }
        public string GameRules { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; } 
        public string Hint1 { get; set; }
        public string Hint2 { get; set; }
    }
    public class GameIdDTO:GameDTO
    {
        public int Id { get; set; }
    }
    public class GameViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GameRules { get; set; }
        public string Question { get; set; }
        public string Hint1 { get; set; }
        public string Hint2 { get; set; }
        public string Answer { get; set; }
    }
}
