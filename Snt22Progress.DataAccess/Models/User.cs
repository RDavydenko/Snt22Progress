using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class User : IBaseEntity<int>
	{
		public int id { get; set; }

		public string name { get; set; }

		public string surname { get; set; }

		public int? age { get; set; }

		public int? weight { get; set; }
	}
}
