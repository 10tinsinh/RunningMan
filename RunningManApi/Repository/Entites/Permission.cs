using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Permission")]
    [Index(nameof(Id), Name = "UQ__Permissi__3213E83E7E7F5048", IsUnique = true)]
    public partial class Permission
    {
        public Permission()
        {
            DetailPermissions = new HashSet<DetailPermission>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("namePermission")]
        [StringLength(25)]
        public string NamePermission { get; set; }

        [InverseProperty(nameof(DetailPermission.Permission))]
        public virtual ICollection<DetailPermission> DetailPermissions { get; set; }
    }
}
