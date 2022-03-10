using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Team")]
    [Index(nameof(Id), Name = "UQ__Team__3213E83E71144CA7", IsUnique = true)]
    public partial class Team
    {
        public Team()
        {
            DetailRounds = new HashSet<DetailRound>();
            DetailTeams = new HashSet<DetailTeam>();
            GamePlays = new HashSet<GamePlay>();
            Points = new HashSet<Point>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("rankTeam")]
        public int? RankTeam { get; set; }

        [InverseProperty(nameof(DetailRound.Team))]
        public virtual ICollection<DetailRound> DetailRounds { get; set; }
        [InverseProperty(nameof(DetailTeam.Team))]
        public virtual ICollection<DetailTeam> DetailTeams { get; set; }
        [InverseProperty(nameof(GamePlay.Team))]
        public virtual ICollection<GamePlay> GamePlays { get; set; }
        [InverseProperty(nameof(Point.Team))]
        public virtual ICollection<Point> Points { get; set; }
    }
}
