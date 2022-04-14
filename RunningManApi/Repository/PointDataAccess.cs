using RunningManApi.DTO.Models;
using RunningManApi.Repository.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningManApi.Repository
{
    public class PointDataAccess
    {
        public List<Point> GetPoint()
        {
            var dataBase = new MyDbContext();
            var point = dataBase.Points.Select(x => new Point
            {
                Id = x.Id,
                Point1 = x.Point1,
                AccountId = x.AccountId,
                TeamId = x.TeamId
            });
            return point.ToList();
        }

        public void CreatePoint(PointDTO point)
        {
            var dataBase = new MyDbContext();
            var checkPoint = dataBase.Points.SingleOrDefault(x => x.AccountId == point.AccountId);
            if(checkPoint == null)
            {
                var result = new Point
                {
                    Point1 = point.Point1,
                    AccountId = point.AccountId,
                    TeamId = point.TeamId

                };
                dataBase.Add(result);
                dataBase.SaveChanges();
            }    
        }

        public void UpdatePoint(int id, PointDTO point)
        {
            var dataBase = new MyDbContext();
            var checkPoint = dataBase.Points.SingleOrDefault(x => x.Id == id);
            if(checkPoint != null)
            {
                checkPoint.Point1 = point.Point1;
                dataBase.SaveChanges();
            }    
        }

        public void DeletePoint(int id)
        {
            var dataBase = new MyDbContext();
            var checkPoint = dataBase.Points.SingleOrDefault(x => x.Id == id);
            if (checkPoint != null)
            {
                dataBase.Remove(checkPoint);
                dataBase.SaveChanges();
            }
        }
    }
}
