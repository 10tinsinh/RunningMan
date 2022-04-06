using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Game")]
    [Index(nameof(Id), Name = "UQ__Game__3214EC0667D3E541", IsUnique = true)]
    public partial class Game
    {
        public Game()
        {
            GamePlays = new HashSet<GamePlay>();
            RoundDetails = new HashSet<RoundDetail>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int? Level { get; set; }
        [Column("Account_Id")]
        public int? AccountId { get; set; }
        [Column("Game_Type_Id")]
        public int GameTypeId { get; set; }
        [Column("Game_Rules")]
        [StringLength(250)]
        public string GameRules { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Games")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(GameTypeId))]
        [InverseProperty("Games")]
        public virtual GameType GameType { get; set; }
        [InverseProperty(nameof(GamePlay.Game))]
        public virtual ICollection<GamePlay> GamePlays { get; set; }
        [InverseProperty(nameof(RoundDetail.Game))]
        public virtual ICollection<RoundDetail> RoundDetails { get; set; }
    }
}
