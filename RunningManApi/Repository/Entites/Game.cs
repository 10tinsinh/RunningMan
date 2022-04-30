using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Game")]
    [Index(nameof(Id), Name = "UQ__Game__3214EC0647818123", IsUnique = true)]
    public partial class Game
    {
        public Game()
        {
            GameHistories = new HashSet<GameHistory>();
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
        [StringLength(250)]
        public string Question { get; set; }
        [StringLength(250)]
        public string Answer { get; set; }
        [Column("Hint_1")]
        [StringLength(250)]
        public string Hint1 { get; set; }
        [Column("Hint_2")]
        [StringLength(250)]
        public string Hint2 { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Games")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(GameTypeId))]
        [InverseProperty("Games")]
        public virtual GameType GameType { get; set; }
        [InverseProperty(nameof(GameHistory.Game))]
        public virtual ICollection<GameHistory> GameHistories { get; set; }
        [InverseProperty(nameof(RoundDetail.Game))]
        public virtual ICollection<RoundDetail> RoundDetails { get; set; }
    }
}
