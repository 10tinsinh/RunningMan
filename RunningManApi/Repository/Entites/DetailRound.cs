using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("DetailRound")]
    [Index(nameof(Id), Name = "UQ__DetailRo__3214EC06F78A597E", IsUnique = true)]
    public partial class DetailRound
    {
        [Key]
        public int Id { get; set; }
        [Column("Round_Id")]
        public int? RoundId { get; set; }
        [Column("Game_Id")]
        public int? GameId { get; set; }
        [Column("Team_Id")]
        public int? TeamId { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty("DetailRounds")]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(RoundId))]
        [InverseProperty("DetailRounds")]
        public virtual Round Round { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("DetailRounds")]
        public virtual Team Team { get; set; }
    }
}
