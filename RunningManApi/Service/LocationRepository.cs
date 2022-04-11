using RunningManApi.DTO.Models;
using RunningManApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Service
{
    public class LocationRepository:ILocationRepository
    {
        private readonly LocationDataAccess locationData;
        private readonly RoundDataAccess roundData;

        public LocationRepository()
        {
            locationData = new LocationDataAccess();
            roundData = new RoundDataAccess();
        }

        public LocationIdDTO CreateLocation(LocationDTO locationDTO)
        {
            var checkLocation = locationData.GetLocations().SingleOrDefault(x => x.Address == locationDTO.Adress);
            if(checkLocation != null)
            {
                throw new Exception();
            }
            var location = new LocationDTO
            {
                Adress = locationDTO.Adress
            };
            locationData.CreateLocation(location);
            var newLocation = locationData.GetLocations().SingleOrDefault(x => x.Address == locationDTO.Adress);
            var result = new LocationIdDTO
            {
                Id = newLocation.Id,
                Adress = newLocation.Address
            };
            return result;
        }

        public void DeleteLocation(string address)
        {
            var checkLocation = locationData.GetLocations().SingleOrDefault(x => x.Address == address);
            if(checkLocation == null)
            {
                throw new Exception();
            }
            var checkRound = roundData.GetRound().Where(x => x.LocationId == checkLocation.Id);
            if(checkRound.Any())
            {
                foreach (int id in checkRound.Select(x => x.Id))
                {
                    roundData.DeleteRound(id);
                }
            }    
               
            locationData.DeleteLocation(checkLocation.Id);
        }

        public List<LocationIdDTO> GetLocation(string address)
        {
            var location = locationData.GetLocations().AsQueryable();
            if(!string.IsNullOrEmpty(address))
            {
                location = location.Where(x => x.Address.Contains(address));
            }
            var result = location.Select(x => new LocationIdDTO
            {
                Id = x.Id,
                Adress = x.Address
            });
            return result.ToList();

        }

        public void UpdateLocation(int id, LocationDTO locationDTO)
        {
            var checkLocation = locationData.GetLocations().SingleOrDefault(x => x.Id == id);
            if(checkLocation == null)
            {
                throw new Exception();
            }
            var location = new LocationDTO
            {
                Adress = locationDTO.Adress
            };
            locationData.UpdateLocation(id, location);

        }
    }
}
