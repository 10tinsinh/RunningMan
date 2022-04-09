using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Team")]
    [Index(nameof(Id), Name = "UQ__Team__3214EC061AEB2737", IsUnique = true)]
    public partial class Team
    {
        public Team()
        {
            GamePlays = new HashSet<GamePlay>();
            Points = new HashSet<Point>();
            RoundDetails = new HashSet<RoundDetail>();
            TeamDetails = new HashSet<TeamDetail>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int? Rank { get; set; }

        [InverseProperty(nameof(GamePlay.Team))]
        public virtual ICollection<GamePlay> GamePlays { get; set; }
        [InverseProperty(nameof(Point.Team))]
        public virtual ICollection<Point> Points { get; set; }
        [InverseProperty(nameof(RoundDetail.Team))]
        public virtual ICollection<RoundDetail> RoundDetails { get; set; }
        [InverseProperty(nameof(TeamDetail.Team))]
        public virtual ICollection<TeamDetail> TeamDetails { get; set; }
    }
}
