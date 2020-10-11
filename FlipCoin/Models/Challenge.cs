namespace FlipCoin.Models
{
	public class Challenge : IModel
	{
		public int ID { get; set; }

		public ApplicationUser Challenger { get; set; }
		public string ChallengerId { get; set; }

		public ApplicationUser Challengee { get; set; }
		public string ChallengeeId { get; set; }

		public Queue QueueItem { get; set; }
		public int QueueItemId { get; set; }

		public bool InProgress { get; set; }

		public double? Result { get; set; }

		public bool Seen { get; set; }

		public dynamic AsResult()
		{
			return new
			{
				id = ID,
				challenger = Challenger.AsResult(),
				inProgress = InProgress,
				result = Result
			};
		}
	}
}
