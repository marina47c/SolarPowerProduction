using SolarPowerAPI.Data;
using SolarPowerAPI.Models.Entities;

namespace SolarPowerAPI.Repositories.QueryBuilders
{
    public class ProductionQueryBuilder : IProductionQueryBuilder
    {
        private readonly SolarPowerContext _context;

        public ProductionQueryBuilder(SolarPowerContext context)
        {
            _context = context;
        }

        public IQueryable<Production> BuildQuery(Guid solarPlantId, DateTime startDateTimeUtc, DateTime endDateTimeUtc)
        {
            return _context.Production
                .Where(p => p.SolarPowerPlantId == solarPlantId
                            && p.ProductionDateTime >= startDateTimeUtc
                            && p.ProductionDateTime <= endDateTimeUtc);
        }
    }
}
