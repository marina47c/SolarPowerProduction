using AutoMapper;
using SolarPowerAPI.Models.DTOs.ProductionDTOs;
using SolarPowerAPI.Models.DTOs.SolarPlantDTOs;
using SolarPowerAPI.Models.Entities;

namespace SolarPowerPlantAPI.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<SolarPlant, GetSolarPlantDTO>().ReverseMap();
            CreateMap<SolarPlant, AddSolarPlantRequestDto>().ReverseMap();
            CreateMap<SolarPlant, UpdateSolarPlantRequestDto>().ReverseMap();

            CreateMap<Production, GetProductionDto>().ReverseMap();
        }
    }
}
