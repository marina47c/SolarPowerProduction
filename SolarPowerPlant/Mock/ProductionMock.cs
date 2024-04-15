using SolarPowerAPI.Models.Entities;
using SolarPowerAPI.PredictProductionHelpers;

namespace SolarPowerAPI.Mock
{
    public class ProductionMock
    {
        private static long id = 1;
        private readonly PredictProductionHelper _predictProductionHelper;

        public ProductionMock(PredictProductionHelper predictProductionHelper)
        {
            _predictProductionHelper = predictProductionHelper;
        }

        public List<Production> CreateMockPowerProduction(Guid solarPowerPlantId, double installedPower, DateTime productionStartDateTime, DateTime productionEndDateTime)
        {
            DateTime currentDateTime = productionStartDateTime;
            List<Production> powerProductionMock = new();

            while (currentDateTime < productionEndDateTime)
            {
                Production powerProduction = new()
                {
                    Id = id++,
                    ProducedPower = GetMockProducedPower(TimeOnly.FromDateTime(currentDateTime), installedPower),
                    ProductionDateTime = currentDateTime,
                    SolarPowerPlantId = solarPowerPlantId
                };
                powerProductionMock.Add(powerProduction);
                currentDateTime = currentDateTime.AddMinutes(15).ToUniversalTime();
            }

            return powerProductionMock;
        }

        private double GetMockProducedPower(TimeOnly currentTime, double installedPower)
        {
            double timeEffectOnProduction = _predictProductionHelper.GetTimeEffectOnProduction(currentTime);
            double otherUncoveredEffectOnProduction = _predictProductionHelper.GetRandomFactorEffect(0, 1);
            double maxProductionIn15Min = installedPower / 4;

            return timeEffectOnProduction * otherUncoveredEffectOnProduction * maxProductionIn15Min;
        }
    }
}
