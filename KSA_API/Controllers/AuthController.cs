using KSA_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KSA_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : Controller
    {
        KSA_DBContext Context = new();
        private readonly ILogger<VehicleController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(ILogger<VehicleController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration= configuration;
        }

        [AllowAnonymous]
        [HttpPost(nameof(Auth))]
        public IActionResult Auth(string Username, string Password)
        {
            //bool isValid = _userService.IsValidUserInformation(data);

            ApiLogin account = Context.ApiLogins.Where(c => c.UserName == Username && c.Password == Password).SingleOrDefault();
            if (account != null)
            {
                var tokenString = GenerateJwtToken(account);
                _logger.LogInformation($"{Username} logged in.");
                return Ok(new { Token = tokenString, Message = "Success" });
            }
            return BadRequest("Please pass the valid Username and Password");
        }
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            return Ok("API Validated");
        }
        /// <summary>
        /// Generate JWT Token after successful login.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private string GenerateJwtToken(ApiLogin account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, account.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, account.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(account.TokenLifetime),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
