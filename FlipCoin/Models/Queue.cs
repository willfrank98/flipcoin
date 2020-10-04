using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
