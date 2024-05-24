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

    public class TaskRepository : ITaskRepository
    {
        private readonly AuctionDbContext _context;
        public TaskRepository(AuctionDbContext context)=>_context=context;
       
        public async Task<Result> Create(ModelTask modeltask,Guid userID, Guid auctionId)
        {
            var auctionEntity = await _context.AuctionEntities.FirstOrDefaultAsync(a => a.Id == auctionId);
            var userEntity = await _context.UserEntities.FirstOrDefaultAsync(u => u.Id == userID);
            if (userEntity==null)
            {
                return Result.Failure("User does not exist");
            }
            var tastEntity = new TaskEntity()
            {
                Id = modeltask.Id,
                Content = modeltask.Content,
                Student = userEntity,
                Auction = auctionEntity,
            };
            try
            {
                await _context.TaskEntities.AddAsync(tastEntity);
                await _context.SaveChangesAsync();                
            }catch (Exception ex) {
                Log.Logger.Error(ex.ToString());
                return Result.Failure(ex.Message);
            }
            return Result.Success();
        }

        public async Task<Result<List<ModelTask>>> GetByAuctionId(Guid id)
        {
            List<TaskEntity> tasklist = new List<TaskEntity>();
            try
            {
                 tasklist = await _context.TaskEntities.Where(t => t.AuctionId == id).Include(t=>t.Student).AsNoTracking().ToListAsync();
            }
            catch(ArgumentException)
            {
                return Result.Failure<List<ModelTask>>("List task is empty");
            }
            catch (Exception ex)
            {
                Log.Logger.Warning(ex.ToString());
                return Result.Failure<List<ModelTask>>(ex.Message);
            }
            var modeltasklist =  tasklist.Select(t => ModelTask.CreateFromDatabase(t.Id, t.Content, t.Student?.UserName ?? "Nonaname",t.StudentId)).ToList();
            return Result.Success(modeltasklist!);
        }

        public Task<Result<ModelTask>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<ModelTask>>> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
