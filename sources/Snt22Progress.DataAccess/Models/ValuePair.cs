using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class ValuePair : IBaseEntity<int>
	{
		public int Id { get; set; }

		public string Key { get; set; }

		public string Value { get; set; }
	}
}
