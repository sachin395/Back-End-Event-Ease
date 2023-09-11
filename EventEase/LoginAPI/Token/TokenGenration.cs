using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoginAPI.Token
{
    public class TokenGenration : ITokenGenration
    {
        public string GenrateToken(string email)
        {

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Email,email)
            };
            var secretKey = Encoding.UTF8.GetBytes("fgjfjexrassghfvuyvuygiuygftderszsuyuygyufyut");
            var symmetricsecuritykey = new SymmetricSecurityKey(secretKey);
            var usersigningCredentials = new SigningCredentials(symmetricsecuritykey, SecurityAlgorithms.HmacSha256);
            var UserjswSecurityToken = new JwtSecurityToken(
            issuer: "EventEase",
            audience: "User",
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: usersigningCredentials
                );
            var userSecurityTokenHandler = new JwtSecurityTokenHandler().WriteToken(UserjswSecurityToken);
            string userJwtSecurityTokenHandler = JsonConvert.SerializeObject(new { Token = userSecurityTokenHandler, UserEmail = email });
            return userJwtSecurityTokenHandler;

        }
    }
}
