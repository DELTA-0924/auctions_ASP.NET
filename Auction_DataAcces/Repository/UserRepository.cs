using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auctuons_core.models;
using Auction_DataAcces.Entity;
using Microsoft.EntityFrameworkCore;
using Mapster;
using MapsterMapper;
using Auctuons_core.Abstractions;
using Auctuons_core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System.Diagnostics;
using Serilog;
using System.Diagnostics.Eventing.Reader;
using CSharpFunctionalExtensions;
using Azure.Identity;
namespace Auction_DataAcces.Repository
{
    public class UserRepository:IUserRepository
    {
        protected readonly AuctionDbContext _context;
        
        public UserRepository(AuctionDbContext context)=>_context = context;
        public async Task<Result> Create(User user)
        {               
                var userEntity = new UserEntity()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                };
                try
                {
                    await _context.UserEntities.AddAsync(userEntity);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex) { Log.Logger.Warning(ex.ToString());return Result.Failure(ex.Message); }                   
            return Result.Success();

        }

        public async Task SetRole(Guid userId,string role)
        {
            try
            {                                
                var roleEntity = await _context.RoleEntities.FirstOrDefaultAsync(r => r.Id == (role=="admin"?(int)Role.Admin:(int)Role.User));
                //Log.Warning($"Role {roleEntity?.Name ?? "Null"}");
                //Log.Warning($"USer id {userId}");                                
                    var userEntity = await _context.UserEntities.SingleOrDefaultAsync(u => u.Id == userId) ?? throw new InvalidOperationException();
                    userEntity.Roles.Clear();
                    userEntity.Roles.Add(roleEntity!);
                              
                //Log.Warning($"set Role User {userEntity.UserName}");
                //if (userEntity.Roles.Count == 0)
                //    Log.Warning($"Roles is null");
                //else
                //{
                    
                //    Log.Warning($"set role--succes");
                //}
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Logger.Warning(ex.ToString());
            }

        }
        public async Task<Result<User>> GetByEmail(string email)
        {
            
                var userEntity = await _context.UserEntities.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            if (userEntity == null)
                return Result.Failure<User>("User is not find");
                var user = User.Create(userEntity.Id, userEntity.UserName, userEntity.Email, userEntity.PasswordHash);
            if (user.IsFailure)
                return Result.Failure<User>(user.Error);
                return Result.Success<User>(user.Value);
            
       
        }
        public async Task<Result<List<User>>> GetList()
        {
            List<User> users = new List<User>();
            try
            {
                var userEntities = await _context.UserEntities.AsNoTracking().ToListAsync();
                users = userEntities.Select(u => User.Create(u.Id, u.UserName, u.Email, u.PasswordHash).Value).ToList();
                
            }
            catch (ArgumentNullException ex)
            {
                return Result.Failure<List<User>>(ex.Message);
            }
            return Result.Success<List<User>>(users);
        }
        public async Task ChangeStatus(Guid auctionId, Status status)
        {
            var aucitonentity = await _context.AuctionEntities.FirstOrDefaultAsync(a => a.Id == auctionId);
            aucitonentity.Status = status.ToString();
            try
            {
                await _context.SaveChangesAsync();
            }catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<HashSet<Permission>>GetUserPermissions(Guid userId)
        {
            var roles=await _context.UserEntities
                .Include(u=>u.Roles)
                .ThenInclude(r=>r.Permissions)
                .Where(u=>u.Id==userId)
                .Select(u=>u.Roles).
                AsSplitQuery().ToListAsync();
            var role = roles[0];
            if(roles==null)
                Log.Logger.Warning("Roles Null");            
            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();                

        }
        public async Task<Result<User>> getProfile(string userId)
        {
            var userEntity=new UserEntity();
            try
            {
               userEntity= await _context.UserEntities.FirstOrDefaultAsync(u => u.Id.ToString().Equals(userId));
            }
            catch (ArgumentNullException ex)
            {
                return Result.Failure<User>(ex.Message);
            }
            catch(Exception ex) {
                return Result.Failure<User>(ex.Message);
            }
            if(userEntity==null)
                return Result.Failure<User>("Пользователь не найден ");
            var user = User.Create(userEntity.Id, userEntity.UserName, userEntity.Email,userEntity.PasswordHash, userEntity.FirstName,userEntity.SurName,userEntity.Age, userEntity.AboutMe, userEntity.AboutCompany);
            return Result.Success<User>(user.Value);
        }
        public async Task<Result>updateProfile(User user)
        {
            UserEntity userEntity=new UserEntity();
            try
            {
                await _context.UserEntities.Where(u => u.Id == user.Id).ExecuteUpdateAsync(s
                => s.SetProperty(u => u.FirstName, u => user.FirstName).
                SetProperty(u => u.SurName, u => user.SurName).
                SetProperty(u => u.AboutMe, u => user.Aboutme).
                SetProperty(u => u.AboutCompany, u => user.AboutCompany).
                SetProperty(u => u.Age, u => user.Age));
            }catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }            
            return Result.Success(user);
        }
    }
}
