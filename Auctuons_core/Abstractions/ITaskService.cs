using Auctuons_core.models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.Abstractions
{
    public interface ITaskService
    {
        Task<Result> CreateTask(string content, Guid userId,Guid auctionId);
        Task<Result<List<ModelTask>>> GetTaskByAuctionId(Guid id);

    }
}
