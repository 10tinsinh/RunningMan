using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Round")]
    [Index(nameof(Id), Name = "UQ__Round__3213E83E891A8B1C", IsUnique = true)]
    public partial class Round
    {
        public Round()
        {
            DetailRounds = new HashSet<DetailRound>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("locationId")]
        public int LocationId { get; set; }
        [Column("accountId")]
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Rounds")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(LocationId))]
        [InverseProperty(nameof(Localtion.Rounds))]
        public virtual Localtion Location { get; set; }
        [InverseProperty(nameof(DetailRound.Round))]
        public virtual ICollection<DetailRound> DetailRounds { get; set; }
    }
}
