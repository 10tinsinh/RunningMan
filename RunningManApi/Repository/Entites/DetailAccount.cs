using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("DetailAccount")]
    [Index(nameof(Id), Name = "UQ__DetailAc__3213E83EE4047EBA", IsUnique = true)]
    public partial class DetailAccount
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("accountId")]
        public int AccountId { get; set; }
        [Column("accountTypeId")]
        public int AccountTypeId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("DetailAccounts")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(AccountTypeId))]
        [InverseProperty("DetailAccounts")]
        public virtual AccountType AccountType { get; set; }
    }
}
