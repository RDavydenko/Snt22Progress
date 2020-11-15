using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class User : IBaseEntity<int>
	{
		public int Id { get; set; }

		public string FName { get; set; }

		public string SName { get; set; }

		public string MName { get; set; }

		public int? Age { get; set; }

		public string Email { get; set; }

		public string Address { get; set; }

		public string Area_Number { get; set; }

		public string Password_Hash { get; set; }

		public string Salt { get; set; }

		public DateTime Registered { get; set; }

		public bool Is_Banned { get; set; } = false;
	}
}
