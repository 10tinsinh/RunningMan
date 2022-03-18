using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Point")]
    [Index(nameof(Id), Name = "UQ__Point__3213E83ED31F5F71", IsUnique = true)]
    public partial class Point
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("point")]
        public int? Point1 { get; set; }
        [Column("accountId")]
        public int? AccountId { get; set; }
        [Column("teamId")]
        public int? TeamId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Points")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("Points")]
        public virtual Team Team { get; set; }
    }
}
