using RunningManApi.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public interface ILocationRepository
    {
        List<LocationIdDTO> GetLocation(string address);
        LocationIdDTO CreateLocation(LocationDTO locationDTO);
        void UpdateLocation(int id, LocationDTO locationDTO);
        void DeleteLocation(string address);
        

    }
}
