using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Location")]
    [Index(nameof(Id), Name = "UQ__Location__3214EC06AD0DC573", IsUnique = true)]
    public partial class Location
    {
        public Location()
        {
            Rounds = new HashSet<Round>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [InverseProperty(nameof(Round.Location))]
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
