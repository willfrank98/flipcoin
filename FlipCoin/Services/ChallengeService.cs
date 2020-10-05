using FlipCoin.Data;
using FlipCoin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FlipCoin.Services
{
	public class ChallengeService
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContext;

		public ChallengeService(ApplicationDbContext context, IHttpContextAccessor httpContext)
		{
			_context = context;
			_httpContext = httpContext;
		}

		public async Task<dynamic> NewChallenge(int queueItemId)
		{
			var queueItem = await _context.Queues.Include(x => x.User).FirstOrDefaultAsync(x => x.ID == queueItemId);

			var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.Id.Equals(userId));

			var challenge = new Challenge
			{
				Challenger = currentUser,
				Challengee = queueItem.User,
				QueueItem = queueItem,
				InProgress = false
			};

			await _context.AddAsync(challenge);
			await _context.SaveChangesAsync();

			var result = new
			{
				success = true
			};

			return result;
		}

		public async Task<dynamic> CheckChallenges()
		{
			var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			var incomingChallenge = await _context.Challenges
				.Include(x => x.Challenger)
				.FirstOrDefaultAsync(x => x.ChallengeeId == userId);

			var outgoingChallenge = await _context.Challenges
				.Include(x => x.Challenger)
				.FirstOrDefaultAsync(x => x.ChallengerId == userId);

			var result = new
			{
				success = true,
				incomingChallenge = incomingChallenge?.AsResult(),
				outgoingChallenge = outgoingChallenge?.AsResult()
			};

			return result;
		}

		public async Task<dynamic> AcceptChallenge(int id)
		{
			// TODO: check user accepting challenge

			var challenge = await _context.Challenges
				//.Include(x => x.Challenger)
				.FirstOrDefaultAsync(x => x.ID == id);

			challenge.InProgress = true;

			_context.Update(challenge);
			await _context.SaveChangesAsync();

			var result = new
			{
				success = true
			};

			return result;
		}
	}
}
