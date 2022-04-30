using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("GameHistory")]
    [Index(nameof(GameId), nameof(AccountId), Name = "UC_GameHistory", IsUnique = true)]
    [Index(nameof(Id), Name = "UQ__GameHist__3214EC062631A27C", IsUnique = true)]
    public partial class GameHistory
    {
        [Key]
        public int Id { get; set; }
        [Column("Game_Id")]
        public int GameId { get; set; }
        [Column("Account_Id")]
        public int AccountId { get; set; }
        public int? Times { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("GameHistories")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(GameId))]
        [InverseProperty("GameHistories")]
        public virtual Game Game { get; set; }
    }
}
