using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Index(nameof(RoleCode), Name = "UQ__Roles__1E835107D91F67A4", IsUnique = true)]
    [Index(nameof(Id), Name = "UQ__Roles__3214EC06EBC41E55", IsUnique = true)]
    public partial class Role
    {
        public Role()
        {
            RolesDetails = new HashSet<RolesDetail>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [Column("Role_Code")]
        [StringLength(25)]
        public string RoleCode { get; set; }
        [Required]
        [Column("Role_Name")]
        [StringLength(50)]
        public string RoleName { get; set; }

        [InverseProperty(nameof(RolesDetail.Roles))]
        public virtual ICollection<RolesDetail> RolesDetails { get; set; }
    }
}
