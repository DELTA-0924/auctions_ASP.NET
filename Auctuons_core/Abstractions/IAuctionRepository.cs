using Auctuons_core.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Auctuons_core.Enums;
namespace Auctuons_core.Abstractions
{
    public interface IAuctionRepository
    {
         Task<Result<List<Auctions>>> GetList();
         Task<Result> Create(Auctions auctions,string userId);
         Task<Result> Update(Guid id, string titleName, string desciption, DateTime created, DateTime finished);
         Task<Result>DeleteById(Guid id);
         Task<Result> ChangeStatus(Guid id, Status status);
        Task<Result<Auctions>> AuctionDetail(Guid id);
    }
}
