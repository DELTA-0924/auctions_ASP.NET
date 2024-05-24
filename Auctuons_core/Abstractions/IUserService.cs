using System.Text;
using Auctuons_core.Enums;
using Auctuons_core.models;
using CSharpFunctionalExtensions;
namespace Auction.Application.Services
{
    public interface IUserService
    {
        Task <Result>Register(string usernName,string email,string password,string role);
        Task <Result<(string, HashSet<Permission>)>>Login(string email, string password);
        Task<Result<List<User>>> GetListUsers();
        Task<Result<User>> GetProfile(string userId);
        Task<Result> UpdateProfile(User user);
    }
}