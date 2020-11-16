using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.Contracts.Models
{
	public class ChangePasswordDto
	{
		public string OldPassword { get; set; }

		public string NewPassword { get; set; }
	}
}
