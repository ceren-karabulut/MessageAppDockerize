using MessageCase.API.Dto;
using MessageCase.Data.Entities;
using MessageCase.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace MessageCase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserRepository _userRepository;
        private ILogger<UserController> _logger;

        public UserController(IConfiguration configuration, IUserRepository userRepository , ILogger<UserController> logger)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistryDto registryDto)
        {
            var userExist = await _userRepository.UserExist(registryDto.Username);
            if (userExist)
            {
                return BadRequest("Kullanıcı zaten mevcut!"); 
            }

            if (!ModelState.IsValid)
            {
                
                return BadRequest();
            }

            var user = new User()
            {
                Username = registryDto.Username,
                CreateTime = DateTime.Now
            };

            var createdUser = await _userRepository.Register(user, registryDto.Password);
            _logger.LogWarning("yeni kullanici olusturuldu"+"+username:"+registryDto.Username);
            return StatusCode(201);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepository.Login(loginDto.Username, loginDto.Password);
            if (user == null)
            {
                _logger.LogError("Basarisiz oturum denemesi!"+ "+"+loginDto.Username);
                return Unauthorized("Kullanıcı bulunamadı");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("appsettings:Token").Value);

            var tokenDesc = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {

                    new Claim( "UserId", user.Id.ToString()),
                    new Claim("Username", user.Username)
                }),

                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)

            };

            var token = tokenHandler.CreateToken(tokenDesc);
            var tokenString = tokenHandler.WriteToken(token);

            _logger.LogWarning( "Basariyla oturum acti" +"+"+loginDto.Username);
            return Ok(tokenString);
            
        }
    }
}
