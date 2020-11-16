using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snt22Progress.Contracts.Models
{
	public class ChangePasswordDto
	{
		[Required]
		public string OldPassword { get; set; }

		[Required]
		public string NewPassword { get; set; }
	}
}
