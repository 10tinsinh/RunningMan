using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Account")]
    [Index(nameof(Id), Name = "UQ__Account__3213E83E5008195F", IsUnique = true)]
    [Index(nameof(UserName), Name = "UQ__Account__66DCF95CBEF18AAB", IsUnique = true)]
    public partial class Account
    {
        public Account()
        {
            DetailAccounts = new HashSet<DetailAccount>();
            DetailTeams = new HashSet<DetailTeam>();
            Games = new HashSet<Game>();
            Points = new HashSet<Point>();
            Rounds = new HashSet<Round>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("userName")]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [Column("passWord")]
        [StringLength(250)]
        public string PassWord { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Column("email")]
        [StringLength(250)]
        public string Email { get; set; }
        [Column("accountStatus")]
        public bool AccountStatus { get; set; }

        [InverseProperty(nameof(DetailAccount.Account))]
        public virtual ICollection<DetailAccount> DetailAccounts { get; set; }
        [InverseProperty(nameof(DetailTeam.Account))]
        public virtual ICollection<DetailTeam> DetailTeams { get; set; }
        [InverseProperty(nameof(Game.Account))]
        public virtual ICollection<Game> Games { get; set; }
        [InverseProperty(nameof(Point.Account))]
        public virtual ICollection<Point> Points { get; set; }
        [InverseProperty(nameof(Round.Account))]
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
