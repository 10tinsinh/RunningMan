using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("GamePlay")]
    [Index(nameof(Id), Name = "UQ__GamePlay__3214EC06FBD56155", IsUnique = true)]
    public partial class GamePlay
    {
        [Key]
        public int Id { get; set; }
        public int? Rank { get; set; }
        [Column("Bonus_Points")]
        public int? BonusPoints { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        [Column("Game_Id")]
        public int? GameId { get; set; }
        [Column("Team_Id")]
        public int? TeamId { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty("GamePlays")]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("GamePlays")]
        public virtual Team Team { get; set; }
    }
}
