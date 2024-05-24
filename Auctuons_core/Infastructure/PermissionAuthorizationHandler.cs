using Auctuons_core.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.Infastructure
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context
            ,PermissionRequirement requirement)
        {

            var httpContext = context.Resource as HttpContext;
            if (httpContext == null)
            {
                Log.Logger.Error("Обработка случая, когда ресурс не является HttpContext");
                return;
            }

            // Извлекаем значение заголовка Authorization
            var authorizationHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                Log.Logger.Error("Обработка случая, когда заголовок Authorization отсутствует");
                return;
            }

            // Извлекаем токен из заголовка Authorization (обычно токен имеет формат "Bearer {token}")
            var stringtoken = authorizationHeader;
            string token=stringtoken;
            Log.Logger.Warning("Header string token -- " + stringtoken);
            //if (stringtoken[0] == '"')
            //{
            //  token = stringtoken.Substring(1, stringtoken.Length - 2);
            //}
            //else{
            //    token=stringtoken;
            //}
            if (string.IsNullOrEmpty(token))
            {
                Log.Logger.Error("Обработка случая, когда токен отсутствует");
                token = httpContext.Request.Cookies["tasty-cookies"];
                Log.Logger.Error($"cookies {token??"Null cookies"}");
            }

            //Log.Logger.Warning("token -- " + token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            //Log.Logger.Warning("JWTtoken -- " + jwtToken);

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;      
            if (userId == null || !Guid.TryParse(userId,out var id)) {
                Log.Logger.Error("UseeId in ccokies null");
                return;
            }
            using var scope=_serviceScopeFactory.CreateScope();
            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
            var permissions = await permissionService.GetPermissionsAsync(id);
            var array=permissions.ToArray();
            //Log.Logger.Warning($"Requirment {requirement.Permissions[0]}");
            for(int i=0;i<array.Length;i++)
              //  Log.Logger.Warning($"Your perrmission {array[i]}");
            if(permissions.Intersect(requirement.Permissions).Any())
            {            
                context.Succeed(requirement);
            }
        }
    }
}
