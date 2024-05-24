using Auctuons_core.Abstractions;
using Auctuons_core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Auctuons_core.Enums;
using CSharpFunctionalExtensions;
namespace Auction.Application.Services
{
    public class UserService:IUserService
    {
        protected readonly IUserRepository _userRepository;
        protected readonly IPasswordHasher _passwordHasher;
        protected readonly ITokenProvider _tokenProvider;
        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenProvider tokenProvider)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _tokenProvider = tokenProvider;
        }   
        public async Task<Result> Register(string userName,string email,string password,string role)
        {            
            var hashedPassword = _passwordHasher.Generate(password);
            var user = User.Create(Guid.NewGuid(), userName, email, hashedPassword);
            if (user.IsFailure)
                return Result.Failure(user.Error);
            var resultCreate =await _userRepository.Create(user.Value);

            await _userRepository.SetRole(user.Value.Id,role);                                   
            return resultCreate;
        }
        public async Task<Result<(string,HashSet<Permission>)>> Login(string email,string password)
        {
            
            var Resultuser = await _userRepository.GetByEmail(email);
            if (Resultuser.IsFailure)
            {
                return Result.Failure<(string,HashSet<Permission>)>("Пользователь не найден");
            }
            var user = Resultuser.Value;
            var result = _passwordHasher.Verify(password, user.PasswordHash);
            if (!result)
            {
                Log.Logger.Warning($"Faile verify password");
                return Result.Failure<(string, HashSet<Permission>)>("Неверный пароль");
            }
            var permissions = await _userRepository.GetUserPermissions(user.Id);
            var token = _tokenProvider.GenerateToken(user);            
            return Result.Success<(string, HashSet<Permission>)>((token,permissions));
        }
        public async Task<Result<List<User>>> GetListUsers()
        {
            var users = await _userRepository.GetList();
            return users.Value;
        }
        public async Task<Result<User>> GetProfile(string userId) 
        {
            var user = await _userRepository.getProfile(userId);
            if (user.IsFailure)
                return Result.Failure<User>(user.Error);
            return Result.Success<User>(user.Value);
        }
        public async Task<Result>UpdateProfile(User user)     {
            var userResult=await _userRepository.updateProfile(user);
            if(userResult.IsFailure)
                return Result.Failure<User>(userResult.Error);
            return Result.Success();
        }
    }
}
