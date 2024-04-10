namespace SolarPowerAPI.Models
{
    public class Settings
    {
        public ConnectionStrings? ConnectionStrings { get; set; }
        public Jwt? Jwt { get; set; }
        public string? LogsLocation { get; set; }
    }

    public class ConnectionStrings
    {
        public string? SolarPowerConnectionString { get; set; }
        public string? SolarPowerAuthConnectionString { get; set; }
    }

    public class Jwt
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }
}
