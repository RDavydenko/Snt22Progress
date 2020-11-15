using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.DataAccess.Repositories;

namespace Snt22Progress.Tests.DataAccess
{
	[TestClass]
	public class BasePostgresRepositoryTest
	{
		[TestMethod]
		public async Task RepositoryConnectionTest()
		{
			string con = "Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=741741741;Search Path=progress;Pooling=false;";

			IRepository<User, int> usersRepository = new UsersRepository(con);
			IRepository<Advertisement, int> advertisementsRepository = new AdvertisementsRepository(con);
			IRepository<AdvertisementFile, int> advertisementFilesRepository = new AdvertisementFilesRepository(con);
			IRepository<Choise, int> choisesRepository = new ChoisesRepository(con);
			IRepository<Document, int> documentsRepository = new DocumentsRepository(con);
			IRepository<Post, int> postsRepository = new PostsRepository(con);
			IViewRepository<PostView, int> postViewsRepository = new PostViewsRepository(con);
			IRepository<Question, int> questionsRepository = new QuestionsRepository(con);
			IRepository<Role, int> rolesRepository = new RolesRepository(con);
			IRepository<UserToChoise, int> userToChoisesRepository = new UserToChoisesRepository(con);
			IRepository<UserToRole, int> userToRolesRepository = new UserToRolesRepository(con);
			IRepository<ValuePair, int> valuePairsRepository = new ValuePairsRepository(con);

			int id = 1;
			await usersRepository.GetAsync(id);
			await advertisementsRepository.GetAsync(id);
			await advertisementFilesRepository.GetAsync(id);
			await choisesRepository.GetAsync(id);
			await documentsRepository.GetAsync(id);
			await postsRepository.GetAsync(id);
			await postViewsRepository.GetAsync(id);
			await questionsRepository.GetAsync(id);
			await rolesRepository.GetAsync(id);
			await userToChoisesRepository.GetAsync(id);
			await userToRolesRepository.GetAsync(id);
			await valuePairsRepository.GetAsync(id);
		}


		[TestMethod]
		public async Task RepositoryMethodsTest()
		{
			string con = "Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=741741741;Search Path=progress;Pooling=false;";

			var repos = new UsersRepository(con);
			var users = (await repos.GetAsync()).ToArray();
						
			var nodeleted = await repos.DeleteAsync(-100);

			var users2 = (await repos.GetAsync()).ToArray();

			var users3 = await repos.GetAsync();
		}
	}
}
