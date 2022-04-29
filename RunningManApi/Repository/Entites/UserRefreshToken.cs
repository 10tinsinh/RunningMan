using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Index(nameof(Id), Name = "UQ__UserRefr__3214EC0610C36B8F", IsUnique = true)]
    [Index(nameof(UserName), Name = "UQ__UserRefr__C9F284566EE9AC8D", IsUnique = true)]
    public partial class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(500)]
        public string RefreshToken { get; set; }
        public bool? IsActive { get; set; }
    }
}
