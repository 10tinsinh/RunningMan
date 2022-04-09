using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.DTO.Models
{
    public class LocationDTO
    {
        public string Adress { get; set; }
    }
    public class LocationIdDTO:LocationDTO
    {
        public int Id { get; set; }
    }
}
