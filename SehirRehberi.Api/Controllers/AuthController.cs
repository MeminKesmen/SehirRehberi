using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SehirRehberi.Api.Dtos;
using SehirRehberi.Business.Abstract;
using SehirRehberi.Entity.Concrete;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SehirRehberi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (await _authService.UserExist(userForRegisterDto.UserName))
            {
                ModelState.AddModelError("UserName","Username already exists");
            }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var userToCreate = new User {UserName=userForRegisterDto.UserName };
            var createdUser = await _authService.Register(userToCreate,userForRegisterDto.Password);
            return StatusCode(201);
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            var user = await _authService.Login(userForLoginDto.UserName,userForLoginDto.Password);
            if (user==null) { return Unauthorized(); }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha512)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(tokenString);
        }
    }
}
