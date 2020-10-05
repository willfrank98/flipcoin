using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlipCoin.Models
{
	public class ApplicationUser : IdentityUser, IModel
	{
		public dynamic AsResult()
		{
			return new
			{
				id = Id,
				userName = UserName
			};
		}
	}
}
