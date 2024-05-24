//using Auction.Application.Services;
//using Microsoft.AspNetCore.Mvc;
//using Auctions_Web_API.Contracts;
//using Mapster;

//using System.Diagnostics;
//using Microsoft.VisualBasic;
//using Microsoft.AspNetCore.Authorization;
//using Auctuons_core.models;
//namespace Auctions_Web_API.Controllers
//{

//    [ApiController]
//    [Route("{controller}")]
//    public class UserController : Controller
//    {
//        protected IUserService _userService;
//        protected readonly ILogger _logger;
//        public UserController(IUserService userService, ILogger<UserController> logger) { _userService = userService; _logger = logger; }
//        [HttpGet, Route("{action=userset}")]
//        public IActionResult UserSet()
//        {
//            return View();
//        }
     
//        [HttpGet("SetRole")]
//        public async Task<IActionResult> SetRole(Guid userId)
//        {
//            return Ok("UserGet");
//        }

//        [HttpGet("UserGet")]
//        public async Task<IActionResult> UserGet()
//        {
//            var users = await _userService.GetListUsers();
//            var usersResponce = users.Select(u => u.Adapt<UserResponce>()).ToList();
//            return View(usersResponce);
//        }

//        [HttpGet("Login")]
//        public IActionResult UserLogin()
//        {
//            return View();
//        }
//        [HttpPost("Login")]
//        public async Task<IActionResult> UserLoginPost()
//        {
//            var token = await _userService.Login(Request.Form["Email"]!, Request.Form["Password"]!);
//            Response.Cookies.Append("tasty-cookies", token);
//            return RedirectToAction("UserGet");
//        }
//    }
//}
