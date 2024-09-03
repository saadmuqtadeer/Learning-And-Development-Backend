using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AuthDbContext _context;

        public AuthController(IConfiguration configuration, AuthDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Registration([FromBody] Register model)
        {
            if(model == null) return BadRequest();

            // Check if the email already exists
            var existingUser = await _context.registers.FirstOrDefaultAsync(u => u.EmployeeId == model.EmployeeId);
            if (existingUser != null)
            {
                return Conflict(new { Message = "User with this email already exists." });
            }

            // Hash the password (replace with your preferred hashing method)
            //var hashedPassword = Bcrypt.Net.BCrypt.HashPassword(model.Password);

            // Create a new User entity
            var user = new Register
            {
                EmployeeId = model.EmployeeId,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                SecurityQuestion = model.SecurityQuestion,
                Role = model.Role
            };

            // Add to DbSet and save changes
            _context.registers.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Id =  user.EmployeeId, Message = "User registered successfully." });;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var model = Authenticate(login);
            if (model != null)
            {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, model.Role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
                var token = GetToken(authClaims);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            };
            return Unauthorized();
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        private Register Authenticate(Login login) {
            var user = _context.registers.FirstOrDefault(x => x.Email == login.Email);
            if(user == null) { return null; }
            if(user.Password == login.Password)
                return user;
            return null;
        }
    }

}
