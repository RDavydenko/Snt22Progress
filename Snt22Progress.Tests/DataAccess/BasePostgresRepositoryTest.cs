using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snt22Progress.DataAccess.Repositories;

namespace Snt22Progress.Tests.DataAccess
{
	[TestClass]
	public class BasePostgresRepositoryTest
	{
		[TestMethod]
		public async Task RepositoryMethodsTest()
		{
			string con = "Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=741741741;Search Path=progress;Pooling=false;";

			var repos = new UsersRepository(con);
			var users = (await repos.GetAsync()).ToArray();

			var r = await repos.AddAsync(new Snt22Progress.DataAccess.Models.User
			{
				Id = 1,
				Name = "Vasya",
				Age = 10,
				Surname = "Pupkin"
			});
		}
	}
}
