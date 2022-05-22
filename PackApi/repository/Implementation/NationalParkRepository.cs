using PackApi.Data;
using PackApi.Models;
using PackApi.repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PackApi.repository.Implementation
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext _db;
         
        public NationalParkRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public bool CreateNationalPark(NationalPack park)
        {
            _db.NationPacks.Add(park);
            return Save();
        }

        public bool DeleteNationalPark(NationalPack park)
        {
            _db.NationPacks.Remove(park);
            return Save();
        }
        public NationalPack GetNationalPack(int id)
        {
           return _db.NationPacks.FirstOrDefault(x => x.Id == id);

        }
        

        public ICollection<NationalPack> GetNationalParks()
        {
            return _db.NationPacks.OrderBy(a => a.Name).ToList();
        }

        public bool NationalParkExist(string name)
        {
            bool value = _db.NationPacks.Any(x => x.Name.Trim().ToLower() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExist(int id)
        {
            bool value = _db.NationPacks.Any(x => x.Id == id);
            return value;
        }
        public bool UpdateNationalPark(NationalPack park)
        {
            _db.NationPacks.Update(park);
            return Save();
        }

        public bool Save()

        {
        return  _db.SaveChanges() >= 0 ? true: false;

        }
        

    }
}
