using Auction.Application.Services;
using Auctions_Web_API.Contracts;
using Auctions_Web_API.Extensions;
using Auctuons_core.models;
using Azure;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Auctuons_core.Enums;
using Auctuons_core.Infastructure;
using Azure.Core;
namespace Auctions_Web_API.Endpoints
{
    public static class User_Auth_Endpoins
    {
        public static void Auth_userConfigureEndpoints(this IEndpointRouteBuilder endpoins)
        {
            endpoins.MapPost("Register/{role}",Register);
            endpoins.MapPost("Login",Login);
            endpoins.MapGet("Users/", GetAllUsers).RequirementPermission([Permission.Delete]);
            endpoins.MapGet("Profile/", GetProfile).RequirementPermission([Permission.Read]);
            endpoins.MapPost("set-Profile/", SetProfile).RequirementPermission([Permission.Read]);
        }
        private static  async Task<IResult> Register(string role,HttpContext context,IUserService userservice)
        {
             var request = context.Request;            
            try
            {
                var userRequest = await request.ReadFromJsonAsync<UserRequest>();
                if (userRequest == null)
                {
                    Log.Logger.Warning("User == null");
                    return Results.BadRequest();
                }                                
                var Result=await userservice.Register(userRequest.Username!, userRequest.Email!, userRequest.Password!,role);
                if (Result.IsFailure)
                    throw new Exception(Result.Error);
            }catch (Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                return Results.BadRequest(ex.Message);
            }
        
            return Results.Ok();
        }
        private static async Task<IResult> Login(HttpContext context,IUserService userService)
        {
            var request = context.Request;
            
            var headers = context.Request.Headers.ToList();
            if (headers.Count == 0)
                Log.Logger.Warning("Headers is null");
            headers.ForEach(item =>
            {
                Log.Logger.Warning(item.Value!);
            });
            string token = string.Empty;
            string[] array = [];
            HashSet < Permission >permission= new HashSet<Permission>();
            try
            {
                var userRequest = await request.ReadFromJsonAsync<UserloginRequest>();
                if (userRequest == null)
                {
                    Log.Logger.Information("User null");
                    return Results.BadRequest();
                }
                var result = await userService.Login(userRequest.Email, userRequest.Password);
                if (result.IsFailure)
                    return Results.BadRequest(result.Error);
                (token, permission) = result.Value;
                Log.Logger.Warning($"{token}");
                array=permission.Select(p=>p.ToString()).ToArray();
                
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex.ToString()); 
                return Results.BadRequest();
            }
            var responce = new LoginResponse(token, array);
            return Results.Ok(responce);
        }
        private static async Task<IResult>GetAllUsers( HttpContext context,IUserService userservice)
        {            
            var headers=context.Request.Headers.ToList();
            
            if(headers.Count==0)
                Log.Logger.Warning("Headers is null");
            headers.ForEach(item =>
            {
                Log.Logger.Warning(item.Value!);
            });
            var users = await userservice.GetListUsers();
            var usersResponce = users.Value.Select(u => u.Adapt<UserResponce>()).ToList();
            return Results.Ok(usersResponce);
        }
        private static async Task<IResult> GetProfile(HttpContext context,IUserService userservice)
        {
            var headers = context.Request.Headers.ToList();

            if (headers.Count == 0)
                Log.Logger.Warning("Headers is null");
            headers.ForEach(item =>
            {
                Log.Logger.Warning(item.Value!);
            });
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();            
            if (authorizationHeader == null)
                return Results.BadRequest("No authorization header for identity user");
            (string encryptUserId, string error) = UserIdentity.Ident(authorizationHeader);
            if (!string.IsNullOrEmpty(error))
                return Results.BadRequest(error);
            if (encryptUserId == null)
                return Results.BadRequest("id user is invalid or responce null");
            var user=await userservice.GetProfile(encryptUserId);
            var userResponce = new UserProfileResponse(user.Value.UserName, user.Value.Email, user.Value.FirstName,
                                                        user.Value.SurName,user.Value.Age, user.Value.Aboutme, user.Value.AboutCompany);

            return Results.Ok(userResponce);
        }
        private static async Task<IResult>SetProfile(HttpContext context,IUserService userservice)
        {
            var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader == null)
                return Results.BadRequest("No authorization header for identity user");
            (string encryptUserId, string error) = UserIdentity.Ident(authorizationHeader);
            if (!string.IsNullOrEmpty(error))
                return Results.BadRequest(error);
            var request = context.Request;
            var userProfile = await request.ReadFromJsonAsync<UserProfileRequest>();

            var userreq = User.CreateProfile(Guid.Parse(encryptUserId), userProfile.Username, userProfile.Firstname, userProfile.Surname, 
                                                        userProfile.age, userProfile.Aboutme, userProfile.Company);
            var userResult =await userservice.UpdateProfile(userreq.Value);
            if (userResult.IsFailure)
                return Results.BadRequest(userResult.Error);
            var user = await userservice.GetProfile(encryptUserId);
            var userResponce = new UserProfileResponse(user.Value.UserName, user.Value.Email, user.Value.FirstName,
                                                        user.Value.SurName, user.Value.Age, user.Value.Aboutme, user.Value.AboutCompany);
            return Results.Ok(userResponce);
        }
    }
}
