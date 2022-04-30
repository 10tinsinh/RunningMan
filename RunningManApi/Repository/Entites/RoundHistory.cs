using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("RoundHistory")]
    [Index(nameof(RoundId), nameof(AccountId), Name = "UC_RoundHistory", IsUnique = true)]
    [Index(nameof(Id), Name = "UQ__RoundHis__3214EC06008BD366", IsUnique = true)]
    public partial class RoundHistory
    {
        [Key]
        public int Id { get; set; }
        [Column("Round_Id")]
        public int RoundId { get; set; }
        [Column("Account_Id")]
        public int AccountId { get; set; }
        public int? Times { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("RoundHistories")]
        public virtual Account Account { get; set; }
    }
}
