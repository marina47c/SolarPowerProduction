using SolarPowerAPI.Models.Entities;

namespace SolarPowerAPI.Repositories.QueryBuilders
{
    public interface IProductionQueryBuilder
    {
        IQueryable<Production> BuildQuery(Guid solarPlantId, DateTime startDateTimeUtc, DateTime endDateTimeUtc);
    }
}
