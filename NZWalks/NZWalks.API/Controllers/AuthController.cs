using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositiories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenHandler _tokenHandler;
        private readonly IMapper _autoMapper;
        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler, IMapper autoMapper)
        {
            _userRepository = userRepository;
            _tokenHandler = tokenHandler;
            _autoMapper = autoMapper;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            var user=await _userRepository.AuthenticateAsync(loginRequest.Name,loginRequest.Password);
            if (user!=null)
            {
                //Generate JWT token
               var token= await _tokenHandler.CreateTokenAsync(_autoMapper.Map<Models.DTO.User>(user));
                return Ok(token);
            }
            return BadRequest("User name and password are incorrect");
        }
    }
}
