using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Models.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalks.API.Repositiories
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {

            //claim represents identity of the user
            //claim is an attribute of identity that can define the permissions
            //Create user claims for the token
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));

            //Iterating roles
            user.Roles.ForEach(role =>
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            });

            //fetching the secret key from appsettings
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            //creating credential using secret keya nd hmac algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //generating signed token
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            var newToken = new JwtSecurityTokenHandler().WriteToken(token);
           return Task.FromResult(newToken);
        }
    }
}
