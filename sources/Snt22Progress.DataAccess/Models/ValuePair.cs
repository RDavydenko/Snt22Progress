using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class ValuePair : IBaseEntity<int>
	{
		public int id { get; set; }

		public string key { get; set; }

		public string value { get; set; }
	}
}
