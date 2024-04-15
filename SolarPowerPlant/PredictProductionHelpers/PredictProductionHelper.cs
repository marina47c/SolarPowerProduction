using SolarPowerAPI.Enums;

namespace SolarPowerAPI.PredictProductionHelpers
{
    public class PredictProductionHelper
    {
        Random random = new Random();

        private readonly SortedDictionary<TimeOnly, ProductionFactors> periodMap = new SortedDictionary<TimeOnly, ProductionFactors>()
        {
            { new TimeOnly(7, 0), ProductionFactors.NoProductionFactor },
            { new TimeOnly(9, 0), ProductionFactors.LowProductionFactor },
            { new TimeOnly(11, 0), ProductionFactors.ModerateProductionFactor},
            { new TimeOnly(13, 0), ProductionFactors.HightProductionFactor },
            { new TimeOnly(15, 0), ProductionFactors.TopProductionFactor },
            { new TimeOnly(17, 0), ProductionFactors.HightProductionFactor },
            { new TimeOnly(19, 0), ProductionFactors.ModerateProductionFactor },
            { new TimeOnly(21, 0), ProductionFactors.LowProductionFactor },
            { new TimeOnly(0, 0), ProductionFactors.NoProductionFactor },
        };

        private readonly SortedDictionary<WeatherCode, ProductionFactors> weatherMap = new SortedDictionary<WeatherCode, ProductionFactors>()
        {
            { WeatherCode.ClearSky, ProductionFactors.TopProductionFactor },
            { WeatherCode.MainlyClear, ProductionFactors.TopProductionFactor },
            { WeatherCode.PartlyCloudy, ProductionFactors.HightProductionFactor },
            { WeatherCode.Overcast, ProductionFactors.ModerateProductionFactor },
            { WeatherCode.Fog, ProductionFactors.LowProductionFactor },
            { WeatherCode.DepositingRimeFog, ProductionFactors.NoProductionFactor },
            { WeatherCode.DrizzleLight, ProductionFactors.ModerateProductionFactor },
            { WeatherCode.DrizzleModerate, ProductionFactors.ModerateProductionFactor },
            { WeatherCode.DrizzleDense, ProductionFactors.LowProductionFactor },
            { WeatherCode.FreezingDrizzleLight, ProductionFactors.LowProductionFactor },
            { WeatherCode.FreezingDrizzleDense, ProductionFactors.NoProductionFactor },
            { WeatherCode.RainSlight, ProductionFactors.ModerateProductionFactor },
            { WeatherCode.RainModerate, ProductionFactors.LowProductionFactor },
            { WeatherCode.RainHeavy, ProductionFactors.LowProductionFactor },
            { WeatherCode.FreezingRainLight, ProductionFactors.NoProductionFactor },
            { WeatherCode.FreezingRainHeavy, ProductionFactors.NoProductionFactor },
            { WeatherCode.SnowFallSlight, ProductionFactors.LowProductionFactor },
            { WeatherCode.SnowFallModerate, ProductionFactors.NoProductionFactor },
            { WeatherCode.SnowFallHeavy, ProductionFactors.NoProductionFactor },
            { WeatherCode.SnowGrains, ProductionFactors.NoProductionFactor },
            { WeatherCode.RainShowersSlight, ProductionFactors.ModerateProductionFactor },
            { WeatherCode.RainShowersModerate, ProductionFactors.LowProductionFactor },
            { WeatherCode.RainShowersViolent, ProductionFactors.NoProductionFactor },
            { WeatherCode.SnowShowersSlight, ProductionFactors.LowProductionFactor },
            { WeatherCode.SnowShowersHeavy, ProductionFactors.NoProductionFactor },
            { WeatherCode.ThunderstormSlight, ProductionFactors.LowProductionFactor },
            { WeatherCode.ThunderstormModerate, ProductionFactors.NoProductionFactor },
            { WeatherCode.ThunderstormHeavyHail, ProductionFactors.NoProductionFactor },
        };

        private readonly SortedDictionary<double, ProductionFactors> temperatureMap = new SortedDictionary<double, ProductionFactors>()
        {
            { double.MinValue, ProductionFactors.NoProductionFactor },
            { -10, ProductionFactors.LowProductionFactor },
            { 0, ProductionFactors.ModerateProductionFactor },
            { 10, ProductionFactors.HightProductionFactor },
            { 25, ProductionFactors.TopProductionFactor },
            { 35, ProductionFactors.TopProductionFactor },
            { 40, ProductionFactors.HightProductionFactor },
            { 45, ProductionFactors.ModerateProductionFactor },
            { 50, ProductionFactors.LowProductionFactor },
            { double.MaxValue, ProductionFactors.NoProductionFactor }
        };

        public double GetTimeEffectOnProduction(TimeOnly currentTime)
        {
            KeyValuePair<TimeOnly, ProductionFactors> period = periodMap.FirstOrDefault(kv => currentTime <= kv.Key);
            if (period.Equals(default(KeyValuePair<TimeOnly, ProductionFactors>)))
            { 
                return 0;
            }
            
            return (double)period.Value / 100;
        }

        public double GetWatherEffectOnProduction(WeatherCode currentWeatherCode)
        {
            KeyValuePair<WeatherCode, ProductionFactors> weather = weatherMap.FirstOrDefault(kv => currentWeatherCode == kv.Key);
            if (weather.Equals(default(KeyValuePair<WeatherCode, ProductionFactors>)))
            {
                return 0;
            }

            return (double)weather.Value / 100;
        }

        public double GetTemperatureEffectOnProduction(double currentTemperature)
        {
            KeyValuePair<double, ProductionFactors> temperature = temperatureMap.FirstOrDefault(kv => currentTemperature <= kv.Key);
            if (temperature.Equals(default(KeyValuePair<double, ProductionFactors>)))
            {
                return 0;
            }

            return (double)temperature.Value / 100;
        }

        public double GetRandomFactorEffect(double minNumber, double maxNumber)
        {
            return random.NextDouble() * (maxNumber - minNumber) + minNumber;
        }
    }
}
