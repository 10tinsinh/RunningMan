using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Game")]
    [Index(nameof(Id), Name = "UQ__Game__3213E83EA0A8BC11", IsUnique = true)]
    public partial class Game
    {
        public Game()
        {
            DetailGames = new HashSet<DetailGame>();
            DetailRounds = new HashSet<DetailRound>();
            GamePlays = new HashSet<GamePlay>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("level")]
        public int? Level { get; set; }
        [Column("accountId")]
        public int? AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Games")]
        public virtual Account Account { get; set; }
        [InverseProperty(nameof(DetailGame.Game))]
        public virtual ICollection<DetailGame> DetailGames { get; set; }
        [InverseProperty(nameof(DetailRound.Game))]
        public virtual ICollection<DetailRound> DetailRounds { get; set; }
        [InverseProperty(nameof(GamePlay.Game))]
        public virtual ICollection<GamePlay> GamePlays { get; set; }
    }
}
