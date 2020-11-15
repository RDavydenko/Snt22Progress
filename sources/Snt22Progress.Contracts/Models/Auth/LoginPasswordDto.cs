using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Snt22Progress.Contracts.Models.Auth
{
	public class LoginPasswordDto
	{
		[Required(ErrorMessage = "{0} - обязательное поле")]
		public string Login { get; set; }

		[Required(ErrorMessage = "{0} - обязательное поле")]
		public string Password { get; set; }
	}
}
