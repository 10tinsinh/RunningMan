using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("DetailRound")]
    [Index(nameof(Id), Name = "UQ__DetailRo__3213E83E2EEEAE40", IsUnique = true)]
    public partial class DetailRound
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("roundId")]
        public int? RoundId { get; set; }
        [Column("gameId")]
        public int? GameId { get; set; }
        [Column("teamId")]
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
