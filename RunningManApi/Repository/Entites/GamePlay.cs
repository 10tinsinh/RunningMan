using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("GamePlay")]
    [Index(nameof(Id), Name = "UQ__GamePlay__3213E83E3876FE87", IsUnique = true)]
    public partial class GamePlay
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("rankGame")]
        public int? RankGame { get; set; }
        [Column("giftPoint")]
        public int? GiftPoint { get; set; }
        [Column("date", TypeName = "date")]
        public DateTime? Date { get; set; }
        [Column("gameId")]
        public int? GameId { get; set; }
        [Column("teamId")]
        public int? TeamId { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty("GamePlays")]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("GamePlays")]
        public virtual Team Team { get; set; }
    }
}
