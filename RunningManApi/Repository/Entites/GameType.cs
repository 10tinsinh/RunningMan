using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("GameType")]
    [Index(nameof(Id), Name = "UQ__GameType__3214EC0640C12F22", IsUnique = true)]
    public partial class GameType
    {
        public GameType()
        {
            GameDetails = new HashSet<GameDetail>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(GameDetail.GameType))]
        public virtual ICollection<GameDetail> GameDetails { get; set; }
    }
}
