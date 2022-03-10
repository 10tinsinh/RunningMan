using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("DetailGame")]
    public partial class DetailGame
    {
        [Key]
        [Column("gameId")]
        public int GameId { get; set; }
        [Key]
        [Column("gameTypeId")]
        public int GameTypeId { get; set; }
        [Column("ruleGame")]
        [StringLength(250)]
        public string RuleGame { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty("DetailGames")]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(GameTypeId))]
        [InverseProperty("DetailGames")]
        public virtual GameType GameType { get; set; }
    }
}
