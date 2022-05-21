using AutoMapper;
using PackApi.Models;
using PackApi.Models.DTOs;

namespace PackApi.ParkMapper
{
    public class ParkMappings: Profile
    {
        public ParkMappings()
        {
            CreateMap<NationalPack,NationalParkDtos>().ReverseMap();
        }
    }
}
