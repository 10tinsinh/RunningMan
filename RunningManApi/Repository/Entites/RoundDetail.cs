using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("RoundDetail")]
    [Index(nameof(RoundId), nameof(GameId), Name = "UC_RoundDetail", IsUnique = true)]
    [Index(nameof(Id), Name = "UQ__RoundDet__3214EC06A464621C", IsUnique = true)]
    public partial class RoundDetail
    {
        [Key]
        public int Id { get; set; }
        [Column("Round_Id")]
        public int? RoundId { get; set; }
        [Column("Game_Id")]
        public int? GameId { get; set; }

        [ForeignKey(nameof(GameId))]
        [InverseProperty("RoundDetails")]
        public virtual Game Game { get; set; }
        [ForeignKey(nameof(RoundId))]
        [InverseProperty("RoundDetails")]
        public virtual Round Round { get; set; }
    }
}
