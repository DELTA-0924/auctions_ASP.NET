using Auctuons_core.Abstractions;
using Auctuons_core.models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository) {
            _taskRepository = taskRepository;
        }
        public async Task<Result> CreateTask(string content, Guid userId, Guid auctionId)
        {
            var modelTaskResult = ModelTask.CreateModelTask(content);
            if (modelTaskResult.IsFailure)
                return Result.Failure(modelTaskResult.Error);
            var taskResult=await _taskRepository.Create(modelTaskResult.Value,userId,auctionId);
            if (taskResult.IsFailure)
                return Result.Failure(taskResult.Error);
            return Result.Success();
        }

        public async Task<Result<List<ModelTask>>> GetTaskByAuctionId(Guid id)
        {
            var tasklistResult = await _taskRepository.GetByAuctionId(id);
            if (tasklistResult.IsFailure)
                return Result.Failure<List<ModelTask>>(tasklistResult.Error);
            return Result.Success(tasklistResult.Value);
        }
    }
}
