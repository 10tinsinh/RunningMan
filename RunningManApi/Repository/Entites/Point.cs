using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Point")]
    [Index(nameof(Id), Name = "UQ__Point__3214EC0608A1BA5A", IsUnique = true)]
    public partial class Point
    {
        [Key]
        public int Id { get; set; }
        [Column("Point")]
        public int? Point1 { get; set; }
        [Column("Account_Id")]
        public int? AccountId { get; set; }
        [Column("Team_Id")]
        public int? TeamId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Points")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(TeamId))]
        [InverseProperty("Points")]
        public virtual Team Team { get; set; }
    }
}
