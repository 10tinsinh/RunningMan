using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Team")]
    [Index(nameof(Id), Name = "UQ__Team__3214EC0698222CB9", IsUnique = true)]
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
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int? Rank { get; set; }

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
