using FlipCoin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FlipCoin.Services;
using Microsoft.AspNetCore.Authorization;

namespace FlipCoin.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class QueueController : Controller
	{
		private readonly QueueService _queueService;

		public QueueController(QueueService queueService)
		{
			_queueService = queueService;
		}

		[HttpGet("get")]
		public async Task<IActionResult> GetQueue()
		{
			var result = await _queueService.GetQueue();

			return Json(result);
		}

		[Authorize]
		[HttpPost("add")]
		public async Task<IActionResult> AddQueueItem([Bind("UserId,Amount")] Queue item)
		{
			if (ModelState.IsValid)
			{
				var result = await _queueService.AddQueueItem(item);
				return Json(result);
			}
			else
			{
				return StatusCode(500);
			}
		}

		[Authorize]
		[HttpGet("remove/{queueItemId:int}")]
		public async Task<IActionResult> RemoveQueueItem(int queueItemId)
		{
			var result = await _queueService.RemoveQueueItem(queueItemId);

			return Json(result);
		}
	}
}
