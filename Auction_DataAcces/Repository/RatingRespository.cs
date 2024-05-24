using Auction_DataAcces.Entity;
using Auctuons_core.Abstractions;
using Auctuons_core.models;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction_DataAcces.Repository
{
    public class RatingRespository : IRatingRepository
    {
        private readonly AuctionDbContext _context;
        public RatingRespository(AuctionDbContext context) {
            _context = context;
        }
        public async Task<Result> Craete(Rating rating)
        {
            try
            {
                var auctionentity = await _context.AuctionEntities.FirstOrDefaultAsync(a => a.Id == rating.AuctionId);
                var userentity = await _context.UserEntities.FirstOrDefaultAsync(u => u.Id == rating.StudentId);
                var ratingEntity = new RatingEntity() {
                    Id = rating.Id,
                    AuctionId = rating.AuctionId,
                    Auction = auctionentity,
                    Point = rating.Point,
                    StudentId = rating.StudentId,
                    Student = userentity
                };
                Log.Logger.Warning("username rating table " + ratingEntity.Student?.UserName??" Null");
                await _context.RatingEntities.AddAsync(ratingEntity);
                await  _context.SaveChangesAsync();
            }catch (Exception ex) {
                Log.Logger.Error(ex.ToString());
                return Result.Failure(ex.Message);
            }
            return Result.Success();
        }

        public async Task<Result> DeleteById(Guid Id)
        {
            try
            {
                await _context.RatingEntities.Where(r=>r.Id==Id).ExecuteDeleteAsync();
            }catch(ArgumentNullException ex)
            {
                Result.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                return Result.Failure(ex.Message);
            }
            return Result.Success();
        }

        public async Task<Result<List<Rating>>> GetRating(Guid auctionId)
        {
            List<RatingEntity> listRatingEntity=new List<RatingEntity>();
            try
            {
                listRatingEntity = await _context.RatingEntities.Where(r=>r.AuctionId==auctionId).Include(r=>r.Student).ToListAsync();
            }catch(ArgumentNullException ex) { 
                Result.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                Result.Failure(ex.Message);
            }            

            var ratinglist = listRatingEntity.Select(r => Rating.CreateFromDataBase(r.Id,r.StudentId, r.AuctionId, r.Point, r.Student?.UserName ?? "Unknown")).ToList(); 
            return Result.Success(ratinglist);
        }

        public async Task<Result> UpdateRating(Guid Id,int point)
        {
            try
            {
                await _context.RatingEntities.Where(r => r.Id == Id).ExecuteUpdateAsync(
                    s => s.SetProperty(r => r.Point, r => point)
                    );
                
            }
            catch (ArgumentNullException ex) { 
                return Result.Failure(ex.Message);
            }
            catch(Exception ex)
            {
                Log.Logger.Error(ex.ToString());
                return Result.Failure(ex.Message);
            }
            return Result.Success();
        }
    }
}
