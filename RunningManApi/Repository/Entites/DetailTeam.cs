using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("DetailTeam")]
    [Index(nameof(Id), Name = "UQ__DetailTe__3213E83EF4337C7A", IsUnique = true)]
    public partial class DetailTeam
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("teamId")]
        public int TeamId { get; set; }
        [Column("accountId")]
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("DetailTeams")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("DetailTeams")]
        public virtual Team Team { get; set; }
    }
}
