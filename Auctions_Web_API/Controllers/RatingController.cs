using Auctions_Web_API.Contracts;
using Auctuons_core.Abstractions;
using Auctuons_core.Infastructure;
using Microsoft.AspNetCore.Mvc;

namespace Auctions_Web_API.Controllers
{
    [ApiController]
    [Route("rating")]
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;
        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }
        [HttpGet("get-ratings/{id}")]
        public async Task<IResult>GetRating(string Id)
        {
            Guid auctionId;
            if (!Guid.TryParse(Id, out auctionId))
                return Results.BadRequest("Invalid auction Id");
            var result= await _ratingService.getRating(auctionId);
            if (result.IsFailure)
                return Results.BadRequest(result.Error);
            var response = result.Value.Select(r => new RatingResponse(r.StudentName, r.Point));
            return Results.Ok(response);
        }
        [HttpPost("create/")]
        public async Task<IResult> CreateRating([FromBody]RatingRequest request)
        {
            if (!ModelState.IsValid)
                return Results.BadRequest(ModelState.Values);
                        
            if (Guid.TryParse(request.StudentId, out Guid StdId) | Guid.TryParse(request.AuctionId, out Guid AucId)){
                var result = await _ratingService.createRating(StdId, AucId, request.Point);
                if (result.IsFailure)
                    return Results.BadRequest(result.Error);
                return Results.Ok();
            }
            else
            {
                return Results.BadRequest(ModelState.Values);   
            }
        }
        [HttpPut("update/{Id}")]
        public async Task<IResult> UpdateRating(string Id,[FromQuery]int point)
        {
            if (!ModelState.IsValid) 
                return Results.BadRequest(ModelState.Values);
            if (Guid.TryParse(Id, out Guid ratingId))
            {
                var result = await _ratingService.updateRating(ratingId, point);
                if (result.IsFailure)
                    return Results.BadRequest(result.Error);
                return Results.Ok();                    
            }
            else return Results.BadRequest("Invalid rating ID");
        }
    }
}
