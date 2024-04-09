using Microsoft.EntityFrameworkCore;
using SolarPowerAPI.Enums;
using SolarPowerAPI.Models.DTOs.ProductionDTOs;
using SolarPowerAPI.Models.Entities;
using SolarPowerAPI.Repositories.QueryBuilders;

namespace SolarPowerAPI.Repositories
{
    public class ProductionRepo : IProductionRepo
    {
        readonly IProductionQueryBuilder _queryBuilder;

        public ProductionRepo(IProductionQueryBuilder queryBuilder)
        {
            _queryBuilder = queryBuilder;
        }

        public async Task<List<Production>?> GetProductionAsync(GetProductionRequestDto getProductionRequest)
        {
            try
            {
                DateTime startDateTimeUtc = getProductionRequest.StartDateTime.ToUniversalTime();
                DateTime endDateTimeUtc = getProductionRequest.EndDateTime.ToUniversalTime();

                IQueryable<Production> query = _queryBuilder.BuildQuery(getProductionRequest.SolarPlantId, startDateTimeUtc, endDateTimeUtc);

                switch (getProductionRequest.GranularityLevel)
                {
                    case GranularityLevel.OneHour:
                        return await GetProductionByHourAsync(query, getProductionRequest);
                    case GranularityLevel.FifteenMinutes:
                        return await GetProductionByFifteenMinutesAsync(query, getProductionRequest);
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                //TODO: log
                return null;
            }
        }

        private async Task<List<Production>?> GetProductionByHourAsync(IQueryable<Production> query, GetProductionRequestDto getProductionRequest)
        {
            // Group productions by date and hour, summing produced power for each hour
            List<Production> productions = await query
                .GroupBy(p => new { p.ProductionDateTime.Date, p.ProductionDateTime.Hour })
                .Select(g => new Production
                {
                    ProductionDateTime = new DateTime(g.Key.Date.Year, g.Key.Date.Month, g.Key.Date.Day, g.Key.Hour, 0, 0),
                    ProducedPower = g.Sum(p => p.ProducedPower),
                    SolarPowerPlantId = getProductionRequest.SolarPlantId,
                })
                .ToListAsync();

            productions = productions.OrderBy(p => p.ProductionDateTime).ToList();

            return productions;
        }

        private async Task<List<Production>?> GetProductionByFifteenMinutesAsync(IQueryable<Production>query, GetProductionRequestDto getProductionRequest)
        {
            List<Production> productions = await query.ToListAsync();
            productions = productions.OrderBy(p => p.ProductionDateTime).ToList();

            return productions;
        }
    }
}
