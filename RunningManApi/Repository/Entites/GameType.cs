using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RunningManApi.Repository.Entites
{
    [Table("GameType")]
    [Index(nameof(Id), Name = "UQ__GameType__3214EC06D8D9C154", IsUnique = true)]
    public partial class GameType
    {
        public GameType()
        {
            Games = new HashSet<Game>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        [InverseProperty(nameof(Game.GameType))]
        public virtual ICollection<Game> Games { get; set; }
    }
}
