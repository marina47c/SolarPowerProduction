using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolarPowerAPI.Models.DTOs.AuthDTOs;
using SolarPowerAPI.Repositories;

namespace SolarPowerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ITokenRepository _tokenRepository { get; }

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            IdentityUser identityUser = new()
            {
                UserName = registerRequestDto.Usename,
                Email = registerRequestDto.Usename
            };

            IdentityResult identityResult =  await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityResult.Succeeded)
            {
                identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles ?? ["Reader"]);
                if (identityResult.Succeeded)
                {
                    return Ok("User was registered.");
                }
            }

            return BadRequest("Something went wrong while registering user");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null) 
            { 
                bool passwordCorrect = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (passwordCorrect)
                {
                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    if (roles.Any())
                    {
                        string jwtToken = _tokenRepository.CreateJWTToken(user, roles.ToList());

                        LoginResponseDto response = new()
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
