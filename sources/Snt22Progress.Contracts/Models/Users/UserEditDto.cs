using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snt22Progress.Contracts.Models.Users
{
	public class UserEditDto
	{
		[Required]
		public string FName { get; set; }

		[Required]
		public string SName { get; set; }

		public string LName { get; set; }

		[Range(0, 150)]
		public int? Age { get; set; }

		public string Address { get; set; }

		public string AreaNumber { get; set; }
	}
}
