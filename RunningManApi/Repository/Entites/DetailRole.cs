using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Index(nameof(Id), Name = "UQ__DetailRo__3213E83E92884C67", IsUnique = true)]
    public partial class DetailRole
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("accountId")]
        public int AccountId { get; set; }
        public int RolesId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("DetailRoles")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(RolesId))]
        [InverseProperty(nameof(Role.DetailRoles))]
        public virtual Role Roles { get; set; }
    }
}
