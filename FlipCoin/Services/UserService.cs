using FlipCoin.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlipCoin.Services
{
	public class UserService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContext;

		public UserService(ApplicationDbContext context, IHttpContextAccessor httpContext)
		{
			_context = context;
			_httpContext = httpContext;
		}

		public async Task<dynamic> GetCurrentUser()
		{
			var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));

			var result = new
			{
				id = user.Id,
				userName = user.UserName
			};

			return result;
		}
	}
}
