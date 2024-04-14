using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarPowerAPI.CustomActionFilters;
using SolarPowerAPI.Models.DTOs.SolarPlantDTOs;
using SolarPowerAPI.Models.Entities;
using SolarPowerAPI.Repositories;

namespace SolarPowerPlantAPI.Controllers
{
    //TODO: uncomment all authorizations
    [Route("api/[controller]")]
    [ApiController]
    public class SolarPlantController : ControllerBase
    {
        private readonly ISolarPlantRepo _repository;
        private readonly IMapper _mapper;

        public SolarPlantController(ISolarPlantRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetAll()
        {
            List<SolarPlant> solarPlants = await _repository.GetAllAsync();
            List<GetSolarPlantDTO> solarPlantDTOs = _mapper.Map<List<GetSolarPlantDTO>>(solarPlants);

            return Ok(solarPlantDTOs);
        }

        [HttpGet("{id:Guid}", Name = "GetById")]
        [Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetById(Guid id)
        {
            SolarPlant? solarPlant = await _repository.GetByIdAsync(id);

            if (solarPlant == null)
            {
                return NotFound("The solar plant record couldn't be found");
            }

            GetSolarPlantDTO solarPlantDTO = _mapper.Map<GetSolarPlantDTO>(solarPlant);

            return Ok(solarPlantDTO);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddSolarPlantRequestDto addSolarPlantDto)
        {
            if (addSolarPlantDto == null)
            {
                return BadRequest("Solar power plant is null.");
            }

            SolarPlant newSolarPlant = _mapper.Map<SolarPlant>(addSolarPlantDto);
            SolarPlant createdSolarPlant = await _repository.CreateAsync(newSolarPlant);
            GetSolarPlantDTO createdSolarPlantDto = _mapper.Map<GetSolarPlantDTO>(createdSolarPlant);

            return CreatedAtRoute("GetById", new { id = createdSolarPlantDto.Id }, createdSolarPlantDto);
        }

        [HttpPut("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSolarPlantRequestDto solarPlantDTO)
        {
            if (solarPlantDTO == null)
            {
                return BadRequest("Solar plant is null.");
            }

            SolarPlant solarPlant = _mapper.Map<SolarPlant>(solarPlantDTO);
            SolarPlant? updatedSolarPlant = await _repository.UpdateAsync(id, solarPlant);
            if (updatedSolarPlant == null)
            {
                return NotFound("The solar plant record couldn't be found");
            }

            GetSolarPlantDTO updatedSolarPlantDto = _mapper.Map<GetSolarPlantDTO>(updatedSolarPlant);

            return Ok(updatedSolarPlantDto);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            SolarPlant? deletedSolarPlant = await _repository.DeleteAsync(id);
            if (deletedSolarPlant == null)
            {
                return NotFound("The solar plant record couldn't be found");
            }

            GetSolarPlantDTO deletedSolarPlantDTO = _mapper.Map<GetSolarPlantDTO>(deletedSolarPlant);

            return Ok(deletedSolarPlantDTO);
        }
    }
}
