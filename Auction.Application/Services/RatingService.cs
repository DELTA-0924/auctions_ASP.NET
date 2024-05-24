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
    public  class RatingService:IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository) {
            _ratingRepository = ratingRepository;
        }

        public async Task<Result> createRating(Guid stdId, Guid aucId, int point)
        {
            var M_Rating_res = Rating.Create(stdId, aucId, point);
            if (M_Rating_res.IsFailure)
                return Result.Failure(M_Rating_res.Error);
            var rating_res=await _ratingRepository.Craete(M_Rating_res.Value);
            if (rating_res.IsFailure)
                return Result.Failure(rating_res.Error);
            return Result.Success();
        }

        public async Task<Result> DeleteRatingById(Guid Id)
        {
            var result = await _ratingRepository.DeleteById(Id);
            if (result.IsFailure)
                return Result.Failure(result.Error);
            return Result.Success();
        }

        public async Task<Result<List<Rating>>> getRating(Guid Id)
        {
            var list_rating_res = await _ratingRepository.GetRating(Id);
            if (list_rating_res.IsFailure)
                return Result.Failure<List<Rating>>(list_rating_res.Error);
            return Result.Success(list_rating_res.Value);
        }

        public async Task<Result> updateRating(Guid Id, int point)
        {
            var result = await _ratingRepository.UpdateRating(Id,point);
            if (result.IsFailure)
                return Result.Failure(result.Error);
            return Result.Success();
        }
    }
}
