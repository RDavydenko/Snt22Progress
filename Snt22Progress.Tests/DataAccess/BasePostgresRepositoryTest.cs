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
				id = 1,
				name = "Vasya",
				age = 10,
				surname = "Pupkin"
			});

			var r2 = await repos.AddAsync(new Snt22Progress.DataAccess.Models.User
			{
				id = 2,
				name = "Pupka",
				age = 10,
				surname = "Vasin"
			});

			var user = await repos.GetAsync(1);

			var user2 = await repos.GetAsync(10);

			var deleted = await repos.DeleteAsync(1);

			var nodeleted = await repos.DeleteAsync(100);

			var users2 = (await repos.GetAsync()).ToArray();

			users2[0].name = "Updated";
			users2[0].age = null;
			var updated = await repos.UpdateAsync(users2[0]);

			users2[1].id = 9;

			var updatedWithId = await repos.UpdateAsync(users[1]);


			var users3 = await repos.GetAsync();
		}
	}
}
