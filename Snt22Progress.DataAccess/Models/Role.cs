using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class Role: IBaseEntity<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DateTime Created { get; set; }

		public int? Creator_Id { get; set; }
	}
}
