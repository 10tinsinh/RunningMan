using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("GameType")]
    [Index(nameof(Id), Name = "UQ__GameType__3213E83E466360FA", IsUnique = true)]
    public partial class GameType
    {
        public GameType()
        {
            DetailGames = new HashSet<DetailGame>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(DetailGame.GameType))]
        public virtual ICollection<DetailGame> DetailGames { get; set; }
    }
}
