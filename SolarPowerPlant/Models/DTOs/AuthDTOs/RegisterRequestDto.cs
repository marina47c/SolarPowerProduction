using System.ComponentModel.DataAnnotations;

namespace SolarPowerAPI.Models.DTOs.AuthDTOs
{
    public class RegisterRequestDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Usename { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
