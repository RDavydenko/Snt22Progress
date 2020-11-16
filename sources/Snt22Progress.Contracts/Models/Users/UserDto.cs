using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.Contracts.Infrastructure;

namespace Snt22Progress.Contracts.Models.Users
{
	public class UserDto : IDto<int>
	{
		public int Id { get; set; }

		public string FNname { get; set; }

		public string SNname { get; set; }

		public string MName { get; set; }

		public int? Age { get; set; }

		public string Email { get; set; }

		public string Address { get; set; }

		public string AreaNumber { get; set; }

		public DateTime Registered { get; set; }

		public bool IsBanned { get; set; }
	}
}
