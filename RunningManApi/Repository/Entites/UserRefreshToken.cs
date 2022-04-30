using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Index(nameof(Id), Name = "UQ__UserRefr__3214EC0623997AE0", IsUnique = true)]
    [Index(nameof(RefreshToken), Name = "UQ__UserRefr__DEA298DA4F9097DB", IsUnique = true)]
    public partial class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(500)]
        public string RefreshToken { get; set; }
        public bool? IsActive { get; set; }
    }
}
