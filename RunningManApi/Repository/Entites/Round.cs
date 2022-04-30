using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("Round")]
    [Index(nameof(Id), Name = "UQ__Round__3214EC061ACCCB09", IsUnique = true)]
    public partial class Round
    {
        public Round()
        {
            GamePlays = new HashSet<GamePlay>();
            RoundDetails = new HashSet<RoundDetail>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [Column("Location_Id")]
        public int LocationId { get; set; }
        [Column("Account_Id")]
        public int AccountId { get; set; }
        [Column("Bonus_Points")]
        public int BonusPoints { get; set; }
        public int? Level { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("Rounds")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(LocationId))]
        [InverseProperty("Rounds")]
        public virtual Location Location { get; set; }
        [InverseProperty(nameof(GamePlay.Round))]
        public virtual ICollection<GamePlay> GamePlays { get; set; }
        [InverseProperty(nameof(RoundDetail.Round))]
        public virtual ICollection<RoundDetail> RoundDetails { get; set; }
    }
}
