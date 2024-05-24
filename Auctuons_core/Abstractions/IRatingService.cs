using Auctuons_core.models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auctuons_core.Abstractions
{
    public  interface IRatingService
    {
        Task<Result> createRating(Guid stdId,Guid aucId,int point );
        Task<Result<List<Rating>>> getRating(Guid Id);
        Task<Result> updateRating(Guid Id,int point);
        Task<Result> DeleteRatingById(Guid Id);
    }
}
