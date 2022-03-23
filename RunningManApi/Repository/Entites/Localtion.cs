using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Localtion")]
    [Index(nameof(Id), Name = "UQ__Localtio__3214EC0604015CC0", IsUnique = true)]
    public partial class Localtion
    {
        public Localtion()
        {
            Rounds = new HashSet<Round>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Adress { get; set; }

        [InverseProperty(nameof(Round.Location))]
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
