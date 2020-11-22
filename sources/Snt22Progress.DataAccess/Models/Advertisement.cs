using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class Advertisement : IBaseEntity<int>
	{
		public int id { get; set; }

		public string title { get; set; }

		public string text { get; set; }

		public decimal price { get; set; }

		public int? image_file_id { get; set; }

		public bool is_privatizated { get; set; } = false;

		public int square { get; set; }

		public DateTime created { get; set; }

		public int? creator_id { get; set; }

		public bool is_active { get; set; } = true;
	}
}
