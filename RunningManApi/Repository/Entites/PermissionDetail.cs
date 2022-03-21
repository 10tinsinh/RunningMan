using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("PermissionDetail")]
    [Index(nameof(Id), Name = "UQ__Permissi__3214EC06EB3CB07C", IsUnique = true)]
    public partial class PermissionDetail
    {
        [Key]
        public int Id { get; set; }
        [Column("Account_Id")]
        public int AccountId { get; set; }
        [Column("Permission_Id")]
        public int PermissionId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("PermissionDetails")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(PermissionId))]
        [InverseProperty("PermissionDetails")]
        public virtual Permission Permission { get; set; }
    }
}
