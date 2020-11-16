using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class UserToChoise : IBaseEntity<int>
	{
		public int id { get; set; }
		
		public int user_id { get; set; }

		public int choise_id { get; set; }
	}
}
