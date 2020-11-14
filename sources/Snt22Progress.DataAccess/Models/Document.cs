using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class Document : Infrastructure.IBaseEntity<int>
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public int Length { get; set; }

		public DateTime Created { get; set; }

		public int? Creator_Id { get; set; }

		public DateTime? Edited { get; set; }

		public int? Editor_Id { get; set; }

		public bool Is_Active { get; set; } = true;
	}
}
