using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SolarPowerAPI.CustomActionFilters;
using SolarPowerAPI.Models.DTOs.ForecastDTOs;
using SolarPowerAPI.Models.Entities;
using SolarPowerAPI.Repositories;

namespace SolarPowerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastController : ControllerBase
    {
        private readonly IForecastRepository _repository;
        private readonly IMapper _mapper;

        public ForecastController(IForecastRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [ValidateModel]
        //[Authorize(Roles = "Reader, Writer")]
        public async Task<IActionResult> GetForecast([FromQuery] GetForecastRequestDto getForecastRequest)
        {
            List<Forecast>? forecast = await _repository.GetForecastedDataAsync(getForecastRequest);
            if (forecast == null)
            {
                return NotFound("Forecasted data can not be found");
            }

            List<GetForecastDto> forecastDtos = _mapper.Map<List<GetForecastDto>>(forecast);

            return Ok(forecastDtos);
        }
    }
}
