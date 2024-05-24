using Auctuons_core.models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auctuons_core.Abstractions;
namespace Auctuons_core.Infastructure
{
    public class JwtProvider: ITokenProvider
    {
        protected readonly JwtOptions _options;
        public JwtProvider(IOptions<JwtOptions> options) => _options = options.Value;
        public string GenerateToken(User user)
        {
            Claim[] claims = [
                new("userId", user.Id.ToString()),                
                ]; 
            var signinCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)), SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signinCredentials,
                expires: DateTime.UtcNow.AddDays(_options.ExpiresHours)); 
            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
