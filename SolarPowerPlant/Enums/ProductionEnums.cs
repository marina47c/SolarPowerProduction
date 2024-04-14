using System.ComponentModel;

namespace SolarPowerAPI.Enums
{
    public enum GranularityLevel
    {
        FifteenMinutes = 0,
        OneHour = 1,
    }

    public enum WeatherCode
    {
        [Description("Clear sky")]
        ClearSky = 0,
        [Description("Mainly clear")]
        MainlyClear = 1,
        [Description("Partly cloudy")]
        PartlyCloudy = 2,
        [Description("Overcast")]
        Overcast = 3,
        [Description("Fog")]
        Fog = 45,
        [Description("Depositing rime fog")]
        DepositingRimeFog = 48,
        [Description("Drizzle: Light intensity")]
        DrizzleLight = 51,
        [Description("Drizzle: Moderate intensity")]
        DrizzleModerate = 53,
        [Description("Drizzle: Dense intensity")]
        DrizzleDense = 55,
        [Description("Freezing Drizzle: Light intensity")]
        FreezingDrizzleLight = 56,
        [Description("Freezing Drizzle: Dense intensity")]
        FreezingDrizzleDense = 57,
        [Description("Rain: Slight intensity")]
        RainSlight = 61,
        [Description("Rain: Moderate intensity")]
        RainModerate = 63,
        [Description("Rain: Heavy intensity")]
        RainHeavy = 65,
        [Description("Freezing Rain: Light intensity")]
        FreezingRainLight = 66,
        [Description("Freezing Rain: Heavy intensity")]
        FreezingRainHeavy = 67,
        [Description("Snow fall: Slight intensity")]
        SnowFallSlight = 71,
        [Description("Snow fall: Moderate intensity")]
        SnowFallModerate = 73,
        [Description("Snow fall: Heavy intensity")]
        SnowFallHeavy = 75,
        [Description("Snow grains")]
        SnowGrains = 77,
        [Description("Rain showers: Slight intensity")]
        RainShowersSlight = 80,
        [Description("Rain showers: Moderate intensity")]
        RainShowersModerate = 81,
        [Description("Rain showers: Violent intensity")]
        RainShowersViolent = 82,
        [Description("Snow showers: Slight intensity")]
        SnowShowersSlight = 85,
        [Description("Snow showers: Heavy intensity")]
        SnowShowersHeavy = 86,
        [Description("Thunderstorm: Slight intensity")]
        ThunderstormSlight = 95,
        [Description("Thunderstorm: Moderate intensity")]
        ThunderstormModerate = 96,
        [Description("Thunderstorm: Heavy hail")]
        ThunderstormHeavyHail = 99
    }

    public enum ProductionFactors
    {
        NoProductionFactor = 0,
        LowProductionFactor = 30,
        ModerateProductionFactor = 50,
        HightProductionFactor = 70,
        TopProductionFactor = 100
    }
}
