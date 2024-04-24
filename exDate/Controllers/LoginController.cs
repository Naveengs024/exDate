using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using exDate.Core;
using exDate.DTO.Login;
using exDate.Models;
using exDate.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace exDate.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, IConfiguration configuration)
        {
            _loginService = loginService;
            _configuration = configuration;
        }
      
        [HttpGet("UserDetails")]
        [Authorize] 
        public async Task<IActionResult> GetUserDetails()
        {
            var details = await _loginService.GetUserDetails();
            return Ok(details);
        }

        [HttpGet("UserEmailValidation")]
        [Authorize] 
        public async Task<IActionResult> GetUserEmailValidation(string User_Email)
        {
            var details = await _loginService.GetUserEmailValidation(User_Email);
            return Ok(details);
        }

        [HttpPost("Registration")]
        [AllowAnonymous] 
        public async Task<IActionResult> CreateProductProcess(Login Details)
        {
            var details = await _loginService.CreateProductProcess(Details);
            return Ok(details);
        }

        [HttpPost("login")]
        [AllowAnonymous] 
        public async Task<ActionResult<string>> Login(LoginInput model)
        {
            var user = await _loginService.AuthenticateAsync(model.User_Name, model.Password);

            if (user == null)
                return Unauthorized();

            var tokenString = GenerateJWTToken(user.UserId, user.User_Name);
            
            return Ok(new { Token = tokenString });
        }

        private string GenerateJWTToken(int userId, string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(ClaimTypes.Name, username),
               
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
