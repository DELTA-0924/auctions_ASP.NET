using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auctuons_core.models;
using CSharpFunctionalExtensions;
namespace Auctuons_core.Abstractions
{
    public  interface ITaskRepository
    {
        Task <Result<List<ModelTask>>> GetList();
        Task<Result> Create(ModelTask modelTask,Guid userId,Guid auctionId);
        Task<Result<ModelTask>>GetById(Guid id);
        Task<Result<List<ModelTask>>> GetByAuctionId(Guid id);
    }
}
