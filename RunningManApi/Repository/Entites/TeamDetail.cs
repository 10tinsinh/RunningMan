using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("TeamDetail")]
    [Index(nameof(Id), Name = "UQ__TeamDeta__3214EC0664D91536", IsUnique = true)]
    public partial class TeamDetail
    {
        [Key]
        public int Id { get; set; }
        [Column("Team_Id")]
        public int TeamId { get; set; }
        [Column("Account_Id")]
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("TeamDetails")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("TeamDetails")]
        public virtual Team Team { get; set; }
    }
}
