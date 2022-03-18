using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("DetailPermission")]
    [Index(nameof(Id), Name = "UQ__DetailPe__3213E83E7547058C", IsUnique = true)]
    public partial class DetailPermission
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("accountId")]
        public int AccountId { get; set; }
        public int PermissionId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("DetailPermissions")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(PermissionId))]
        [InverseProperty("DetailPermissions")]
        public virtual Permission Permission { get; set; }
    }
}
