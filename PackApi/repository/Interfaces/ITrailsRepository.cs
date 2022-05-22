using PackApi.Models;
using System.Collections.Generic;

namespace PackApi.repository.Interfaces
{
    public interface ITrailsRepository
    {
        ICollection<Trails> Gettrails();
        ICollection<Trails> GetTrailsInNationalPark (int id);
        Trails GetTrail(int trailsId);

        bool TrailExist(string trails);
        bool TrailExist(int id);
        bool CreateTrails(Trails trails);
        bool UpdateTrail(Trails trails);
        bool DeleteTrails(Trails trails);
        bool Save();
    }
}
