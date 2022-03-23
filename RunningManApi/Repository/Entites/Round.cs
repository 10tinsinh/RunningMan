using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Round")]
    [Index(nameof(Id), Name = "UQ__Round__3214EC069C4266E6", IsUnique = true)]
    public partial class Round
    {
        public Round()
        {
            RoundDetails = new HashSet<RoundDetail>();
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
        [InverseProperty(nameof(RoundDetail.Round))]
        public virtual ICollection<RoundDetail> RoundDetails { get; set; }
    }
}
