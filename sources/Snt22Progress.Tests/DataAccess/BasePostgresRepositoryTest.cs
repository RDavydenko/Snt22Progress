﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Snt22Progress.DataAccess.Infrastructure;
using Snt22Progress.DataAccess.Models;
using Snt22Progress.DataAccess.Repositories;
using Snt22Progress.DataAccess.Repositories.Interfaces;

namespace Snt22Progress.Tests.DataAccess
{
	[TestClass]
	public class BasePostgresRepositoryTest
	{
		string con = "Server=localhost;Port=5432;Database=postgres;User ID=postgres;Password=741741741;Search Path=progress;Pooling=false;";

		[TestMethod]
		public async Task RepositoryConnectionTest()
		{
			IRepository<User, int> usersRepository = new UsersRepository(con);
			IRepository<Advertisement, int> advertisementsRepository = new AdvertisementsRepository(con);
			IRepository<AdvertisementFile, int> advertisementFilesRepository = new AdvertisementFilesRepository(con);
			IViewRepository<AdvertisementView, int> advertisementViewsRepository = new AdvertisementViewsRepository(con);
			IRepository<DebtorFile, int> debtorFilesRepository = new DebtorFilesRepository(con);
			IRepository<Choise, int> choisesRepository = new ChoisesRepository(con);
			IRepository<Document, int> documentsRepository = new DocumentsRepository(con);
			IViewRepository<DocumentView, int> documentViewsRepository = new DocumentViewsRepository(con);
			IRepository<Post, int> postsRepository = new PostsRepository(con);
			IViewRepository<PostView, int> postViewsRepository = new PostViewsRepository(con);
			IRepository<Question, int> questionsRepository = new QuestionsRepository(con);
			IViewRepository<QuestionView, int> questionViewsRepository = new QuestionViewsRepository(con);
			IRepository<Legislation, int> legislationsRepository = new LegislationsRepository(con);
			IRepository<Role, int> rolesRepository = new RolesRepository(con);
			IRepository<UserToChoise, int> userToChoisesRepository = new UserToChoisesRepository(con);
			IRepository<UserToRole, int> userToRolesRepository = new UserToRolesRepository(con);
			IRepository<ValuePair, int> valuePairsRepository = new ValuePairsRepository(con);

			int id = 1;
			await usersRepository.GetAsync(id);
			await advertisementsRepository.GetAsync(id);
			await advertisementFilesRepository.GetAsync(id);
			await advertisementViewsRepository.GetAsync(id);
			await debtorFilesRepository.GetAsync(id);
			await choisesRepository.GetAsync(id);
			await documentsRepository.GetAsync(id);
			await documentViewsRepository.GetAsync(id);
			await postsRepository.GetAsync(id);
			await postViewsRepository.GetAsync(id);
			await questionsRepository.GetAsync(id);
			await questionViewsRepository.GetAsync(id);
			await legislationsRepository.GetAsync(id);
			await rolesRepository.GetAsync(id);
			await userToChoisesRepository.GetAsync(id);
			await userToRolesRepository.GetAsync(id);
			await valuePairsRepository.GetAsync(id);

			await usersRepository.GetAsync("WHERE id = 1");
			await advertisementsRepository.GetAsync("WHERE id = 1");
			await advertisementFilesRepository.GetAsync("WHERE id = 1");
			await advertisementViewsRepository.GetAsync("WHERE id = 1");
			await debtorFilesRepository.GetAsync("WHERE id = 1");
			await choisesRepository.GetAsync("WHERE id = 1");
			await documentsRepository.GetAsync("WHERE id = 1");
			await documentViewsRepository.GetAsync("WHERE id = 1");
			await postsRepository.GetAsync("WHERE id = 1");
			await postViewsRepository.GetAsync("WHERE id = 1");
			await questionsRepository.GetAsync("WHERE id = 1");
			await questionViewsRepository.GetAsync("WHERE id = 1");
			await legislationsRepository.GetAsync("WHERE id = 1");
			await rolesRepository.GetAsync("WHERE id = 1");
			await userToChoisesRepository.GetAsync("WHERE id = 1");
			await userToRolesRepository.GetAsync("WHERE id = 1");
			await valuePairsRepository.GetAsync("WHERE id = 1");
		}


		[TestMethod]
		public async Task RepositoryMethodsTest()
		{
			var repos = new UsersRepository(con);
			var users = (await repos.GetAsync()).ToArray();
						
			var nodeleted = await repos.DeleteAsync(-100);

			var users2 = (await repos.GetAsync()).ToArray();

			var users3 = await repos.GetAsync();
		}

		[TestMethod]
		public async Task PostsCountTest()
		{
			IPostViewRepository postViewRepository = new PostViewsRepository(con);
			var count = await postViewRepository.GetCountAsync();
		}
	}
}
