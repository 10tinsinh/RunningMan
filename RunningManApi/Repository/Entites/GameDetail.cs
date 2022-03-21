using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("GameDetail")]
    public partial class GameDetail
    {
        [Key]
        [Column("Game_Id")]
        public int GameId { get; set; }
        [Key]
        [Column("Game_Type_Id")]
        public int GameTypeId { get; set; }
        [Column("Game_Rules")]
        [StringLength(250)]
        public string GameRules { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty("GameDetails")]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(GameTypeId))]
        [InverseProperty("GameDetails")]
        public virtual GameType GameType { get; set; }
    }
}
