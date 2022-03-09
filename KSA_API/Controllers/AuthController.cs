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
            var key = Encoding.ASCII.GetBytes(account.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.UserName) }),
                Expires = DateTime.UtcNow.AddMinutes(account.TokenLifetime),
                Issuer = account.Issuer,
                Audience = account.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
