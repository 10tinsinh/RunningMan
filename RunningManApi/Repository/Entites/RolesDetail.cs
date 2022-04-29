using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("RolesDetail")]
    [Index(nameof(AccountId), nameof(RolesId), Name = "UC_RolesDetail", IsUnique = true)]
    [Index(nameof(Id), Name = "UQ__RolesDet__3214EC0617475F9F", IsUnique = true)]
    public partial class RolesDetail
    {
        [Key]
        public int Id { get; set; }
        [Column("Account_Id")]
        public int AccountId { get; set; }
        [Column("Roles_Id")]
        public int RolesId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("RolesDetails")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(RolesId))]
        [InverseProperty(nameof(Role.RolesDetails))]
        public virtual Role Roles { get; set; }
    }
}
