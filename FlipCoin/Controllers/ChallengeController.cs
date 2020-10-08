using FlipCoin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FlipCoin.Services;
using Microsoft.AspNetCore.Authorization;

namespace FlipCoin.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ChallengeController : Controller
	{
		private readonly ChallengeService _challengeService;

		public ChallengeController(ChallengeService challengeService)
		{
			_challengeService = challengeService;
		}

		[Authorize]
		[HttpGet("add/{queueItemId:int}")]
		public async Task<IActionResult> NewChallenge(int queueItemId)
		{
			var result = await _challengeService.NewChallenge(queueItemId);

			return Json(result);
		}

		[Authorize]
		[HttpGet("check")]
		public async Task<IActionResult> CheckChallenges()
		{
			var result = await _challengeService.CheckChallenges();

			return Json(result);
		}

		[Authorize]
		[HttpGet("accept/{challengeId:int}")]
		public async Task<IActionResult> AcceptChallenge(int challengeId)
		{
			var result = await _challengeService.AcceptChallenge(challengeId);

			return Json(result);
		}
	}
}
