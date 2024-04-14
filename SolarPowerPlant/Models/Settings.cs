using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models
{
    public class Settings
    {
        [Required]
        public ConnectionStrings? ConnectionStrings { get; set; }

        [Required]
        public Jwt? Jwt { get; set; }

        [Required]
        public string? LogsLocation { get; set; }

        [Required]
        public OpenMeteo? OpenMeteo { get; set; }
    }

    public class ConnectionStrings
    {
        [Required]
        public string? SolarPowerConnectionString { get; set; }

        [Required]
        public string? SolarPowerAuthConnectionString { get; set; }
    }

    public class Jwt
    {
        [Required]
        public string? Key { get; set; }

        [Required]
        public string? Issuer { get; set; }

        [Required]
        public string? Audience { get; set; }
    }

    public class OpenMeteo
    {
        [Required]
        public string? OpenMeteoForecastEndpoint { get; set; }

        [Required]
        public string? OpenMeteoForecastQuery { get; set; }
    }
}
