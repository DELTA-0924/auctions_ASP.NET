using Auctuons_core.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Auctions_Web_API.Contracts;
using Auctuons_core.models;
using Auction.Application.Services;
using CSharpFunctionalExtensions;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using Auctuons_core.Infastructure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
namespace Auctions_Web_API.Controllers
{
    [ApiController]
    [Route("{controller=Auction}")]
    public class AuctionController : Controller
    {
        private string _staticFilePath = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles/Image");
        private readonly IAuctionService _auctionService;
        private readonly ImageService _imageServices;
        public AuctionController(IAuctionService auctionService, ImageService imageService)
        {
            _auctionService = auctionService;
            _imageServices = imageService;
        }
        [HttpGet, Route("{action=GetAuctions}")]
        public async Task<ActionResult<List<AuctionResponce>>> GetAuctions()
        {
            var auctionResults = await _auctionService.GetAllAuctions();
            if(auctionResults.IsFailure)
                return BadRequest(auctionResults.Error);
            var responce = auctionResults.Value.Select(a => new AuctionResponce(a.Id, a.TitleName, a.Description, a.Created, a.Finished,a._status.ToString(),a.CreatorId,a.Creator)).ToList();
            return Ok(responce);
        }
        [Authorize(Policy = "Create")]
        [HttpGet("create")]
        public IActionResult CreateAuctions()
        {
            return Ok();
        }
        [Authorize(Policy = "Create")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAuctions([FromBody] AuctionRequest auctionRequest)
        {
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader == null)
                return BadRequest("No authorization header for identity user");
            (string encryptUserId, string error) = UserIdentity.Ident(authorizationHeader);
            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);
            if (encryptUserId == null)
                return BadRequest("id user is invalid or responce null");
            if (auctionRequest == null)
            {
                return BadRequest();
            }
            if (!DateTime.TryParse(auctionRequest.Finished, out DateTime datefinished))
                return BadRequest();

            var auctionResult = Auctions.Create(new Guid(), auctionRequest.Title, auctionRequest.Description, datefinished);
            if (auctionResult.IsFailure)
            {
                return BadRequest(auctionResult.Error);
            }
            Log.Logger.Warning(auctionResult.Value.Finished.ToString());
            var ResultDb = await _auctionService.CreateAuction(auctionResult.Value, encryptUserId);
            if (ResultDb.IsFailure)
                return BadRequest(ResultDb.Error);
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult>Delete([FromRoute]string id)
        {
            Guid auctionId;
            if (!Guid.TryParse(id, out auctionId))
                return BadRequest("Invalid Id");
            var result = await _auctionService.DeleteAuction(auctionId); 
            if(result.IsFailure)
                return BadRequest(result.Error);
            return Ok();
        }
        [HttpPut("change-status/{Id}/{status:int}")]
        public async Task<ActionResult> StatusChange([FromRoute]string Id,int status)
        {
            Guid auctionId;
            if(!Guid.TryParse(Id,out auctionId))
                return BadRequest("Invalid auction Id");
            var result=await _auctionService.ChangeStatusAuction(auctionId,status);
            if(result.IsFailure)
                return BadRequest(result.Error);
            return Ok();
        }
        [HttpGet("auction-detail/{Id}")]
        public async Task<ActionResult> GetAuctionDetail([FromRoute]string Id)
        {
            Guid auctionId;
            if (!Guid.TryParse(Id, out auctionId))
                return BadRequest("Invalid auction Id");
            var auctionresult=await _auctionService.getAuctionDetail(auctionId);
            if (auctionresult.IsFailure)
                return BadRequest($"ошибка при запросе аукциона:{auctionresult.Error}");
            var a = auctionresult.Value;
            var responce =  new AuctionResponce(a.Id, a.TitleName, a.Description, a.Created, a.Finished, a._status.ToString(), a.CreatorId, a.Creator);
            return Ok(responce);
        }
    }
}
