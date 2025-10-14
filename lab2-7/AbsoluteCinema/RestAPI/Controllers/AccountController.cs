using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestAPI.Models.ViewModels;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims;         
using Microsoft.IdentityModel.Tokens; 
using System.Text;                     
using Microsoft.Extensions.Configuration;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        public AccountController(UserManager<IdentityUser> userMgr, IConfiguration config)
        {
            userManager = userMgr;
            configuration = config;
        }
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName ?? user.Email)
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiryMinutes = double.Parse(jwtSettings["DurationInMinutes"]);
            var expiry = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "Registration successful." });
            }

            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new { errors });
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            IdentityUser? user = await userManager.FindByEmailAsync(loginModel.Email);

            if (user != null && await userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                var tokenString = GenerateJwtToken(user);

                return Ok(new
                {
                    message = "Login successful.",
                    token = tokenString, 
                    user_id = user.Id
                });
            }

            return Unauthorized(new { message = "Invalid email or password." });
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logged out (Token discarded by client)." });
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await userManager.GetUserAsync(User);

            if (user == null) return Unauthorized(new { message = "Unauthorized access." });

            return Ok(new { user.Email, user.UserName, user.Id });
        }
    }
}