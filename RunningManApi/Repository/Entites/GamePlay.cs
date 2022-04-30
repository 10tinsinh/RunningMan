using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("GamePlay")]
    [Index(nameof(RoundId), nameof(TeamId), Name = "UC_GamePlay", IsUnique = true)]
    [Index(nameof(Id), Name = "UQ__GamePlay__3214EC0619B44DCF", IsUnique = true)]
    public partial class GamePlay
    {
        [Key]
        public int Id { get; set; }
        public int? Rank { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }
        [Column("Round_Id")]
        public int? RoundId { get; set; }
        [Column("Team_Id")]
        public int? TeamId { get; set; }

        [ForeignKey(nameof(RoundId))]
        [InverseProperty("GamePlays")]
        public virtual Round Round { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("GamePlays")]
        public virtual Team Team { get; set; }
    }
}
