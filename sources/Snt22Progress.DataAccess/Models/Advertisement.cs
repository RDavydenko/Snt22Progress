using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class Advertisement : IBaseEntity<int>
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Text { get; set; }

		public decimal Price { get; set; }

		public int? Image_File_Id { get; set; }

		public bool Is_Privatizated { get; set; } = false;

		public int Square { get; set; }

		public DateTime Created { get; set; }

		public int? Creator_Id { get; set; }

		public DateTime? Edited { get; set; }

		public int? Editor_Id { get; set; }

		public bool Is_Active { get; set; } = true;
	}
}
