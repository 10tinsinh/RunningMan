using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Permission")]
    [Index(nameof(PermissionCode), Name = "UQ__Permissi__0C93AC102FB2A33D", IsUnique = true)]
    [Index(nameof(Id), Name = "UQ__Permissi__3214EC067C45F18A", IsUnique = true)]
    public partial class Permission
    {
        public Permission()
        {
            PermissionDetails = new HashSet<PermissionDetail>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column("Permission_Code")]
        [StringLength(25)]
        public string PermissionCode { get; set; }
        [Required]
        [Column("Permission_Name")]
        [StringLength(50)]
        public string PermissionName { get; set; }

        [InverseProperty(nameof(PermissionDetail.Permission))]
        public virtual ICollection<PermissionDetail> PermissionDetails { get; set; }
    }
}
