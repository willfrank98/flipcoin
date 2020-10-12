using FlipCoin.Data;
using FlipCoin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
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

			// create challenge
			var challenge = new Challenge
			{
				Challenger = currentUser,
				Challengee = queueItem.User,
				QueueItem = queueItem,
				InProgress = true
			};

			await _context.AddAsync(challenge);

			// adjust balance
			currentUser.Balance -= queueItem.Amount;
			_context.Update(currentUser);

			await _context.SaveChangesAsync();

			var result = new
			{
				success = true,
				challenge = challenge.AsResult()
			};

			return result;
		}

		public async Task<dynamic> CheckChallenges()
		{
			var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			var incomingChallenge = await _context.Challenges
				.Include(x => x.Challenger)
				.FirstOrDefaultAsync(x => x.ChallengeeId == userId && x.InProgress);

			var outgoingChallenge = await _context.Challenges
				.Include(x => x.Challenger)
				.FirstOrDefaultAsync(x => x.ChallengerId == userId && !x.Seen);

			if (outgoingChallenge != null && outgoingChallenge.Result != null)
			{
				outgoingChallenge.Seen = true;
				_context.Update(outgoingChallenge);
				await _context.SaveChangesAsync();
			}

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
			var challenge = await _context.Challenges
				.Include(x => x.Challenger)
				.Include(x => x.Challengee)
				.Include(x => x.QueueItem)
				.FirstOrDefaultAsync(x => x.ID == id);

			// TODO: check user accepting challenge
			var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			if (challenge.ChallengeeId != userId)
			{
				return new
				{
					success = false
				};
			}

			// determine result, close challenge
			var rand = new Random();
			double challengeResult = 0.0;
			while (challengeResult == 0.0)
			{
				challengeResult = rand.NextDouble();
			}

			challenge.Result = challengeResult;
			challenge.InProgress = false;

			_context.Update(challenge);

			// update balances
			var amount = challenge.QueueItem.Amount;

			if (challengeResult > 0.5)
			{
				challenge.Challenger.Balance += amount * 2;
			}
			else
			{
				challenge.Challengee.Balance += amount * 2;
			}

			_context.Update(challenge.Challenger);
			_context.Update(challenge.Challengee);

			// remove associated queue item
			var item = await _context.Queues
				.FirstOrDefaultAsync(x => x.UserId == challenge.ChallengeeId);

			_context.Remove(item);

			// save changes
			await _context.SaveChangesAsync();

			var result = new
			{
				success = true,
				challenge = challenge.AsResult()
			};

			return result;
		}
	}
}
