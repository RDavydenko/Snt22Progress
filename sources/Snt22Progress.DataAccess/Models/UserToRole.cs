using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class UserToRole : IBaseEntity<int>
	{
		public int id { get; set; }

		public int user_id { get; set; }

		public int role_id { get; set; }

		public DateTime created { get; set; }

		public int? creator_id { get; set; }
	}
}
