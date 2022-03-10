using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("AccountType")]
    [Index(nameof(Id), Name = "UQ__AccountT__3213E83E53E9403F", IsUnique = true)]
    public partial class AccountType
    {
        public AccountType()
        {
            DetailAccounts = new HashSet<DetailAccount>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nameType")]
        [StringLength(25)]
        public string NameType { get; set; }

        [InverseProperty(nameof(DetailAccount.AccountType))]
        public virtual ICollection<DetailAccount> DetailAccounts { get; set; }
    }
}
