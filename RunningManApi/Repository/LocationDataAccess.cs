using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class LocationDataAccess
    {
       public List<Location> GetLocations()
        {
            var dataBase = new MyDbContext();
            var locations = dataBase.Locations.Select(x => new Location
            {
                Id = x.Id,
                Address= x.Address
            });
            return locations.ToList();
        }

        public void CreateLocation(LocationDTO locationDTO)
        {
            var dataBase = new MyDbContext();
            var location = new Location
            {
                Address = locationDTO.Adress
            };
            dataBase.Add(location);
            dataBase.SaveChanges();
        }

        public void UpdateLocation(int id, LocationDTO locationDTO)
        {
            var dataBase = new MyDbContext();
            var location = dataBase.Locations.SingleOrDefault(x => x.Id == id);
            if(location != null)
            {
                location.Address = locationDTO.Adress;
                dataBase.SaveChanges();
            }    
        }

        public void DeleteLocation(int id)
        {
            var dataBase = new MyDbContext();
            var location = dataBase.Locations.SingleOrDefault(x => x.Id == id);
            if(location != null)
            {
                dataBase.Remove(location);
                dataBase.SaveChanges();
            }    
        }
    }
}
