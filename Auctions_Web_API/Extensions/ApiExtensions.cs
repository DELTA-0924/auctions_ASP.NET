using Auctuons_core.Infastructure;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Auctuons_core.Enums;
using Auction.Application.Services;
using Auctuons_core.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Auctions_Web_API.Extensions
{
    public static class ApiExtensions
    {
        public static void AddApiAuthentication(this IServiceCollection services, IOptions<JwtOptions> jwtoptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new() {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions.Value.SecretKey))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["tasty-cookies"];
                            return Task.CompletedTask;
                        }
                    
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Create", policy =>
                policy.Requirements.Add(new PermissionRequirement([Permission.Create])));
            });
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();                                                        
        }
        static public  IEndpointConventionBuilder RequirementPermission<TBuilder>
            (this TBuilder builder,
            params Permission[]permissions)
            where TBuilder : IEndpointConventionBuilder
        {
            return builder.RequireAuthorization(policy =>
            policy.AddRequirements(new PermissionRequirement(permissions))
            );
        }
    }
}
