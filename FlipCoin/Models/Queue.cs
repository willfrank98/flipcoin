namespace FlipCoin.Models
{
	public class Queue
	{
		public int ID { get; set; }

		public ApplicationUser User { get; set; }
		public string UserId { get; set; }

		public decimal Amount { get; set; }
	}
}
