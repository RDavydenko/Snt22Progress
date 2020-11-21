using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	/// <summary>
	/// Файл о должниках
	/// </summary>
	public class DebtorFile : IBaseEntity<int>
	{
		public int id { get; set; }

		public string name { get; set; }

		public string native_name { get; set; }

		public string path { get; set; }

		public int length { get; set; }

		public DateTime uploaded { get; set; }

		public int? uploader_id { get; set; }

		public bool is_active { get; set; } = true;
	}
}
