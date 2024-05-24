using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.Infastructure
{
    public class UserIdentity
    {
        public static (string encryptUserId,string error) Ident(string authorizationHeader)
        {
            string error=string.Empty;
            var stringtoken = authorizationHeader;
            string token=string.Empty;

            try
            {
                token = stringtoken;

                //token = stringtoken.Substring(1, stringtoken.Length - 2);
                Log.Logger.Warning($"Identity {token}");
            }catch(ArgumentOutOfRangeException ex)
            {
                error = "toke is invalid :"+ex.Message;
            }
            if (token == null || string.IsNullOrWhiteSpace(token))
            {
                error = "You are not authentication";
                return ("null", error);
            }
                var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var encryptUserId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            return (encryptUserId!, error!);
        }
    }
}
