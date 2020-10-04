using FlipCoin.Data;
using FlipCoin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FlipCoin.Services
{
	public class QueueService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContext;

		public QueueService(ApplicationDbContext context, IHttpContextAccessor httpContext)
		{
			_context = context;
			_httpContext = httpContext;
		}

		public async Task<List<dynamic>> GetQueue()
		{
			var queueItems = await _context.Queues.Include(x => x.User).ToListAsync();

			var result = new List<dynamic>();

			foreach (var item in queueItems)
			{
				result.Add(new
				{
					id = item.ID,
					userName = item.User.UserName,
					userId = item.User.Id,
					amount = item.Amount
				});
			}

			return result;
		}

		public async Task<dynamic> AddQueueItem(Queue item)
		{
			await _context.AddAsync(item);
			await _context.SaveChangesAsync();

			var result = new
			{
				success = true
			};

			return result;
		}

		public async Task<dynamic> RemoveQueueItem(int id)
		{
			var currentUserId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			var item = await _context.Queues.FirstOrDefaultAsync(x => x.ID == id && x.UserId == currentUserId);

			dynamic result;
			if (item != null)
			{
				_context.Remove(item);
				await _context.SaveChangesAsync();

				result = new
				{
					success = true
				};
			}
			else
			{
				result = new
				{
					success = false
				};
			}
			
			return result;
		}
	}
}
