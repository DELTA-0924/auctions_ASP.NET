using Auctuons_core.Enums;
using Auctuons_core.models;
using CSharpFunctionalExtensions;
namespace Auctuons_core.Abstractions
{
    public interface IUserRepository
    {
        Task <Result>Create(User user);
        Task <Result<User>> GetByEmail(string email);
        Task<Result<List<User>>> GetList();
        Task<HashSet<Permission>>GetUserPermissions(Guid userId);
        Task SetRole(Guid userId,string role);
        Task ChangeStatus(Guid auctionId, Status status);
        Task<Result<User>> getProfile(string Guid);
        Task<Result> updateProfile(User user);

    }
}