using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Index(nameof(Id), Name = "UQ__Roles__3213E83EEE1D3C37", IsUnique = true)]
    public partial class Role
    {
        public Role()
        {
            DetailRoles = new HashSet<DetailRole>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("nameRoles")]
        [StringLength(25)]
        public string NameRoles { get; set; }

        [InverseProperty(nameof(DetailRole.Roles))]
        public virtual ICollection<DetailRole> DetailRoles { get; set; }
    }
}
