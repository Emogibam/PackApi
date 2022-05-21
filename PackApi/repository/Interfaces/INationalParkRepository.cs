using PackApi.Models;
using System.Collections.Generic;

namespace PackApi.repository.Interfaces
{
    public interface INationalParkRepository
    {
        ICollection<NationalPack> GetNationalParks();
        NationalPack GetNationalPack(int id);

        bool NationalParkExist(string name);
        bool NationalParkExist(int id);
        bool CreateNationalPark(NationalPack park);
        bool UpdateNationalPark(NationalPack park);
        bool DeleteNationalPark(NationalPack park);
        bool Save();

    }
}
