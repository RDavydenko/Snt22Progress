using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snt22Progress.Contracts.Models.Users
{
	public class UserRegisterDto
	{
		[Required]
		public string FName { get; set; }

		[Required]
		public string LName { get; set; }

		[Required]
		public string AreaNumber { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
