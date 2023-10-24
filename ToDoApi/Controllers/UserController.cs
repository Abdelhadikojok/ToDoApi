using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.tools;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoApi.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ToDoDbContex _context;
        private readonly IConfiguration _config; // You need to inject IConfiguration for accessing appsettings.json

        public UserController(ToDoDbContex context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        [Route("logIn")]
        public async Task<ActionResult<string>> LogUser([FromBody] User user)
        {
            try
            {
                var password = Password.HashPassword(user.Password);
                var dbUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == password);

                if (dbUser == null)
                {
                    return BadRequest("Email or password are invalid");
                }

                // If the user is found, generate a JWT token and return it
                var token = GenerateToken(dbUser);
                return Ok(token);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                  new Claim(ClaimTypes.NameIdentifier, user.Email),
                  new Claim("userID", user.UserId.ToString())
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Create an anonymous object to hold the token and email
            var response = new
            {
                token = tokenString,
                email = user.Email,
                userid = user.UserId,
          
            };

            // Serialize the object to JSON
            var jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response);

            Console.WriteLine(Convert.ToInt32(HttpContext.User.FindFirstValue("userId")));

            return jsonResponse;
        }
    }
}
