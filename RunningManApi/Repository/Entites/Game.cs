using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Game")]
    [Index(nameof(Id), Name = "UQ__Game__3214EC060045BCBC", IsUnique = true)]
    public partial class Game
    {
        public Game()
        {
            DetailRounds = new HashSet<DetailRound>();
            GameDetails = new HashSet<GameDetail>();
            GamePlays = new HashSet<GamePlay>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int? Level { get; set; }
        [Column("Account_Id")]
        public int? AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Games")]
        public virtual Account Account { get; set; }
        [InverseProperty(nameof(DetailRound.Game))]
        public virtual ICollection<DetailRound> DetailRounds { get; set; }
        [InverseProperty(nameof(GameDetail.Game))]
        public virtual ICollection<GameDetail> GameDetails { get; set; }
        [InverseProperty(nameof(GamePlay.Game))]
        public virtual ICollection<GamePlay> GamePlays { get; set; }
    }
}
