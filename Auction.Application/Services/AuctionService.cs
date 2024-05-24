using Auctuons_core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auctuons_core.models;
using CSharpFunctionalExtensions;
using Auctuons_core.Enums;
using System.Net.NetworkInformation;
namespace Auction.Application.Services
{
    public class AuctionService: IAuctionService
    {
        private readonly IAuctionRepository _repository;
        public AuctionService(IAuctionRepository repository)
        {
            _repository = repository;
        }
        public async Task<Result<List<Auctions>>> GetAllAuctions()
        {
            return await _repository.GetList();
        }
        public async Task<Result> CreateAuction(Auctions auction, string userId)
        {
            return await _repository.Create(auction,userId);
        }
        public async Task<Result> UpdateAuction(Guid id, string titelName, string descriptions, DateTime created, DateTime finished)
        {
            return await _repository.Update(id, titelName, descriptions, created, finished);
        }
        public async Task <Result>DeleteAuction(Guid id) {
            return await _repository.DeleteById(id);
        }
        public async Task<Result> ChangeStatusAuction(Guid id,int status)
        {
            Status parcedStatus;
            if (!Enum.TryParse<Status>(status.ToString(), out parcedStatus))
                return Result.Failure("Invalid status");
            return await _repository.ChangeStatus(id, parcedStatus);
        }
        public async Task<Result<Auctions>> getAuctionDetail(Guid Id)
        {

            return await _repository.AuctionDetail(Id);
        }
    }
}
