using FlipCoin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlipCoin.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{
		//private readonly ApplicationDbContext _context;
		//private IHttpContextAccessor _httpContext;
		private readonly UserService _userService;
		public UserController(UserService userService)
		{
			_userService = userService;
		}

		[HttpGet("get")]
		public async Task<IActionResult> GetCurrentUser()
		{
			var result = await _userService.GetCurrentUser();

			return Json(result);
		}
	}
}
