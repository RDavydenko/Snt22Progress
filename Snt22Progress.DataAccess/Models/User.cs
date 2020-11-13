using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class User : IBaseEntity<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public int? Age { get; set; }

		public int? Weight { get; set; }
	}
}
