  using Auctions_Web_API.Contracts;
using Auctuons_core.Abstractions;
using Auctuons_core.Infastructure;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Auctions_Web_API.Controllers
{
    [ApiController]
    [Route("Task")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService=  taskService;
        }
        [HttpPost("Create")]
        public  async Task<IActionResult>Create([FromBody]Taskrequest taskrequest )
        {
            var authorizationHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader == null)
                return BadRequest("No authorization header for identity user");
            (string encryptUserId, string error) = UserIdentity.Ident(authorizationHeader);
            if (!string.IsNullOrEmpty(error))
                return BadRequest(error);
            if (encryptUserId == null)
                return BadRequest("id user is invalid or responce null");
            Guid auctionId;
            Guid userId;
            if (!Guid.TryParse(taskrequest.AuctionId, out auctionId))
                return BadRequest("auction id is invalid");
            if(!Guid.TryParse(encryptUserId, out userId))
                return BadRequest("User id is invalid");
            string content = taskrequest.Content;
            
            var result =await _taskService.CreateTask(content,userId,auctionId);
            if (result.IsFailure)
                return BadRequest(result.Error);
            return Ok();
        }
        [HttpGet("TaskList/{Id}")]
        public async Task<ActionResult>GetTask([FromRoute]string Id)
        {
            Guid auctionId;
            Log.Logger.Warning($"Auction request Id {Id}");
            if (!Guid.TryParse(Id, out auctionId))
                return BadRequest("auction id is invalid");
            var listTaskResult = await _taskService.GetTaskByAuctionId(auctionId);
            if(listTaskResult.IsFailure)
                return BadRequest(listTaskResult.Error);
            var tasks = listTaskResult.Value.Select(t => new TaskResponce(t.Content, t.StudentName??"Unknown",t.StudentId.ToString()!)).ToList();
            return Json(tasks);
        }
    }
}
