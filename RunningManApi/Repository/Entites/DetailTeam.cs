using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("DetailTeam")]
    [Index(nameof(Id), Name = "UQ__DetailTe__3214EC0659CCAB8A", IsUnique = true)]
    public partial class DetailTeam
    {
        [Key]
        public int Id { get; set; }
        [Column("Team_Id")]
        public int TeamId { get; set; }
        [Column("Account_Id")]
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("DetailTeams")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("DetailTeams")]
        public virtual Team Team { get; set; }
    }
}
