using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Registers.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Registers.FindAsync(id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Register updatedUser)
        {
            if (updatedUser == null) return BadRequest();

            var user = await _context.Registers.FindAsync(id);
            if (user == null) return NotFound("User not found.");

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.SecurityQuestion = updatedUser.SecurityQuestion;
            user.Role = updatedUser.Role;

            _context.Registers.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User updated successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Registers.FindAsync(id);
            if (user == null) return NotFound("User not found.");

            _context.Registers.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User deleted successfully." });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Registration([FromBody] Register model)
        {
            if (model == null) return BadRequest();

            var existingUser = await _context.Registers.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (existingUser != null) return Conflict(new { Message = "User with this email already exists." });

            // Hash the password (replace with your preferred hashing method)
            //var hashedPassword = Bcrypt.Net.BCrypt.HashPassword(model.Password);

            var user = new Register
            {
                EmployeeId = model.EmployeeId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber,
                SecurityQuestion = model.SecurityQuestion,
                Role = model.Role
            };

            _context.Registers.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { Id = user.EmployeeId, Message = "User registered successfully." });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                return BadRequest("Invalid login request.");
            }

            var model = _context.Registers.FirstOrDefault(x => x.Email == login.Email);

            if (model == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Ensure to use proper password hashing and verification in a real application
            if (model.Password != login.Password)
            {
                return Unauthorized("Invalid email or password.");
            }

            string username = model.FirstName + " " + model.LastName;
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, model.Role),
                new Claim(ClaimTypes.NameIdentifier, model.EmployeeId.ToString())
            });

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddHours(3),
                Subject = identity,
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                expiration = tokenDescriptor.Expires,
                role = model.Role,
                Id = model.EmployeeId
            });
        }
    }
}
