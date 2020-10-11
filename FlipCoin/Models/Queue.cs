namespace FlipCoin.Models
{
	public class Queue : IModel
	{
		public int ID { get; set; }

		public ApplicationUser User { get; set; }
		public string UserId { get; set; }

		public int Amount { get; set; }

		public dynamic AsResult()
		{
			return new
			{
				id = ID,
				userName = User.UserName,
				userId = User.Id,
				amount = Amount
			};
		}
	}
}
