﻿using System;
using System.Collections.Generic;
using System.Text;
using Snt22Progress.DataAccess.Infrastructure;

namespace Snt22Progress.DataAccess.Models
{
	public class UserToRole : IBaseEntity<int>
	{
		public int Id { get; set; }

		public int User_Id { get; set; }

		public int Role_Id { get; set; }

		public DateTime Created { get; set; }

		public int? Creator_Id { get; set; }
	}
}
