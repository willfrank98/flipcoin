using Microsoft.AspNetCore.Identity;

namespace FlipCoin.Models
{
	public class ApplicationUser : IdentityUser, IModel
	{
		public int Balance { get; set; }

		public dynamic AsResult()
		{
			return new
			{
				id = Id,
				userName = UserName,
				balance = Balance
			};
		}
	}
}
