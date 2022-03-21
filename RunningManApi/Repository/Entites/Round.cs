using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Round")]
    [Index(nameof(Id), Name = "UQ__Round__3214EC06EBE5A24E", IsUnique = true)]
    public partial class Round
    {
        public Round()
        {
            DetailRounds = new HashSet<DetailRound>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("Location_Id")]
        public int LocationId { get; set; }
        [Column("Account_Id")]
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
