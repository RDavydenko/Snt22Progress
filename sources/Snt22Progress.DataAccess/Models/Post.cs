using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	/// <summary>
	/// Пост (запись)
	/// </summary>
	public class Post : Infrastructure.IBaseEntity<int>
	{
		public int id { get; set; }

		public string title { get; set; }

		public string text { get; set; }

		public DateTime created { get; set; }

		public int? creator_id { get; set; }

		public DateTime? edited { get; set; }
		
		public int? editor_id { get; set; }

		public bool is_active { get; set; } = true;
	}
}
