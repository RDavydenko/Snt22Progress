using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	/// <summary>
	/// Законодательство (ссылка)
	/// </summary>
	public class Legislation : IBaseEntity<int>
	{
		public int id { get; set; }

		public string text { get; set; }

		public string url { get; set; }

		public DateTime created { get; set; }

		public int? creator_id { get; set; }

		public bool is_active { get; set; } = true;
	}
}
