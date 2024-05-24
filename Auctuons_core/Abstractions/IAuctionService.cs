using Auctuons_core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
namespace Auctuons_core.Abstractions
{
    public interface IAuctionService
    {
        Task<Result<List<Auctions>>> GetAllAuctions();
        Task<Result> CreateAuction(Auctions auction,string userId);
        Task<Result> UpdateAuction(Guid id, string titelName, string descriptions, DateTime created, DateTime finished);
        Task<Result> DeleteAuction(Guid id);
        Task<Result> ChangeStatusAuction(Guid Id, int status);
        Task<Result<Auctions>> getAuctionDetail(Guid Id);

    }
}
