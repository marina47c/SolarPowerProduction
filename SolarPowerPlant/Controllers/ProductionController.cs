using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarPowerAPI.CustomActionFilters;
using SolarPowerAPI.Models.DTOs.ProductionDTOs;
using SolarPowerAPI.Models.Entities;
using SolarPowerAPI.Repositories;

namespace SolarPowerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionController : ControllerBase
    {
        private readonly IProductionRepo _repository;
        private readonly IMapper _mapper;

        public ProductionController(IProductionRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ValidateModel]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetProduction([FromQuery] GetProductionRequestDto getProductionRequest) 
        {
            List<Production>? production = await _repository.GetProductionAsync(getProductionRequest);
            if (production == null)
            {
                return NotFound("Production data can not be found");
            }

            List<GetProductionDto> productionDtos = _mapper.Map<List<GetProductionDto>>(production);

            return Ok(productionDtos);
        }
    }
}
