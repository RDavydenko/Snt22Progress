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

		public string fname { get; set; }

		public string lname { get; set; }

		public string mname { get; set; }

		public int? age { get; set; }

		public string email { get; set; }

		public string address { get; set; }

		public string area_number { get; set; }

		public string password_hash { get; set; }

		public string salt { get; set; }

		public DateTime registered { get; set; }

		public bool is_banned { get; set; } = false;

		public bool is_government_member { get; set; } = false;
	}
}
