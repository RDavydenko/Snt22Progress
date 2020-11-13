﻿using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snt22Progress.DataAccess.Repositories
{
	public class UsersRepository : BasePostgresRepository<User, int>, IRepository<User, int>
	{
		public override string TableName => "progress.users";

		public UsersRepository(string connection) : base(connection)
		{

		}
	}
}
