using SolarPowerAPI.Models.Entities;

namespace SolarPowerAPI.Mock
{
    public class ProductionMock
    {
        // Constants for power factors
        private const double LowPowerProductionFactor = 0.3;
        private const double ModeratePowerProductionFactor = 0.6;
        //Solar plant id
        private static long id = 1;
        // Random number generator
        Random random = new Random();

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

        // TODO: replace this with GetTimeEffectOnProduction from PredisctProduction
        private double GetMockProducedPower(TimeOnly currentTime, double installedPower)
        {
            PeriodOfDay? periodOfDay = GetPeriodOfDay(currentTime);
            if (periodOfDay == null)
            {
                return 0;
            }

            // Define power production variables
            double noPowerProduction = 0;
            // devide with 4 because we are setting 15 min data
            double quarterInstalledPower = installedPower / 4; 
            double lowPowerProduction = quarterInstalledPower * LowPowerProductionFactor;
            double moderatePowerProduction = quarterInstalledPower * ModeratePowerProductionFactor;

            // Determine power production based on the period of the day
            switch (periodOfDay)
            {
                case PeriodOfDay.Night:
                    return noPowerProduction; 
                case PeriodOfDay.EarlyMorning:
                case PeriodOfDay.Evening:
                    return GetRandomNumber(noPowerProduction, lowPowerProduction); 
                case PeriodOfDay.Morning:
                case PeriodOfDay.Afternoon:
                    return GetRandomNumber(lowPowerProduction, moderatePowerProduction); 
                case PeriodOfDay.Noon:
                    return GetRandomNumber(moderatePowerProduction, quarterInstalledPower); 
                default:
                    return 0; 
            }
        }

        // Very arbitrary separation of day periods
        private PeriodOfDay? GetPeriodOfDay(TimeOnly currentTime)
        {
            if ((currentTime <= new TimeOnly(7, 00)) || currentTime > new TimeOnly(19, 00))
            {
                return PeriodOfDay.Night;
            }
            else if ((currentTime > new TimeOnly(7, 00)) && (currentTime <= new TimeOnly(9, 00)))
            {
                return PeriodOfDay.EarlyMorning;
            }
            else if ((currentTime > new TimeOnly(11)) && (currentTime <= new TimeOnly(12, 00)))
            {
                return PeriodOfDay.Morning;
            }
            else if ((currentTime > new TimeOnly(12, 00)) && (currentTime <= new TimeOnly(15, 00)))
            {
                return PeriodOfDay.Noon;
            }
            else if (( currentTime > new TimeOnly(15, 00)) && (currentTime <= new TimeOnly(17, 00)))
            {
                return PeriodOfDay.Afternoon;
            }
            else if ( (currentTime > new TimeOnly(17,00)) && (currentTime <= new TimeOnly(19, 00)))
            {
                return PeriodOfDay.Evening;
            }
            else
            {
                return null;
            }
        }

        private double GetRandomNumber(double minNumber, double maxNumber)
        {
            return random.NextDouble() * (maxNumber - minNumber) + minNumber;
        }

        private enum PeriodOfDay
        {
            Night = 0,
            EarlyMorning = 1,
            Morning = 2,
            Noon = 3,
            Afternoon = 4,
            Evening = 5
        }
    }
}
