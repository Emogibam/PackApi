using Microsoft.EntityFrameworkCore;
using PackApi.Data;
using PackApi.Models;
using PackApi.repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PackApi.repository.Implementation
{
    public class TrailsRepository : ITrailsRepository
    {
        private readonly ApplicationDbContext _db;
        public TrailsRepository(ApplicationDbContext db)
        {
            _db = db;

        }

        public bool CreateTrails(Trails park)
        {
            _db.Trails.Add(park);
            return Save();
        }

        public bool DeleteTrails(Trails park)
        {
            _db.Trails.Remove(park);
            return Save();
        }
        public Trails GetTrail(int id)
        {
            return _db.Trails.Include(c => c.NationalPark).FirstOrDefault(x => x.Id == id);

        }


        public ICollection<Trails> Gettrails()
        {
            return _db.Trails.Include(c => c.NationalPark).OrderBy(a => a.Name).ToList();
        }

        public bool TrailExist(string name)
        {
            bool value = _db.Trails.Any(x => x.Name.Trim().ToLower() == name.ToLower().Trim());
            return value;
        }

        public bool TrailExist(int id)
        {
            bool value = _db.Trails.Any(x => x.Id == id);
            return value;
        }
        public bool UpdateTrail(Trails park)
        {
            _db.Trails.Update(park);
            return Save();
        }

        public bool Save()

        {
            return _db.SaveChanges() >= 0 ? true : false;

        }

        public ICollection<Trails> GetTrailsInNationalPark(int id)
        {
            return _db.Trails.Include(c => c.NationalPark).Where(c => c.NationalParkID == id).ToList();
        }
    }
}
