using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auctuons_core.models;
using Auction_DataAcces;
using Microsoft.EntityFrameworkCore;
using Auction_DataAcces.Entity;
using Auctuons_core.Abstractions;
using System.Diagnostics;
using CSharpFunctionalExtensions;
using Serilog;
using Auctuons_core.Enums;
using System.Runtime.CompilerServices;
namespace Auction_DataAcces.Repository
{
    public class AuctionRepository: IAuctionRepository
    {
        private readonly AuctionDbContext _context;
        public AuctionRepository(AuctionDbContext context)=>_context=context;
       public async Task<Result<List<Auctions>>> GetList()
        {
            List<Auctions>auctions=new List<Auctions>();
            try
            {
                var auctionsentities = await _context.AuctionEntities.Include(a=>a.Creator).AsNoTracking().ToListAsync();
                auctions = auctionsentities.Select(a => Auctions.CreateFromDataBase(a.Id, a.TitleName, a.Description, a.Created, a.Finished,
                    Enum.Parse<Status>(a.Status),a.CreatorId,a.Creator?.UserName??"Unknown")).ToList();
            }catch(Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                return Result.Failure<List<Auctions>>(ex.Message);
            }
            return Result.Success(auctions);
        }
        public async Task<Result> Create(Auctions auction, string userId)
        {
            Result result=new Result();
            var user=await _context.UserEntities.FirstOrDefaultAsync(u=>u.Id.ToString()==userId);
            var auctionEntity = new AuctionEntity()
            {
                Id = auction.Id,
                TitleName = auction.TitleName,
                Description = auction.Description,
                Created = auction.Created,
                Finished = auction.Finished,
                CreatorId = user.Id,
                Status = auction._status.ToString()
                };
            auctionEntity.Creator = user ;
            Log.Logger.Warning($"Auction name -- {auctionEntity.TitleName}");
            try
            {
                await _context.AuctionEntities.AddAsync(auctionEntity);
                await _context.SaveChangesAsync();
            }catch(Exception ex){
                Log.Logger.Error(ex.ToString());
                result = Result.Failure(ex.Message);
            }
            return result;
        }
         public   async Task<Result >Update(Guid id,string titleName,string desciption,DateTime created,DateTime finished)
                {
                    Result result=new Result();
                    try
                    {
                        await _context.AuctionEntities
                                .Where(a => a.Id == id)
                                .ExecuteUpdateAsync(s => s
                                                .SetProperty(a => a.TitleName, a => titleName)
                                                .SetProperty(a => a.Description, a => desciption)
                                                .SetProperty(a => a.Finished, a => finished)
                                                .SetProperty(a => a.Created, a => created));
                    }catch(Exception ex)
                    {
                        Log.Logger.Error(ex.ToString());
                        result= Result.Failure(ex.Message);
                    }
                        return result;
                }
        public async Task<Result> DeleteById(Guid id)
        {
            Result result=new Result();
            try
            {
                await _context.AuctionEntities.Where(a => a.Id == id).ExecuteDeleteAsync();
            }catch(Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                result= Result.Failure(ex.Message);
            }
            return result;
        }
        public async Task<Result> ChangeStatus(Guid id,Status status)
        {
            try
            {
                await _context.AuctionEntities.Where(a => a.Id == id).ExecuteUpdateAsync(
                    s=>s.SetProperty(a=>a.Status,a=>Enum.GetName(typeof(Status),status))
                    );
            }
            catch (ArgumentNullException ex)
            {
                Log.Logger.Error(ex.ToString());
                return Result.Failure(ex.Message);
            }catch(Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                return Result.Failure(ex.Message);
            }
            return Result.Success();
        }
        public async Task<Result<Auctions>>AuctionDetail(Guid Id)
        {

            Auctions auctions;
            try
            {
                var a = await _context.AuctionEntities.Include(a => a.Creator).AsNoTracking().FirstOrDefaultAsync(a=>a.Id==Id);
                Log.Logger.Warning($"Auction name {a.TitleName}");
                Log.Logger.Warning($"creator name {a.Creator.UserName}");
                Log.Logger.Warning($"Auction ID {a.Id}");
                auctions = Auctions.CreateFromDataBase(a.Id, a.TitleName, a.Description, a.Created, a.Finished,
                    Enum.Parse<Status>(a.Status), a.CreatorId, a.Creator?.UserName ?? "Unknown");
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                return Result.Failure<Auctions>(ex.Message);
            }
            return Result.Success(auctions);
        }
    }
}
