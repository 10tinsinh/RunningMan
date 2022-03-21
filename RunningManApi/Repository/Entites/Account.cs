using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Account")]
    [Index(nameof(Id), Name = "UQ__Account__3214EC064D6866DE", IsUnique = true)]
    [Index(nameof(UserName), Name = "UQ__Account__C9F2845620022A21", IsUnique = true)]
    public partial class Account
    {
        public Account()
        {
            DetailTeams = new HashSet<DetailTeam>();
            Games = new HashSet<Game>();
            PermissionDetails = new HashSet<PermissionDetail>();
            Points = new HashSet<Point>();
            RolesDetails = new HashSet<RolesDetail>();
            Rounds = new HashSet<Round>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(250)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Email { get; set; }
        [Column("Account_Status")]
        public bool AccountStatus { get; set; }

        [InverseProperty(nameof(DetailTeam.Account))]
        public virtual ICollection<DetailTeam> DetailTeams { get; set; }
        [InverseProperty(nameof(Game.Account))]
        public virtual ICollection<Game> Games { get; set; }
        [InverseProperty(nameof(PermissionDetail.Account))]
        public virtual ICollection<PermissionDetail> PermissionDetails { get; set; }
        [InverseProperty(nameof(Point.Account))]
        public virtual ICollection<Point> Points { get; set; }
        [InverseProperty(nameof(RolesDetail.Account))]
        public virtual ICollection<RolesDetail> RolesDetails { get; set; }
        [InverseProperty(nameof(Round.Account))]
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
