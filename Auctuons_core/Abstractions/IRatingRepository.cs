using Auctuons_core.models;
using CSharpFunctionalExtensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.Abstractions
{
    public interface IRatingRepository
    {
        Task<Result>Craete(Rating rating);
        Task<Result<List<Rating>>> GetRating(Guid auctionId);
        Task <Result>UpdateRating(Guid Id,int point);
        Task <Result>DeleteById(Guid Id);
    }
}
