namespace FlipCoin.Models
{
	public class Challenge
	{
		public int ID { get; set; }

		public ApplicationUser Challenger { get; set; }
		public string ChallengerId { get; set; }

		public ApplicationUser Challengee { get; set; }
		public string ChallengeeId { get; set; }

		public Queue QueueItem { get; set; }
		public int QueueItemId { get; set; }
	}
}
