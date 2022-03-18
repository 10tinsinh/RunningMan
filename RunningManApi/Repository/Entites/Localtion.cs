using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Localtion")]
    [Index(nameof(Id), Name = "UQ__Localtio__3213E83E4EF34D2B", IsUnique = true)]
    public partial class Localtion
    {
        public Localtion()
        {
            Rounds = new HashSet<Round>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("adress")]
        [StringLength(250)]
        public string Adress { get; set; }

        [InverseProperty(nameof(Round.Location))]
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
